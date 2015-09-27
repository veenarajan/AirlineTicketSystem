/*
 * Team void main - Uma and Veena
 * No of Airline threads - 3
 * No of Agency threads - 6
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace AirlineTicketSystem
{
    /// <summary>
    /// Main class creating Airline and agency threads, starts threads and instantiate objects.
    /// </summary>
    class Program
    {
        public static Thread AirlineThread1, AirlineThread2, AirlineThread3;
        public static bool[] isAliveStatus = { true, true, true };
        static void Main(string[] args)
        {
            Console.WriteLine("Airline Ticketing System ");
            Console.WriteLine("Press Enter to begin and Exit......\n\n");
            Console.ReadLine();

            // Register Airline
            Airline Airline1 = new Airline("JetAirways", 1, 50);
            Airline Airline2 = new Airline("BritishAir", 2, 50);
            Airline Airline3 = new Airline("Luftansa", 3, 50);

            AirlineThread1 = new Thread(new ThreadStart(Airline1.PricingModel));
            AirlineThread2 = new Thread(new ThreadStart(Airline2.PricingModel));
            AirlineThread3 = new Thread(new ThreadStart(Airline3.PricingModel));

            Thread A1 = new Thread(new ThreadStart(Airline1.AirlineFun));
            Thread A2 = new Thread(new ThreadStart(Airline2.AirlineFun));
            Thread A3 = new Thread(new ThreadStart(Airline3.AirlineFun));


            TravelAgency Agency1 = new TravelAgency("Agency1", 1);
            TravelAgency Agency2 = new TravelAgency("Agency2", 2);
            TravelAgency Agency3 = new TravelAgency("Agency3", 3);
            TravelAgency Agency4 = new TravelAgency("Agency4", 4);
            TravelAgency Agency5 = new TravelAgency("Agency5", 5);
            TravelAgency Agency6 = new TravelAgency("Agency6", 6);

            Thread AgencyThread1 = new Thread(new ThreadStart(Agency1.TravelAgencyFun));
            Thread AgencyThread2 = new Thread(new ThreadStart(Agency2.TravelAgencyFun));
            Thread AgencyThread3 = new Thread(new ThreadStart(Agency3.TravelAgencyFun));
            Thread AgencyThread4 = new Thread(new ThreadStart(Agency4.TravelAgencyFun));
            Thread AgencyThread5 = new Thread(new ThreadStart(Agency5.TravelAgencyFun));
            Thread AgencyThread6 = new Thread(new ThreadStart(Agency6.TravelAgencyFun));

            Airline1.pricecut += new priceCutEvent(Agency1.EventHandler);
            Airline2.pricecut += new priceCutEvent(Agency2.EventHandler);
            Airline3.pricecut += new priceCutEvent(Agency3.EventHandler);
            Airline1.pricecut += new priceCutEvent(Agency4.EventHandler);
            Airline2.pricecut += new priceCutEvent(Agency5.EventHandler);
            Airline3.pricecut += new priceCutEvent(Agency6.EventHandler);


            OrderProcessing.OrderConfirmation += new OrderConfirmationEvent(Agency1.EventHandler_ConfirmationStatus);
            OrderProcessing.OrderConfirmation += new OrderConfirmationEvent(Agency2.EventHandler_ConfirmationStatus);
            OrderProcessing.OrderConfirmation += new OrderConfirmationEvent(Agency3.EventHandler_ConfirmationStatus);
            OrderProcessing.OrderConfirmation += new OrderConfirmationEvent(Agency4.EventHandler_ConfirmationStatus);
            OrderProcessing.OrderConfirmation += new OrderConfirmationEvent(Agency5.EventHandler_ConfirmationStatus);
            OrderProcessing.OrderConfirmation += new OrderConfirmationEvent(Agency6.EventHandler_ConfirmationStatus);

         
            /*
            Airline1.pricecut += new priceCutEvent(Airline1.AirlineFun);
            Airline2.pricecut += new priceCutEvent(Airline2.AirlineFun);
            Airline3.pricecut += new priceCutEvent(Airline3.AirlineFun);
            */

            AirlineThread1.Start();
            AirlineThread2.Start();
            AirlineThread3.Start();

            A1.Start();
            A2.Start();
            A3.Start();


            AgencyThread1.Start();
            AgencyThread2.Start();
            AgencyThread3.Start();
            AgencyThread4.Start();
            AgencyThread5.Start();
            AgencyThread6.Start();


            AirlineThread1.Join();
            AirlineThread2.Join();
            AirlineThread3.Join();

            A1.Join();
            A2.Join();
            A3.Join();
            

            AgencyThread1.Join();
            AgencyThread2.Join();
            AgencyThread3.Join();
            AgencyThread4.Join();
            AgencyThread5.Join();
            AgencyThread6.Join();

            Console.WriteLine("Execution Successful. Press Enter to Exit !!");

            Console.ReadLine();
        }
    }
}
