namespace UniversalGym.Shared
{
    //using Absolute.Extensions;
    using System;
    using System.Linq;
    using UniversalGym.Data;

    public class UserCode
    {
        public static string GenerateDistinctCode(UniversalGymEntities db, int length = 6)
        {
            var usedCodes = db.Users.Select(s => s.ReferalUrl);
            var newCode = GenInviteCode(length);
            var taken = usedCodes.Any(a => a == newCode);
            while (taken)
            {
                newCode = GenInviteCode(length);
                taken = usedCodes.Any(a => a == newCode);
            }
            return newCode;
        }
        private static string GenInviteCode(int length = 10)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            var result = new string(
                Enumerable.Repeat(chars, length)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());
            return result;
        }


    }
}

