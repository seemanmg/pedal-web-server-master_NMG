using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using UniversalGym.API;
using UniversalGym.Requests;
using UniversalGym.StripeHelper;

namespace Pedal.Tests
{
    [TestClass]
    public class UnitTest1
    {
        public void TestMethod1()
        {
            //var studioID = -99;
            var id = Guid.NewGuid();
            //var email = "pedal@pedal.com";
            //var fist = "Bob";
            //var last = "Belcher";
            var password = Guid.NewGuid().ToString();
        }



        [TestMethod]
        public void TestPurchasePass()
        {

            // precondition /// the user passed in is the friend of someone else. 
            // need to make sure that there is a GiveCredit with this use as the friend
            //token of this user
            var token = new Guid("a9b854bc-2e12-4c46-be5a-f052a59733ad");
            var request = new PurchaseDayPassRequest
            {
                authToken = token.ToString(),
                gymId = 2258
            };
            var service = new GymService(); 
            var item = service.PurchaseGym(request);
            Assert.AreEqual(200, item.status);
            //to check
            // both users has +5 credit 
            //Give Credit was changed. 
        }



        


        public void PopulateTestFund()
        {
            var customer = new StripeCustomer().CreateCustomer("tester@testing.tested", "Billy Grim",
                "4000 0000 0000 0077", "10", "2017", "555");
            new StripeCharge().ChargeStripeAccount(customer, 50000, "populating test account");
        }

        

    }
}
