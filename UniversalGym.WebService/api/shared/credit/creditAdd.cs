using System;
using UniversalGym.Data;
using UniversalGym.Slack;


namespace UniversalGym.Credit
{
    public class creditAdd
    {
        public UniversalGymEntities Db { get; set; }

        public creditAdd(UniversalGymEntities db)
        {
            this.Db = db;
        }

        public int AddCredit(User toUser, User fromUser, int creditToAdd, int charged)
        {
            toUser.Credits += creditToAdd;
            var item = new GiveCredit()
            {
                AmountToGiveNewUser = creditToAdd,
                DateTime = DateTime.UtcNow,
                UserIdToGiveCredits = toUser.UserId,
                AmountCharged = charged,
                
            };
            if (fromUser != null)
            {
                item.UserIdWhoGiveCredits = fromUser.UserId;
                // If we are giving a referal bonus eventually
                //item.AmountToGiveReferer = 0;
            }
            Db.GiveCredits.Add(item);
            Db.SaveChanges();

            var giveCreditText = "Credit"
            + Environment.NewLine
            + "To: "
            + toUser.Email
            + Environment.NewLine;

            if (fromUser != null)
            {
                giveCreditText = giveCreditText
                + "From: "
                + fromUser.Email
                +Environment.NewLine;
            }

            giveCreditText = giveCreditText
            + "$"
            + item.AmountToGiveNewUser;

            SlackHelper.sendGiveCreditChannel(giveCreditText);

            return toUser.Credits;
        }
    }
}
