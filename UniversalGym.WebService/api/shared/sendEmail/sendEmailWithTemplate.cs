namespace UniversalGym.Email
{
    using System;
    using System.IO;
    using System.Net.Mail;
    using System.Web;
    using System.Web.Caching;

    public class EmailTemplateHelper
    {

        public static bool SendEmail(string subject, string emailAddress, string link, string name, string templateURL, string extraMessage = "")
        {
            MailMessage message = new MailMessage
            {
                Subject = subject,
                From = new MailAddress("hello@pedal.com")
            };
            message.To.Add(emailAddress);


            message.Body = PrepareMailBodyWith(templateURL, new string[] { "Link", link, "Name", name, "Subject", subject, "Message", extraMessage });


            message.IsBodyHtml = true;
            SmtpClient client = new SmtpClient
            {
                ServicePoint = { MaxIdleTime = 1 }
            };

            if (@Environment.GetEnvironmentVariable("isEmailOff").Equals("no"))
            {
                client.Send(message);
            }
            message.Dispose();
            client.Dispose();
            return true;
        }


        private static string PrepareMailBodyWith(string templateName, params string[] pairs)
        {
            var mailBodyOfTemplate = GetMailBodyOfTemplate(templateName);
            for (var i = 0; i < pairs.Length; i += 2)
            {
                //mailBodyOfTemplate = mailBodyOfTemplate.Replace("<%={0}%>".FormatWith(new object[] { pairs[i] }), pairs[i + 1]);
                mailBodyOfTemplate = mailBodyOfTemplate.Replace(String.Format("<%={0}%>", new object[] { pairs[i] }), pairs[i + 1]);
            }

            if (!String.IsNullOrEmpty(mailBodyOfTemplate) && mailBodyOfTemplate.Contains("<%=HEADERLOCATION%>"))
            {
                string host = "http://pictures.pedal.com/backend/email-logo.png";
                mailBodyOfTemplate = mailBodyOfTemplate.Replace("<%=HEADERLOCATION%>", host);
            }

            return mailBodyOfTemplate;
        }
      
        private static string GetMailBodyOfTemplate(string templateName)
        {
            string key = "mailTemplate:" + templateName;
            if (HttpContext.Current != null)
            {
                var str2 = (string)HttpContext.Current.Cache[key];
                if (String.IsNullOrEmpty(str2))
                {
                    str2 = ReadFileFrom(templateName);
                    if (!String.IsNullOrEmpty(str2))
                    {
                        HttpContext.Current.Cache.Insert(key, str2, null, DateTime.Now.AddHours(1.0),
                            Cache.NoSlidingExpiration);
                    }
                }
                return str2;
            }
            else
            {
                var str2 = ReadFileFrom(templateName);
                return str2;
            }
        }


        private static string ReadFileFrom(string templateName)
        {

            return File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"\assets\emailTemplates\" + templateName);

        }
    } 
}

