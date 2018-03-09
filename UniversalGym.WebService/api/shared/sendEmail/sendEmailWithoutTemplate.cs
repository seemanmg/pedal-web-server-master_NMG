namespace UniversalGym.Email
{
    using System;
    using System.Net.Mail;

    public class EmailNoTemplateHelper
    {
        public static bool SendEmail(string subject, string emailAddress, string body)
        {
            MailMessage message = new MailMessage
            {
                Subject = subject,
                From = new MailAddress("hello@pedal.com")
            };
            message.To.Add(emailAddress);
            message.CC.Add("chadd@pedal.com");
            

            message.Body = body;


            message.IsBodyHtml = false;
            SmtpClient client = new SmtpClient
            {
                ServicePoint = { MaxIdleTime = 1 }
            };
            //if (@Environment.GetEnvironmentVariable("isEmailOff").Equals("no"))
            //{
                client.EnableSsl = true;
                client.Send(message);
            //}
            message.Dispose();
            client.Dispose();
            return true;
        }


       


    }
}

