namespace UniversalGym
{
    using Email;
    using Slack;
    using System;
    using System.IO;
    using System.Linq;
    using System.Web;
    using UniversalGym.Data;

    public class Logs
    {

        public static void writeLog(string message)
        {
            StreamWriter writer = new StreamWriter(Path.Combine(HttpRuntime.AppDomainAppPath, @"UserLogs\nov12.xls"), true);
            writer.WriteLine(message);
            writer.Flush();
            writer.Close();
            writer.Dispose();

        }

        public static void LogsInsertError(Exception exception)
        {
            using (var db = new UniversalGymEntities())
            {


                Log log = new Log
                {
                    InsertDate = DateTime.UtcNow,
                    IsError = true
                };

                log.LogMessage = "";
                if (exception.Message != null)
                {
                    log.LogMessage = log.LogMessage 
                        + "Error: " 
                        + exception.Message;
                }
                else if (exception.StackTrace != null)
                {
                    log.LogMessage = log.LogMessage
                                            + "| stacktrace : "
                                            + exception.StackTrace;
                }
                else if (exception.InnerException != null)
                {
                    log.LogMessage = log.LogMessage + "inner:";

                    if (exception.InnerException.Message != null)
                    {
                        log.LogMessage = log.LogMessage
                            + " "
                            + exception.Message;
                    }
                    else if (exception.InnerException.StackTrace != null)
                    {
                        log.LogMessage = log.LogMessage
                                                + " "
                                                + exception.StackTrace;
                    }
                }
                
                db.Logs.Add(log);

                db.SaveChanges();



                var lastLogs = db.Logs.OrderByDescending(o => o.LogId).Take(6).ToArray();
                var body = "Exception: " + Environment.NewLine;
                foreach (Log lastLog in lastLogs)
                {
                    body = body 
                        + "Id: " 
                        + lastLog.LogId
                        + Environment.NewLine
                        + "Date: "
                        + lastLog.InsertDate
                        + Environment.NewLine
                        + "LogMessage: "
                        + lastLog.LogMessage
                        + Environment.NewLine
                        + Environment.NewLine;
                }

                SlackHelper.sendErrorChannel(body);

                /*EmailNoTemplateHelper.SendEmail(
                    "Exception Found", 
                    "error@pedal.com", 
                    body);*/

            }
        }

        public static void LogsInsertAction(string message, bool send = true)
        {
            using (var db = new UniversalGymEntities())
            {
                Log log = new Log
                {
                    InsertDate = DateTime.UtcNow,
                    LogMessage = message,
                    IsError = false
                };
                db.Logs.Add(log);
               
                db.SaveChanges();

                if (send)
                {
                    var body = "Action: "
                            + "Id: "
                            + log.LogId
                            + Environment.NewLine
                            + "Date: "
                            + log.InsertDate
                            + Environment.NewLine
                            + "LogMessage: "
                            + log.LogMessage
                            + Environment.NewLine
                            + Environment.NewLine;

                    SlackHelper.sendActionChannel(body);

                }
            }
        }

    }

   
}

