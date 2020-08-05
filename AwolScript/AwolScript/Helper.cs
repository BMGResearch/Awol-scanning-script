using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace AwolScript
{
    class Helper
    {

        public static void SendEmail(string email, string subject, string body)
        {

            BMGServices.Email.EmailHelper emailHelper = new BMGServices.Email.EmailHelper("Awol Script");

            List<string> emailsTo = email.Split(',').ToList();
            emailHelper.SendStandardBMGEmail(subject, body, emailsTo);

        }


        public static void SendEmailWithReportToAgency(string email, string subject, string body)
        {

            BMGServices.Email.EmailHelper emailHelper = new BMGServices.Email.EmailHelper("Awol Script");

            List<string> emailsTo = email.Split(',').ToList();
            emailHelper.SendStandardBMGEmail(subject, body, emailsTo);
        }


        
    }
}
