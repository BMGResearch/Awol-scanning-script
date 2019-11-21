﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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
        public static string connectionString = "data source = 192.100.50.14; initial catalog = CC_Data_WMS_LIVE; user id = BMG_WMS; password=E_cKyS*B4.!JrJW<;MultipleActiveResultSets=True;";

        //Live Connection String
       // public static string connectionString = "data source = BMG-DBEXT01\\BMG_PROD_EXT; initial catalog = CC_Data; user id = BMG_WMS; password=E_cKyS*B4.!JrJW<;MultipleActiveResultSets=True;";


        static void Main(string[] args)
        {
            try
            {
                //Program will be scheduled to run 2x a day at 11am and 6pm to scan peoples which didn't come in
                //to work but they have a schedule. Than agency will get notyfication and also internal stuff, user will be marked as awol.
                int currentHour = DateTime.Now.Hour;

                List<CC_Scheduling> AwolInterviewers = new List<CC_Scheduling>();

                //will scan everyone who not come in to work but have a schedule it will run check For all shift.
                AwolInterviewers = GetAwolInterviewers();

                if (AwolInterviewers != null)
                {
                    HandleAwolRun(AwolInterviewers);
                }
                else
                {
                    Helper.SendEmail("mateusz.stacel@bmgresearch.co.uk, Mokbul.Miah@bmgresearch.co.uk", "Awol run", $"No awol interviewers at {DateTime.Now} ");
                }
            }
            catch (Exception e)
            {

                Helper.SendEmail("mateusz.stacel@bmgresearch.co.uk, Mokbul.Miah@bmgresearch.co.uk", "Awol error", $" {e.ToString()} ");
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
                CC_InterviewerList currentInterviewer = db.CC_InterviewerList.Where(x => x.IntNameID == item.Interviewer).FirstOrDefault();

                string employedBy = currentInterviewer.EmployedBy;

                //Check if user details are not null some interviewers in interviewers list don't have this value.
                if (!String.IsNullOrWhiteSpace(employedBy))
                {
                    //Selecting multiple emails some agency contains multiple emails
                    //  List<string> sendToEmails = db.CC_AgencyDetails.Where(x => x.AgencyName.ToLower().Trim() == employedBy).Select(x => x.AgencyAMEmail).ToList();
                    string teamLeaderEmail = "";

                    teamLeaderEmail = "";// db.CC_ManagerTeamLink.Where(x => x.Managers == currentInterviewer.TEAM).Select(x => x.Email).FirstOrDefault();

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
                    //    string body = $"Awol notyfication for {currentInterviewer.IntNameID} were not send to agency as agency name were not found employedBy value: {employedBy}";
                    //    Helper.SendEmail(temLeaderEmail, subject, body);                     
                    //}

                    //string agencyEmails = String.Join(", ", sendToEmails.ToArray());
                    ////Append team leaders and manager for notyfications
                    //agencyEmails += $", {temLeaderEmail}";

                    string PayId = db.CC_AgencyDetails.Where(x => x.AgencyName.ToLower().Trim() == employedBy).Select(x => x.PayID).FirstOrDefault();

                    if(PayId == null)
                    {
                        PayId = "BMG-";
                    }


                    int totalAwol = db.CC_SchedulingSickLate.SqlQuery($"SELECT *, [Shift-Start] as ShiftStart, [Shift-End] as ShiftEnd FROM CC_SchedulingSickLate where status = 'awol' and Interviewer = '{item.Interviewer}' and Date >= DATEADD(MONTH, -3, GETDATE())").ToList().Count;

                    string subject = $"{PayId} B - AWOL({totalAwol}) - {currentInterviewer.IntNameID}";
                    string body = $"Awol notyfication for {currentInterviewer.IntNameID}, shift start: {item.ShiftStart.ToLongTimeString()} / shift end: {item.ShiftEnd.ToLongTimeString()}";
                    Helper.SendEmailWithReportToAgency(teamLeaderEmail, subject, body);

                }



            }

        }

        private static void CreateNewAwolEntryInCCSchedulingSickLate(List<CC_Scheduling> data)
        {


        


            DBContext db = new DBContext();

            // List<CC_SchedulingSickLate> newRangeToAdd = new List<CC_SchedulingSickLate>();
            string query = "";
            foreach (CC_Scheduling item in data)
            {

                 query += $@"INSERT INTO cc_schedulingsicklate 
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
                                        VALUES('{item.Interviewer}',
                                                     '{DateTime.Today}',
                                                     '{item.ShiftStart}',
                                                     '{item.ShiftEnd}',
                                                     'AWOL',
                                                     {item.TotalHours},
                                                     'Automated AWOL',
                                                     '{item.Team}',
                                                     '{DateTime.Now}',
                                                     0,
                                                     0,
                                                     0,
                                                     0);";

                //CC_SchedulingSickLate newRecord = new CC_SchedulingSickLate
                //{
                //    Interviewer = item.Interviewer,
                //    Date = DateTime.Today,
                //    ShiftStart = item.ShiftStart,
                //    ShiftEnd = item.ShiftEnd,
                //    Status = "AWOL",
                //    TotalHours = item.TotalHours,
                //    Comments = "Automated AWOL",
                //    Manager = item.Team,
                //    EntryDate = DateTime.Now,
                //    IntSign = false,
                //    ManSign = false,
                //    Approve = false,
                //    Deny = false
                //};



                //newRangeToAdd.Add(newRecord);
            }

            using (var connection = new SqlConnection(connectionString))
            {
               int resultback = connection.Execute(query);
            }

            //  db.CC_SchedulingSickLate.AddRange(newRangeToAdd);

            //   db.SaveChanges();
        }

        private static string GetIdsRange(List<CC_Scheduling> dataSet)
        {
            //exctract ids from  awol interviewers list
            List<int> ids = dataSet.Select(x => x.ID).ToList();
           //prepare ids range for sql query 
            string idsRange = String.Join(", ", ids);

            return idsRange;
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
                                            WHERE  cc_interviewerlist.live = 1 
                                                   AND cc_scheduling.status = 'core' 
                                                   AND cc_scheduling.approved = 1 
                                                   AND cc_scheduling.[in] = 0 
                                                   AND Cast(cc_scheduling.date AS DATE) = Cast(Getdate() AS DATE) 
                                                   AND CONVERT(TIME, cc_scheduling.[shift-start]) < CONVERT(TIME, Getdate())
                                ";

            using (var connection = new SqlConnection(connectionString))
            {
                List<CC_Scheduling> data = connection.Query<CC_Scheduling>(query).ToList();

                return data;
            };

          //  DBContext context = new DBContext();

          //  List<CC_Scheduling> data = context.CC_Scheduling.SqlQuery(query).ToList();

            
        }


        private static List<T> ConvertDataTable<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }
        private static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)

                        if (!String.IsNullOrEmpty(dr[column.ColumnName].ToString()))
                        {
                            pro.SetValue(obj, dr[column.ColumnName]);
                        }
                        else
                        {
                            continue;
                        }

                    else
                    {
                        continue;
                    }

                }
            }
            return obj;
        }
    }


}






