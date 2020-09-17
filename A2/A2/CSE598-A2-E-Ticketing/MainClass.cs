using System;
using System.Threading;

namespace CSE598_A2_E_Ticketing
{
    class MainClass
    {

        private static readonly int N = 5; // number of ticketing agencies
        static void Main(string[] args)
        {
            MultiCellBuffer mcb = new MultiCellBuffer();
            Park park = new Park(mcb); // create park with multicell buffer
            Thread parkThread = new Thread(new ThreadStart(park.runParkThread));
            parkThread.Name = 1.ToString();
            parkThread.Start();


            for(int i = 0; i < N; i++) //N ticketing agency threads will be created
            {
                TicketingAgency agencyThread = new TicketingAgency(park, mcb, i+1); // create each ticketAgency with same buffer as the park
                Park.priceCut += new priceCutEvent(agencyThread.ticketsOnSale); // add the new Ticketing agency to the priceCutEvent
                Thread thread = new Thread(new ThreadStart(agencyThread.priceChecker));
                thread.Name = (i + 1).ToString();
                thread.Start();
            }
        }
    }
}
