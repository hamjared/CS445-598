using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace CSE598_A2_E_Ticketing
{
    class TicketingAgency
    {
        Park park;
        int id;
        MultiCellBuffer mcb;
        private int creditCardNumber;

        public TicketingAgency(Park park, MultiCellBuffer mcb, int id)
        {
            this.park = park;
            this.mcb = mcb;
            this.id = id;
            creditCardNumber = new Random().Next(5000, 7000);
        }
        public void priceChecker()
        {
            Random rand = new Random();
            while (park.getNumPriceCuts() < Park.getMaxPriceCuts())
            {
                Thread.Sleep(rand.Next(500, 1000)); // sleep for between  and 1 second
                int curTicketPrice = park.getTicketPrice();
                Console.WriteLine("TicketingAgency{0} can buy Park tickets for: ${1} each", Thread.CurrentThread.Name, curTicketPrice);
                if(rand.NextDouble() < 0.2)
                {
                    int numTicketsToOrder = rand.Next(10, 20);
                    this.orderTickets(numTicketsToOrder, curTicketPrice);
                    
                }
            }

            Console.WriteLine("Park has had 20 price cut events, TicketingAgency{0} exiting...", Thread.CurrentThread.Name);
        }

        public void ticketsOnSale(int price)
        {
            Random rand = new Random();
            Console.WriteLine("Tickets on Sale for ${0}!", price);
            if(rand.NextDouble() < 0.7)
            {
                int numTicketsToOrder = rand.Next(50, 400);
                this.orderTickets(numTicketsToOrder, price);
            }
        }

        public void orderTickets(int numTickets, int price)
        {
            Console.WriteLine("TicketingAgency{0} sent order for {1} tickets at ${2} per ticket to the park", this.id, numTickets, price);
            Order order = new Order(id.ToString(), creditCardNumber, numTickets, price);
            mcb.setOneCell(order);
        }
    }
}
