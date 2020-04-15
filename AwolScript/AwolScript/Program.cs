using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace AwolScript
{
    class Program
    {
        //Test Connection String
        public static string connectionString = "data source = 192.100.50.14; initial catalog = CC_Data; user id = BMG_WMS; password=E_cKyS*B4.!JrJW<;MultipleActiveResultSets=True;";

        //Live Connection String
        //public static string connectionString = "data source = BMG-DBEXT01\\BMG_PROD_EXT; initial catalog = CC_Data; user id = BMG_WMS; password=E_cKyS*B4.!JrJW<;MultipleActiveResultSets=True;";

        static void Main(string[] args)
        {
            try
            {

                Helper.SendEmail("mateusz.stacel@bmgresearch.com", "Awol Script Started", DateTime.Now.ToLocalTime().ToString() );
                //Program will be scheduled to run 2x a day at 11am and 6pm to scan peoples which didn't come in
                //to work but they have a schedule. Than agency will get notification and also internal stuff, user will be marked as awol.

                List<CC_Scheduling> AwolInterviewers = new List<CC_Scheduling>();


                //will scan everyone who not come in to work but have a schedule it will run check For all shift.
                AwolInterviewers = GetAwolInterviewers();

                if (AwolInterviewers != null)
                {
                    HandleAwolRun(AwolInterviewers);
                }
                else
                {
                    Helper.SendEmail("mateusz.stacel@bmgresearch.com", "Awol run", $"No awol interviewers at {DateTime.Now} ");
                }

                Helper.SendEmail("mateusz.stacel@bmgresearch.com", "Awol Script Finished", DateTime.Now.ToLocalTime().ToString());
            }
            catch (Exception e)
            {
                BMGServices.Errors.ErrorHandler errorHandler = new BMGServices.Errors.ErrorHandler("AWOL SCRIPT");

                errorHandler.LogError(e.ToString(),BMGServices.Errors.ErrorHandler.Severity.Medium,BMGServices.Errors.ErrorHandler.IPLocation.Server, null);
                Helper.SendEmail("mateusz.stacel@bmgresearch.com, Mokbul.Miah@bmgresearch.com", "Awol error", $" {e.ToString()} ");
            }

                  
        }


        private static void HandleAwolRun(List<CC_Scheduling> data)
        {

            //Create new Entry on CC Scheduling with Awol Status for each Awol Interviewer
            CreateNewAwolEntryInCCSchedulingSickLate(data);
            //get range of id's for Awol interviewers and update CCScheduling status to AWOL
            UpdateCCScheduleStatusToAwol(data.Select( x => x.ID).ToList());
            //Send report for interviewer Awol
            SendEmailWithReportAwol(data);
        }

        //Agency emails are disabled for testing time only internal peoples will get email.
        private static void SendEmailWithReportAwol(List<CC_Scheduling> data)
        {

            DBContext db = new DBContext();

            foreach (CC_Scheduling item in data)
            {

                CC_InterviewerList currentInterviewer = new CC_InterviewerList();

                using (var connection = new SqlConnection(connectionString))
                {
                    currentInterviewer = connection.Query<CC_InterviewerList>(@"SELECT * from CC_InterviewerList where IntNameID = @Interviewer", new { Interviewer = item.Interviewer }).FirstOrDefault();
                }

                string employedBy = currentInterviewer.EmployedBy;

                //Check if user details are not null some interviewers in interviewers list don't have this value.
                if (!String.IsNullOrWhiteSpace(employedBy))
                {
                    //Selecting multiple emails some agency contains multiple emails
                    //  List<string> sendToEmails = db.CC_AgencyDetails.Where(x => x.AgencyName.ToLower().Trim() == employedBy).Select(x => x.AgencyAMEmail).ToList();
                    string teamLeaderEmail = "";

                    using (var connection = new SqlConnection(connectionString))
                    {
                        CC_ManagerTeamLink interviewerManager = connection.Query<CC_ManagerTeamLink>(@"SELECT * from CC_ManagerTeamLink where Managers = @InterviewerManager", new { InterviewerManager = currentInterviewer.TEAM }).FirstOrDefault();
                       
                        if(interviewerManager == null)
                        {
                            teamLeaderEmail = "";
                        } else
                        {
                            teamLeaderEmail = interviewerManager.Email;
                        }
                        
                     
                    }


                    if (string.IsNullOrWhiteSpace(teamLeaderEmail))
                    {
                        teamLeaderEmail += $" {ConfigurationSettings.AppSettings["ManagerEmail"]}";
                    }
                    else
                    {
                        teamLeaderEmail += $", {ConfigurationSettings.AppSettings["ManagerEmail"]}";
                    }
                    
                    // If agency not exist in table, manager and team leader will recive email with details.
                    //if (sendToEmails == null)
                    //{
                    //    string subject = $"Awol, {currentInterviewer.IntNameID}";
                    //    string body = $"Awol notification for {currentInterviewer.IntNameID} were not send to agency as agency name were not found employedBy value: {employedBy}";
                    //    Helper.SendEmail(temLeaderEmail, subject, body);                     
                    //}

                    //string agencyEmails = String.Join(", ", sendToEmails.ToArray());
                    ////Append team leaders and manager for notifications
                    //agencyEmails += $", {temLeaderEmail}";

                    string PayId = "";

                    using (var connection = new SqlConnection(connectionString))
                    {
                        CC_AgencyDetails payId = connection.Query<CC_AgencyDetails>("SELECT * from CC_AgencyDetails where AgencyName = @EmployedBy", new { EmployedBy = "Next" }).FirstOrDefault();
                        PayId = payId.PayID;
                    }

                    if (PayId == null)
                    {
                        PayId = "BMG-";
                    }

                    int totalAwol;

                    using (var connection = new SqlConnection(connectionString))
                    {
                        totalAwol = connection.Query<CC_Scheduling>(@"SELECT *, [Shift-Start] as ShiftStart, [Shift-End] as ShiftEnd FROM CC_SchedulingSickLate where status = 'awol' and Interviewer = @Interviewer and Date >= DATEADD(MONTH, -3, GETDATE())", new { Interviewer = item.Interviewer }).ToList().Count;
                    }

                    string subject = $"{PayId} B - AWOL({totalAwol}) - {currentInterviewer.IntNameID}";

                    FileInfo fi = new FileInfo(System.Reflection.Assembly.GetEntryAssembly().Location);

                    var xsdf = 1;

                    string templatePath = Path.Combine(fi.Directory.ToString(), "EmailsTemplates\\AwolTemplate.txt");
                    string emailTemplate = System.IO.File.ReadAllText(templatePath);
                    string formatedTemplate = Format(emailTemplate, new
                    {
                     Interviewer = currentInterviewer.IntNameID,
                        shiftStart = item.ShiftStart.ToLongTimeString(),
                        ShiftEnd =  item.ShiftEnd.ToLongTimeString()
                
                    });

                    Helper.SendEmailWithReportToAgency(teamLeaderEmail, subject, formatedTemplate);

                }

            }

        }

        public static string Format(string input, object p)
        {
            foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(p))
                input = input.Replace("{" + prop.Name + "}", (prop.GetValue(p) ?? "N/A").ToString());

            return input;
        }

        private static void CreateNewAwolEntryInCCSchedulingSickLate(List<CC_Scheduling> data)
        {

  
            foreach (CC_Scheduling item in data)
            {
          
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Execute($@"INSERT INTO cc_schedulingsicklate 
                                                    (interviewer,
                                                     [date],
                                                     [shift-start],
                                                     [shift-end],
                                                     status,
                                                     totalhours,
                                                     comments,
                                                     manager,
                                                     entrydate,
                                                     intsign,
                                                     mansign,
                                                     approve,
                                                     [deny])
                                        VALUES(@Interviewer,
                                                     @Date,
                                                     @ShiftStart,
                                                     @ShiftEnd,
                                                     'AWOL',
                                                     @TotalHours,
                                                     'Automated AWOL',
                                                     @Team,
                                                     @DateNow,
                                                     0,
                                                     0,
                                                     0,
                                                     0); ", new { Interviewer = item.Interviewer, Date = DateTime.Today, ShiftStart = item.ShiftStart, ShiftEnd = item.ShiftEnd, TotalHours = item.TotalHours,Team = item.Team, DateNow = DateTime.Now });
                }
            }

        }

        private static void UpdateCCScheduleStatusToAwol(List<int> idsRange)
        {

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Execute(@"Update CC_Scheduling set Status = 'AWOL' WHERE id IN @idsRange;", new { idsRange });
            }
        }

        private static List<CC_Scheduling> GetAwolInterviewers()
        {

            string query = @"SELECT cc_scheduling.* , [Shift-Start] as ShiftStart, [Shift-End] as ShiftEnd, CC_Scheduling.[Autorisated By] as Autorisated_By

                                            FROM   cc_interviewerlist 
                                                   INNER JOIN cc_scheduling 
                                                           ON cc_scheduling.interviewer = cc_interviewerlist.intnameid 
														   LEFT outer JOIN Master_Activity on Master_Activity.IntName = CC_InterviewerList.IntNameID
														   and Master_Activity.Date = CC_Scheduling.Date
                                            WHERE  cc_interviewerlist.live = 1 
                                                   AND cc_scheduling.status = 'core' 
                                                   AND cc_scheduling.approved = 1 
                                                   AND cc_scheduling.[in] = 0 
                                                   AND Cast(cc_scheduling.date AS DATE) = Cast(Getdate() AS DATE) 
                                                   AND CONVERT(TIME, cc_scheduling.[shift-start]) < CONVERT(TIME, Getdate())
												   and Master_Activity.Date is null";

            using (var connection = new SqlConnection(connectionString))
            {
                List<CC_Scheduling> data = connection.Query<CC_Scheduling>(query).ToList();

                return data;
            };
            
        }

    }


}







