using UniversalGym.Responses;
using UniversalGym.Requests;
using UniversalGym.Data;
using System.Linq;
using System.Collections.Generic;
using System;

namespace UniversalGym.Admin
{
    public class getAllGymInvoices
    {
        public static getAllGymInvoicesResponse getAllGymInvoicesImplementation(BaseRequest request)
        {

            if (request.authToken != Constants.MarketingToken)
                return new getAllGymInvoicesResponse() { success = false, status = 404, message = "Wrong Token" };

            var rv = new getAllGymInvoicesResponse();



            rv.gymInvoices = new List<Responses.GymInvoices>();
            using (var db = new UniversalGymEntities())
            {
                var gymInvoices = db.GymInvoices.ToList();
                foreach (var gymInvoice in gymInvoices)
                {
                    var temp = new Responses.GymInvoices
                    {
                        Id = gymInvoice.Id.ToString() ?? "NULL",
                        StartPeriodDate = gymInvoice.StartPeriodDate.ToString() ?? "NULL",
                        EndPeriodDate = gymInvoice.EndPeriodDate.ToString() ?? "NULL",
                        AmountPaid = gymInvoice.AmountPaid.ToString() ?? "NULL",
                        IsCollected = gymInvoice.IsCollected.ToString() ?? "NULL",
                        GymId = gymInvoice.GymId.ToString() ?? "NULL",
                    };

                    rv.gymInvoices.Add(temp);
                }

            }

            rv.success = true;
            rv.message = "";
            return rv;
        }
    }
}