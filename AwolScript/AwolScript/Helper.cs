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
            MailMessage mail = new MailMessage("noreply@AutomatedAwol.com", email);
            SmtpClient client = new SmtpClient("172.16.0.43");
            client.Port = 25;
            client.UseDefaultCredentials = false;
            client.EnableSsl = false;
            client.Credentials = new NetworkCredential("", "");
            //mail.To.Add("add more peoples...");
            mail.Subject = subject;
            mail.IsBodyHtml = false;


            mail.Body = body;


            client.Send(mail);
        }


        public static void SendEmailWithReportToAgency(string email, string subject, string body)
        {
            MailMessage mail = new MailMessage("noreply@AutomatedAwol.com", email);
            SmtpClient client = new SmtpClient("172.16.0.43");
            client.Port = 25;
            client.UseDefaultCredentials = false;
            client.EnableSsl = false;
            client.Credentials = new NetworkCredential("", "");
            //mail.To.Add("add more peoples...");
            mail.Subject = subject;
            mail.IsBodyHtml = false;


            mail.Body = body;


            client.Send(mail);
        }


        
    }
}
