namespace UniversalGym
{
    using System;
    using System.Web.Security;

    public class MembershipHelper
    {
       
        // Creates User/Gym
        public static Guid createMembership(string email, string password, string role)
        {
            var username = getUsernameFromEmail(email, role);
            var user = Membership.CreateUser(username, password, email);
            Roles.AddUserToRole(username, Constants.UserRole);
            var guid = Guid.Parse(user.ProviderUserKey.ToString());
            return guid;
        }

        private static string getUsernameFromEmail(string email, string role)
        {
            if (role.Equals(Constants.GymRole))
            {
                return email + "_gym";
            }
            else if (role.Equals(Constants.UserRole))
            {
                return email + "_user";
            }
            else
            {
                return email;
            }

        }

        // Check for duplicate users/gyms
        public static Boolean emailAlreadyExists(string email, string role)
        {
            var username = getUsernameFromEmail(email, role);

            var listOfCurrentUsers = Roles.FindUsersInRole(role, username);
            if (listOfCurrentUsers.Length > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // validates user exists
        public static Boolean userExists(string email, string password, string role)
        {
            var username = getUsernameFromEmail(email, role);
            bool userExistsAtAll = Membership.ValidateUser(username, password);
            bool userExistsInThatRole = Roles.IsUserInRole(username, role);

            return userExistsAtAll && userExistsAtAll;

        }

        // retreive valid user/gym
        public static MembershipUser getUser(string email, string role)
        {
            var username = getUsernameFromEmail(email, role);
            return Membership.GetUser(username);
        }
        


    }


}

