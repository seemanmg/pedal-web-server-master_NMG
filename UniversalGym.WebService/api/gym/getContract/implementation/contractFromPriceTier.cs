using System;
using UniversalGym.Requests;
using UniversalGym.Responses;

namespace UniversalGym.Gym
{
    public class contractFromPriceTier
    {
        public static ContractResponse contractFromPriceTierImplementation(ContractFromPriceTierRequest request)
        {
            var gymName = "YOUR GYM";
            var price = 250;
            if (request.gymName != null)
            {
                gymName = request.gymName;
            }

            if (request.priceTier != null)
            {
                price = Constants.returnGymPay(request.priceTier);
            }
            
            var date = DateTime.Now.ToShortDateString();
            return new ContractResponse { gymName = gymName, price = price, date = date, success = true };


        }
    }
}