using System;
using System.Collections.Generic;
using System.Text;

namespace CSE598_A2_E_Ticketing
{
    public class Order
    {
        private static readonly double taxRate = 0.05;
        private static readonly double locationCharge = 3;
        private string senderID; // identity of the sender. Will use the thread name for this
        private int cardNo; // credit card number 
        private int amount; // number of tickets to order
        private double unitPrice; // price per ticket

        private static readonly int maxCreditCardNumber = 7000;
        private static readonly int minCreditCardNumber = 5000;

        public Order(string senderID, int cardNo, int amount, double unitPrice)
        {
            this.senderID = senderID;
            this.cardNo = cardNo;
            this.amount = amount;
            this.unitPrice = unitPrice;
        }

        public bool verifyCreditCard()
        {
            return (cardNo >= minCreditCardNumber) && (cardNo <= maxCreditCardNumber);
        }

        private double calculateTotalPrice()
        {
            return (1 + taxRate) * amount * unitPrice + locationCharge;
        }

        public void processOrder()
        {
            Console.WriteLine("Processing order from TicketingAgency{0} for {1} tickets for ${2} per ticket", senderID, amount, unitPrice);
            if (verifyCreditCard())
            {
                Console.WriteLine("Credit card number for TicketAgency{0}'s order is valid! Completing order", senderID);
            }
            else
            {
                Console.WriteLine("Credit card number for TicketAgency{0}'s order is invalid, canceling order", senderID);
                return;
            }

            Console.WriteLine("TicketingAgency{0}'s order for {1} tickets has been completed! Order Total: ${2:.00}", senderID, amount, this.calculateTotalPrice());
            

        }



    }
}
