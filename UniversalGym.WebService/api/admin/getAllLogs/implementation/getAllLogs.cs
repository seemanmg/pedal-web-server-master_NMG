using UniversalGym.Responses;
using UniversalGym.Requests;
using UniversalGym.Data;
using System.Linq;
using System.Collections.Generic;
using System;

namespace UniversalGym.Admin
{
    public class getAllLogs
    {
        public static getAllLogsResponse getAllLogsImplementation(getAllLogsRequest request)
        {


            if (request.authToken != Constants.MarketingToken)
                return new getAllLogsResponse() { success = false, status = 404, message = "Wrong Token" };

            var rv = new getAllLogsResponse();


            rv.logs = new List<Responses.Logs>();
            using (var db = new UniversalGymEntities())
            {

                var logs = Enumerable.Empty<Data.Log>(); ;
                if (request.getAction && request.getError)
                {
                    logs = db.Logs.OrderByDescending(w => w.InsertDate).ToList().Take(100);
                }
                else if (request.getAction && !request.getError)
                {
                    logs = db.Logs.OrderByDescending(w => w.InsertDate).Where(w => w.IsError == false).ToList().Take(100);
                }
                else if (!request.getAction && request.getError)
                {
                    logs = db.Logs.OrderByDescending(w => w.InsertDate).Where(w => w.IsError == true).ToList().Take(100);

                }
                foreach (var log in logs)
                {
                    var temp = new Responses.Logs
                    {
                        LogId = log.LogId.ToString() ?? "NULL",
                        LogMessage = log.LogMessage ?? "NULL",
                        InsertDate = log.InsertDate.ToString() ?? "NULL",
                        IsError = log.IsError.ToString() ?? "NULL",
                    };

                    rv.logs.Add(temp);
                }

            }
            rv.success = true;
            rv.message = "";
            return rv;
        }
    }
}