using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using UniversalGym.Data;

namespace UniversalGym
{
    /// <summary>
    /// Summary description for Handler1
    /// </summary>
    public class SlackHandler : IHttpHandler
    {

        private bool payloadNotSent = true;
        private Queue<string> commandQueue = new Queue<string>();

        public void ProcessRequest(HttpContext context)
        {


            try
            {

                // example:
                /*json = { 
                    token = gIkuvaNzQIHg97ATvDxqgjtO
                    team_id = T0001
                    team_domain = example
                    channel_id = C2147483705
                    channel_name = test
                    user_id = U2147483697
                    user_name = Steve
                    command =/ weather
                    text = 94070
                    response_url = https://hooks.slack.com/commands/1234/5678 
                }*/

                Logs.LogsInsertAction("Slack command attempted");

                if (context == null || context.Request["token"] == null || context.Request["text"] == null || context.Request["response_url"] == null)
                {
                    return;
                }

                string token = context.Request["token"];
                string text = context.Request["text"];
                string url = context.Request["response_url"];

                if (token.Equals("sru5Fa7oNZ1jyxaLgbBKDpK1"))
                {
                    var commandArray = text.Split(' ');
                    foreach (string thisCommand in commandArray)
                    {
                        commandQueue.Enqueue(thisCommand);
                    }

                    using (var db = new UniversalGymEntities())
                    {


                        if (commandQueue.Count > 0)
                        {
                            var command = commandQueue.Dequeue();
                            if (command.Equals("help"))
                            {
                                var message =
                                    "COMMANDS:"
                                    + Environment.NewLine
                                    + "WEB CACHING"
                                    + Environment.NewLine
                                    + "cache reset"
                                    + Environment.NewLine
                                    + "cache development - 'Development Mode' turns off the cache for 3 hours"
                                    + Environment.NewLine
                                    + "cache devstatus - Returns time in seconds development mode"
                                    + Environment.NewLine
                                    + Environment.NewLine
                                    + "STATISTICS"
                                    + Environment.NewLine
                                    + "summary"
                                    + Environment.NewLine
                                    + "gympass summary"
                                    + Environment.NewLine
                                    + "user summary"
                                    + Environment.NewLine
                                    + "gym summary"
                                    + Environment.NewLine
                                    + "searchrequests summary"
                                    + Environment.NewLine
                                    + "givecredits summary"
                                    + Environment.NewLine
                                    + Environment.NewLine
                                    + "OPTIONAL COMMAND FILTERS (add to end of command):"
                                    + Environment.NewLine
                                    + "Example:"
                                    + Environment.NewLine
                                    + "      /pedal gym summary from 2008 to 2009/12/31 23:59:59 compare from 2010 show"
                                    + Environment.NewLine
                                    + "Shows to everyone in channel the stats of all gyms from the start of 2008 to the end of 2009 compared to the stats of all gyms from 2010 to now"
                                    + Environment.NewLine
                                    + Environment.NewLine
                                    + "DATERANGE: summarizes information only in daterange"
                                    + Environment.NewLine
                                    + "DATERANGE compare from DATERANGE: summarizes information only in daterange and compares changes from another daterange"
                                    + Environment.NewLine
                                    + "show : shows response to everyone in the channel (must be after other filters)"
                                    + Environment.NewLine
                                    + "DATERANGE is one of:"
                                    + Environment.NewLine
                                    + "from DATE : from a date to now"
                                    + Environment.NewLine
                                    + "from DATE to DATE: from a date to another date"
                                    + Environment.NewLine
                                    + Environment.NewLine
                                    + "DATE is one of:"
                                    + Environment.NewLine
                                    + "now: Right now"
                                    + Environment.NewLine
                                    + "2008 : The start of a year"
                                    + Environment.NewLine
                                    + "2008/02 : The start of a month"
                                    + Environment.NewLine
                                    + "2008/02/14 : The start of a day"
                                    + Environment.NewLine
                                    + "2008/02/14 14 : The start of an hour"
                                    + Environment.NewLine
                                    + "2008/02/14 14:50 : The start of a minute"
                                    + Environment.NewLine
                                    + "2008/02/14 14:50:50 : The start of a second";

                                sendMessageAsync(message, getResponseType(false), url);
                            }
                            else if (command.Equals("cache"))
                            {
                                runCloudflareMessage(url);
                            }
                            else if (command.Equals("summary"))
                            {
                                var message = "";
                                var responseType = false;

                                if (commandQueue.Count > 0)
                                {
                                    var filter = commandQueue.Peek();
                                    if (filter.Equals("from"))
                                    {
                                        filter = commandQueue.Dequeue();
                                        Tuple<DateTime?, DateTime?, DateTime?, DateTime?> fromDates = fromFilter();


                                        if (fromDates.Item1 != null && fromDates.Item3 != null)
                                        {
                                            var toDate = fromDates.Item2 ?? DateTime.Now;
                                            var compareToDate = fromDates.Item4 ?? DateTime.Now;


                                            var fromGymPassCount = db.GymPasses.Where(x => x.ServerTimeDateBought >= fromDates.Item1 && x.ServerTimeDateBought < toDate).ToList().Count;
                                            var compareGymPassCount = db.GymPasses.Where(x => x.ServerTimeDateBought >= fromDates.Item3 && x.ServerTimeDateBought < compareToDate).ToList().Count;
                                            var fromGymCount = db.Gyms.Where(x => x.ActiveDate >= fromDates.Item1 && x.ActiveDate < toDate).ToList().Count;
                                            var compareGymCount = db.Gyms.Where(x => x.ActiveDate >= fromDates.Item3 && x.ActiveDate < compareToDate).ToList().Count;
                                            var fromUserCount = db.Users.Where(x => x.joinDate >= fromDates.Item1 && x.joinDate < toDate).ToList().Count;
                                            var compareUserCount = db.Users.Where(x => x.joinDate >= fromDates.Item3 && x.joinDate < compareToDate).ToList().Count;

                                            var passData =
                                                        from gp in db.GymPasses
                                                        group gp by 1 into g
                                                        select new
                                                        {
                                                            fromTotalCost = g.Where(x => x.ServerTimeDateBought >= fromDates.Item1 && x.ServerTimeDateBought < toDate).Sum(x => x.GymPassCost),
                                                            compareTotalCost = g.Where(x => x.ServerTimeDateBought >= fromDates.Item3 && x.ServerTimeDateBought < compareToDate).Sum(x => x.GymPassCost),
                                                            fromTotalCreditsUsed = g.Where(x => x.ServerTimeDateBought >= fromDates.Item1 && x.ServerTimeDateBought < toDate).Sum(x => x.CreditsUsed),
                                                            compareTotalCreditsUsed = g.Where(x => x.ServerTimeDateBought >= fromDates.Item3 && x.ServerTimeDateBought < compareToDate).Sum(x => x.CreditsUsed),
                                                            fromTotalCharged = g.Where(x => x.ServerTimeDateBought >= fromDates.Item1 && x.ServerTimeDateBought < toDate).Sum(x => x.AmountCharged),
                                                            compareTotalCharged = g.Where(x => x.ServerTimeDateBought >= fromDates.Item3 && x.ServerTimeDateBought < compareToDate).Sum(x => x.AmountCharged),

                                                        };

                                            var fromPassRevenue = 0;
                                            var comparePassRevenue = 0;
                                            var fromCosts = 0;
                                            var compareCosts = 0;
                                            if (passData.SingleOrDefault() != null)
                                            {
                                                fromPassRevenue = passData.SingleOrDefault().fromTotalCharged + passData.SingleOrDefault().compareTotalCreditsUsed;
                                                comparePassRevenue = passData.SingleOrDefault().compareTotalCharged + passData.SingleOrDefault().compareTotalCreditsUsed;
                                                fromCosts = passData.SingleOrDefault().fromTotalCost;
                                                compareCosts = passData.SingleOrDefault().compareTotalCost;
                                            }


                                            var giveCreditData =
                                                from gp in db.GiveCredits
                                                group gp by 1 into g
                                                select new
                                                {
                                                    fromTotalCredit = g.Where(x => x.DateTime >= fromDates.Item1 && x.DateTime < toDate).Sum(x => x.AmountCharged),
                                                    compareTotalCredit = g.Where(x => x.DateTime >= fromDates.Item3 && x.DateTime < compareToDate).Sum(x => x.AmountCharged),
                                                };
                                            var fromRevenue = 0;
                                            var compareRevenue = 0;
                                            if (giveCreditData.SingleOrDefault() != null)
                                            {
                                                fromRevenue = giveCreditData.SingleOrDefault().fromTotalCredit ?? 0;
                                                compareRevenue = giveCreditData.SingleOrDefault().compareTotalCredit ?? 0;
                                            }

                                            var gymData =
                                                        from gp in db.GymInvoices
                                                        group gp by 1 into g
                                                        select new
                                                        {
                                                            fromUncollected = g.Where(x => x.IsCollected == false && x.StartPeriodDate >= fromDates.Item1 && x.StartPeriodDate < toDate).Sum(x => x.AmountPaid),
                                                            compareUncollected = g.Where(x => x.IsCollected == false && x.StartPeriodDate >= fromDates.Item3 && x.StartPeriodDate < compareToDate).Sum(x => x.AmountPaid),
                                                        };

                                            var fromUncollectedPayment = 0;
                                            var compareUncollectedPayment = 0;
                                            if (gymData.SingleOrDefault() != null)
                                            {
                                                fromUncollectedPayment = gymData.SingleOrDefault().fromUncollected;
                                                compareUncollectedPayment = gymData.SingleOrDefault().compareUncollected;
                                            }

                                            var fromUnusedCredits = db.Users.AsEnumerable().Where(x => x.joinDate >= fromDates.Item1 && x.joinDate < toDate).Sum(x => x.Credits);
                                            var compareUnusedCredits = db.Users.AsEnumerable().Where(x => x.joinDate >= fromDates.Item3 && x.joinDate < compareToDate).Sum(x => x.Credits);

                                            var fromKnownprofit = fromRevenue - fromCosts - fromUnusedCredits;
                                            var compareKnownprofit = compareRevenue - compareCosts - compareUnusedCredits;


                                            message = "Summary from " + fromDates.Item1 + " to " + toDate + " compared from " + fromDates.Item3 + " to " + compareToDate
                                            + Environment.NewLine
                                            + "+ Revenue: "
                                            + formatDollarComparison(fromRevenue, compareRevenue)
                                            + Environment.NewLine
                                            + "- Costs: "
                                            + formatDollarComparison(fromCosts, compareCosts)
                                            + Environment.NewLine
                                            + "- Unused Credits: "
                                            + formatDollarComparison(fromUnusedCredits, compareUnusedCredits)
                                            + Environment.NewLine
                                            + " = Known Profit: "
                                            + formatDollarComparison(fromKnownprofit, compareKnownprofit)
                                            + Environment.NewLine
                                            + Environment.NewLine
                                            + "Pass Revenue: "
                                            + formatDollarComparison(fromPassRevenue, comparePassRevenue)
                                            + Environment.NewLine
                                            + "Uncollected Gym Payments (in costs): "
                                            + formatDollarComparison(fromUncollectedPayment, compareUncollectedPayment)
                                            + Environment.NewLine
                                            + "Number of gym passes: "
                                            + formatComparison(fromGymPassCount, compareGymPassCount)
                                            + Environment.NewLine
                                            + "Number of users: "
                                            + formatComparison(fromUserCount, compareUserCount)
                                            + Environment.NewLine
                                            + "Number of gyms: "
                                            + formatComparison(fromGymCount, compareGymCount)
                                            + Environment.NewLine;


                                        }
                                        else if (fromDates.Item1 != null && fromDates.Item3 == null)
                                        {
                                            var toDate = fromDates.Item2 ?? DateTime.Now;


                                            var gymPassCount = db.GymPasses.Where(x => x.ServerTimeDateBought >= fromDates.Item1 && x.ServerTimeDateBought < toDate).ToList().Count;
                                            var gymCount = db.Gyms.Where(x => x.ActiveDate >= fromDates.Item1 && x.ActiveDate < toDate).ToList().Count;
                                            var userCount = db.Users.Where(x => x.joinDate >= fromDates.Item1 && x.joinDate < toDate).ToList().Count;

                                            var passData =
                                                        from gp in db.GymPasses.Where(x => x.ServerTimeDateBought >= fromDates.Item1 && x.ServerTimeDateBought < toDate)
                                                        group gp by 1 into g
                                                        select new
                                                        {
                                                            totalCost = g.Sum(x => x.GymPassCost),
                                                            totalCreditsUsed = g.Sum(x => x.CreditsUsed),
                                                            totalCharged = g.Sum(x => x.AmountCharged),

                                                        };
                                            var passRevenue = 0;
                                            var costs = 0;
                                            if (passData.SingleOrDefault() != null)
                                            {
                                                passRevenue = passData.SingleOrDefault().totalCharged + passData.SingleOrDefault().totalCreditsUsed;
                                                costs = passData.SingleOrDefault().totalCost;
                                            }

                                            var giveCreditData =
                                                from gp in db.GiveCredits.Where(x => x.DateTime >= fromDates.Item1 && x.DateTime < toDate)
                                                group gp by 1 into g
                                                select new
                                                {
                                                    totalCredit = g.Sum(x => x.AmountCharged),
                                                };
                                            var revenue = 0;
                                            if (giveCreditData.SingleOrDefault() != null)
                                            {
                                                revenue = giveCreditData.SingleOrDefault().totalCredit ?? 0;
                                            }

                                            var gymData =
                                                        from gp in db.GymInvoices.Where(x => x.StartPeriodDate >= fromDates.Item1 && x.StartPeriodDate < toDate)
                                                        group gp by 1 into g
                                                        select new
                                                        {
                                                            uncollected = g.Where(x => x.IsCollected == false).Sum(x => x.AmountPaid),
                                                        };

                                            var uncollectedPayment = 0;
                                            if (gymData.SingleOrDefault() != null)
                                            {
                                                uncollectedPayment = gymData.SingleOrDefault().uncollected;
                                            }
                                            var userData =
                                                        from gp in db.Users.Where(x => x.joinDate >= fromDates.Item1 && x.joinDate < toDate)
                                                        group gp by 1 into g
                                                        select new
                                                        {
                                                            unused = g.Sum(x => x.Credits),

                                                        };
                                            var unusedCredits = 0;
                                            if (userData.SingleOrDefault() != null)
                                            {
                                                unusedCredits = userData.SingleOrDefault().unused;
                                            }

                                            var knownprofit = revenue - costs - unusedCredits;


                                            message = "Summary from " + fromDates.Item1 + " to " + toDate
                                            + Environment.NewLine
                                            + "+ Revenue: "
                                            + formatDollar(revenue)
                                            + Environment.NewLine
                                            + "- Costs: "
                                            + formatDollar(costs)
                                            + Environment.NewLine
                                            + "- Unused Credits: "
                                            + formatDollar(unusedCredits)
                                            + Environment.NewLine
                                            + " = Known Profit: "
                                            + formatDollar(knownprofit)
                                            + Environment.NewLine
                                            + Environment.NewLine
                                            + "Pass Revenue: "
                                            + formatDollar(passRevenue)
                                            + Environment.NewLine
                                            + "Uncollected Gym Payments (in costs): "
                                            + formatDollar(uncollectedPayment)
                                            + Environment.NewLine
                                            + "Number of gym passes: "
                                            + gymPassCount
                                            + Environment.NewLine
                                            + "Number of users: "
                                            + userCount
                                            + Environment.NewLine
                                            + "Number of gyms: "
                                            + gymCount
                                            + Environment.NewLine;

                                        }


                                    }

                                    while (commandQueue.Count > 0)
                                    {
                                        if (commandQueue.Dequeue().Equals("show"))
                                        {
                                            responseType = true;
                                        }
                                    }


                                }

                                if (message.Equals(""))
                                {

                                    var gymPassCount = db.GymPasses.ToList().Count;
                                    var gymCount = db.Gyms.ToList().Count;
                                    var userCount = db.Users.ToList().Count;

                                    var passData =
                                                from gp in db.GymPasses
                                                group gp by 1 into g
                                                select new
                                                {
                                                    totalCost = g.Sum(x => x.GymPassCost),
                                                    totalCreditsUsed = g.Sum(x => x.CreditsUsed),
                                                    totalCharged = g.Sum(x => x.AmountCharged),

                                                };
                                    var passRevenue = 0;
                                    var costs = 0;
                                    if (passData.SingleOrDefault() != null)
                                    {
                                        passRevenue = passData.SingleOrDefault().totalCharged + passData.SingleOrDefault().totalCreditsUsed;
                                        costs = passData.SingleOrDefault().totalCost;
                                    }

                                    var giveCreditData =
                                        from gp in db.GiveCredits
                                        group gp by 1 into g
                                        select new
                                        {
                                            totalCredit = g.Sum(x => x.AmountCharged),
                                        };
                                    var revenue = 0;
                                    if (giveCreditData.SingleOrDefault() != null)
                                    {
                                        revenue = giveCreditData.SingleOrDefault().totalCredit ?? 0;
                                    }

                                    var gymData =
                                                from gp in db.GymInvoices
                                                group gp by 1 into g
                                                select new
                                                {
                                                    uncollected = g.Where(x => x.IsCollected == false).Sum(x => x.AmountPaid),
                                                };

                                    var uncollectedPayment = 0;
                                    if (gymData.SingleOrDefault() != null)
                                    {
                                        uncollectedPayment = gymData.SingleOrDefault().uncollected;
                                    }

                                    var userData =
                                                from gp in db.Users
                                                group gp by 1 into g
                                                select new
                                                {
                                                    unused = g.Sum(x => x.Credits),

                                                };
                                    var unusedCredits = 0;
                                    if (userData.SingleOrDefault() != null)
                                    {
                                        unusedCredits = userData.SingleOrDefault().unused;
                                    }

                                    var knownprofit = revenue - costs - unusedCredits;


                                    message = "Summary: "
                                    + Environment.NewLine
                                    + "+ Revenue: "
                                    + formatDollar(revenue)
                                    + Environment.NewLine
                                    + "- Costs: "
                                    + formatDollar(costs)
                                    + Environment.NewLine
                                    + "- Unused Credits: "
                                    + formatDollar(unusedCredits)
                                    + Environment.NewLine
                                    + " = Known Profit: "
                                    + formatDollar(knownprofit)
                                    + Environment.NewLine
                                    + Environment.NewLine
                                    + "Pass Revenue: "
                                    + formatDollar(passRevenue)
                                    + Environment.NewLine
                                    + "Uncollected Gym Payments (in costs): "
                                    + formatDollar(uncollectedPayment)
                                    + Environment.NewLine
                                    + "Number of gym passes: "
                                    + gymPassCount
                                    + Environment.NewLine
                                    + "Number of users: "
                                    + userCount
                                    + Environment.NewLine
                                    + "Number of gyms: "
                                    + gymCount
                                    + Environment.NewLine;

                                }
                                sendMessageAsync(message, getResponseType(responseType), url);
                            }
                            else if (command.Equals("gympass"))
                            {
                                if (commandQueue.Count > 0 && commandQueue.Dequeue().Equals("summary"))
                                {
                                    sendMessageAsync("There is no information at this time, please tell james what data should go here!", getResponseType(false), url);
                                }


                            }
                            else if (command.Equals("user"))
                            {
                                if (commandQueue.Count > 0 && commandQueue.Dequeue().Equals("summary"))
                                {
                                    sendMessageAsync("There is no information at this time, please tell james what data should go here!", getResponseType(false), url);
                                }
                            }
                            else if (command.Equals("gym"))
                            {
                                if (commandQueue.Count > 0 && commandQueue.Dequeue().Equals("summary"))
                                {
                                    sendMessageAsync("There is no information at this time, please tell james what data should go here!", getResponseType(false), url);
                                }
                            }
                            else if (command.Equals("searchrequests"))
                            {
                                if (commandQueue.Count > 0 && commandQueue.Dequeue().Equals("summary"))
                                {
                                    sendMessageAsync("There is no information at this time, please tell james what data should go here!", getResponseType(false), url);
                                }
                            }
                            else if (command.Equals("givecredits"))
                            {
                                if (commandQueue.Count > 0 && commandQueue.Dequeue().Equals("summary"))
                                {
                                    sendMessageAsync("There is no information at this time, please tell james what data should go here!", getResponseType(false), url);
                                }
                            }
                        }
                    }


                    sendMessageAsync("Your command was invalid, type /pedal help to see valid commands.", getResponseType(false), url);

                
            }

            }
            catch (Exception exception)
            {
                Logs.LogsInsertError(exception);
            }



        }

        private async void runCloudflareMessage(string url)
        {
            var responseType = false;

            if (commandQueue.Count > 0)
            {
                var filter = commandQueue.Peek();
                if (filter.Equals("reset"))
                {
                    var message = "Still have to figure this out.";
                    sendMessageAsync(message, getResponseType(responseType), url);
                }
                else if (filter.Equals("development"))
                {
                    var message = "Still have to figure this out.";
                    sendMessageAsync(message, getResponseType(responseType), url);

                }
                else if (filter.Equals("devstatus"))
                {
                    System.Diagnostics.Process process = new System.Diagnostics.Process();
                    System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                    startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                    startInfo.FileName = "cmd.exe";
                    var arguments = "curl -X GET \"https://api.cloudflare.com/client/v4/zones/" +
                    Constants.cloudflareZone +
                    "/settings/development_mode\" " +
                    "-H \"X-Auth-Email: jamespsteinberg@gmail.com\" " +
                    "-H \"X-Auth-Key: " + Constants.cloudflareToken + "\" " +
                    "-H \"Content-Type: application/json\"";
                    startInfo.Arguments = arguments;
                    process.StartInfo = startInfo;
                    process.Start();
                    string message = process.StandardOutput.ReadToEnd();
                    //Wait for process to finish
                    process.WaitForExit();
                    sendMessageAsync(message, getResponseType(responseType), url);

                }
                else
                {
                    var message = "Command invalid, please type /pedal help for valid commands.";
                    sendMessageAsync(message, getResponseType(responseType), url);
                }
            }
            else
            {
                var message = "Command invalid, please type /pedal help for valid commands.";
                sendMessageAsync(message, getResponseType(responseType), url);
            }
           
        }

        private string formatDollarComparison(int from, int compare)
        {
            var percent = getPercent(from, compare);
            return formatDollar(from) + " -> " + formatDollar(compare) + " = " + percent + "%";
        }

        private string formatComparison(int from, int compare)
        {
            var percent = getPercent(from, compare);
            return from + " -> " + compare + " = " + percent + "%";
        }

        private int getPercent(int from, int compare)
        {
            if (compare == 0 || from == 0)
            {
                return 0;
            }
            else
            {
                var numerator = from - compare;
                var divide = numerator / (double)compare;
                var percent = (int)(divide * 100);
                return percent;
            }
        }


        private string formatDollar(int dollars)
        {
            decimal amount = dollars / 100M;
            return string.Format("{0:c}", amount);
        }

        private Tuple<DateTime?, DateTime?, DateTime?, DateTime?> fromFilter()
        {
            var fromDate = getDate();
            if (commandQueue.Count > 0)
            {
                var command = commandQueue.Peek();
                if (command != null && command.Equals("to"))
                {
                    command = commandQueue.Dequeue();
                    var toDate = getDate();

                    if (commandQueue.Count > 0)
                    {
                        var afterFromFilter = commandQueue.Peek();
                        if (afterFromFilter.Equals("compare"))
                        {
                            afterFromFilter = commandQueue.Dequeue();
                            afterFromFilter = commandQueue.Peek();
                            if (afterFromFilter.Equals("from"))
                            {
                                afterFromFilter = commandQueue.Dequeue();
                                Tuple<DateTime?, DateTime?> compareSide = compareSideDate();
                                return new Tuple<DateTime?, DateTime?, DateTime?, DateTime?>(fromDate, toDate, compareSide.Item1, compareSide.Item2);
                            }
                        }

                    }

                    return new Tuple<DateTime?, DateTime?, DateTime?, DateTime?>(fromDate, toDate, null, null);
                }
                else if (command != null && command.Equals("compare"))
                {
                    command = commandQueue.Dequeue();
                    command = commandQueue.Peek();
                    if (command.Equals("from"))
                    {
                        command = commandQueue.Dequeue();
                        Tuple<DateTime?, DateTime?> compareSide = compareSideDate();
                        return new Tuple<DateTime?, DateTime?, DateTime?, DateTime?>(fromDate, null, compareSide.Item1, compareSide.Item2);
                    }
                }
            }

            return new Tuple<DateTime?, DateTime?, DateTime?, DateTime?>(fromDate, null, null, null);
        }

        private Tuple<DateTime?, DateTime?> compareSideDate()
        {
            var fromDate = getDate();
            if (commandQueue.Count > 0)
            {
                var command = commandQueue.Peek();
                if (command != null && command.Equals("to"))
                {
                    command = commandQueue.Dequeue();
                    var toDate = getDate();

                    return new Tuple<DateTime?, DateTime?>(fromDate, toDate);
                }
            }

            return new Tuple<DateTime?, DateTime?>(fromDate, null);
        }

        private DateTime? getDate()
        {
            if (commandQueue.Count > 0)
            {
                int year = 2010;
                int month = 1;
                int day = 1;
                int hour = 0;
                int minute = 0;
                int second = 0;
                int millisecond = 0;

                var calendar = commandQueue.Dequeue();
                if (calendar.Equals("now"))
                {
                    return DateTime.Now;
                }

                string[] calendarDate = calendar.Split('/');


                if (calendarDate.ElementAtOrDefault(0) != null)
                {
                    int.TryParse(calendarDate[0], out year);
                }
                if (calendarDate.ElementAtOrDefault(1) != null)
                {
                    int.TryParse(calendarDate[1], out month);
                }
                if (calendarDate.ElementAtOrDefault(2) != null)
                {
                    int.TryParse(calendarDate[2], out day);
                }

                if (commandQueue.Count > 0)
                {
                    var potentialTimeDate = commandQueue.Peek();
                    if (!potentialTimeDate.Equals("show") && !potentialTimeDate.Equals("to") && !potentialTimeDate.Equals("compare"))
                    {
                        potentialTimeDate = commandQueue.Dequeue();
                        string[] timeDate = potentialTimeDate.Split(':');

                        if (timeDate.ElementAtOrDefault(0) != null)
                        {
                            int.TryParse(timeDate[0], out hour);
                        }
                        if (timeDate.ElementAtOrDefault(1) != null)
                        {
                            int.TryParse(timeDate[1], out minute);
                        }
                        if (timeDate.ElementAtOrDefault(2) != null)
                        {
                            int.TryParse(timeDate[2], out second);
                        }
                    }
                }
                return new DateTime(year, month, day, hour, minute, second, millisecond);
            }
            return null;
        }

        private string getResponseType(bool isPublic)
        {
            if (isPublic)
            {
                return "in_channel";
            }
            else
            {
                return "ephemeral";
            }
        }

        public bool IsReusable
        {
            get
            {
                return true;
            }
        }

        private void sendMessageAsync(string message, string responseType, string url)
        {
            if (payloadNotSent)
            {
                var payload = new
                {
                    response_type = responseType,
                    text = message,
                };

                var httpClient = new HttpClient();
                var serializedPayload = JsonConvert.SerializeObject(payload);
                var webHookUrl = new Uri(url);
                var response = httpClient.PostAsync(url,
                    new StringContent(serializedPayload, Encoding.UTF8, "application/json"));
                payloadNotSent = false;

                Logs.LogsInsertAction("Slack command success");

            }
        }



    }
}