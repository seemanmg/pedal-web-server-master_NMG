using UniversalGym.Data;


namespace UniversalGym.Credit
{
    public class CreditUse
    {
        public UniversalGymEntities Db { get; set; }

        public CreditUse(UniversalGymEntities db)
        {
            this.Db = db;
        }

        public int UseCredit(User user, int price)
        {
            var priceAtInt = price;
            var creditsUsed = 0;
            if (price > user.Credits)
            {
                creditsUsed = user.Credits;
                user.Credits = 0;
            }
            else
            {
                creditsUsed = priceAtInt;
                user.Credits -= priceAtInt;
            }
            Db.SaveChanges();
            return creditsUsed;
        }

    }
}
