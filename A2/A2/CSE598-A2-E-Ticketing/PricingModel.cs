using System;
using System.Collections.Generic;
using System.Text;

namespace CSE598_A2_E_Ticketing
{
    public class PricingModel
    {
        Random rand;
        int currentPrice;
        int previousPrice;
        

        public PricingModel()
        {
            rand = new Random();
            currentPrice = updatePrice();
            previousPrice = 0;
           
        }

        public int getCurrentPrice()
        {
            return currentPrice;
        }

        public int getPreviousPrice()
        {
            return previousPrice;
        }

        public bool hasPriceDropped()
        {
            return currentPrice < previousPrice;
        }

        public int updatePrice()
        {
            previousPrice = currentPrice;
            currentPrice = rand.Next(80, 300);
            return currentPrice;

        }
    }
}
