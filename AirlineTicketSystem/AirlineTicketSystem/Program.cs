using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace AirlineTicketSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            Airline Airline1 = new Airline("JetAirways", 1, 10);
            Airline Airline2 = new Airline("BritishAir", 2, 10);
            Airline Airline3 = new Airline("Luftansa", 3, 10);

            Thread AirlineThread1 = new Thread(new ThreadStart(Airline1.PricingModel));
            Thread AirlineThread2 = new Thread(new ThreadStart(Airline2.PricingModel));
            Thread AirlineThread3 = new Thread(new ThreadStart(Airline3.PricingModel));

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
            
            
            Airline1.pricecut += new priceCutEvent(Airline.AirlineFun);
            Airline2.pricecut += new priceCutEvent(Airline.AirlineFun);
            Airline3.pricecut += new priceCutEvent(Airline.AirlineFun);


            OrderProcessing.OrderConfirmation += new OrderConfirmationEvent(Agency1.EventHandler_ConfirmationStatus);
            OrderProcessing.OrderConfirmation += new OrderConfirmationEvent(Agency2.EventHandler_ConfirmationStatus);
            OrderProcessing.OrderConfirmation += new OrderConfirmationEvent(Agency3.EventHandler_ConfirmationStatus);
            OrderProcessing.OrderConfirmation += new OrderConfirmationEvent(Agency4.EventHandler_ConfirmationStatus);
            OrderProcessing.OrderConfirmation += new OrderConfirmationEvent(Agency5.EventHandler_ConfirmationStatus);
            OrderProcessing.OrderConfirmation += new OrderConfirmationEvent(Agency6.EventHandler_ConfirmationStatus);

 /*           Airline1.pricecut += new priceCutEvent(Agency2.EventHandler);
            Airline1.pricecut += new priceCutEvent(Agency3.EventHandler);
            Airline2.pricecut += new priceCutEvent(Agency1.EventHandler);
            Airline2.pricecut += new priceCutEvent(Agency3.EventHandler);
            Airline2.pricecut += new priceCutEvent(Agency4.EventHandler);
            Airline3.pricecut += new priceCutEvent(Agency1.EventHandler);
            Airline3.pricecut += new priceCutEvent(Agency2.EventHandler);
            Airline3.pricecut += new priceCutEvent(Agency4.EventHandler);
  */
          


            AirlineThread1.Start();
            AirlineThread2.Start();
            AirlineThread3.Start();

            AgencyThread1.Start();
            AgencyThread2.Start();
            AgencyThread3.Start();
            AgencyThread4.Start();
            AgencyThread5.Start();
            AgencyThread6.Start();

            AirlineThread1.Join();
            AirlineThread2.Join();
            AirlineThread3.Join();

            AgencyThread1.Join();
            AgencyThread2.Join();
            AgencyThread3.Join();
            AgencyThread4.Join();

            Console.ReadLine();
        }
    }
}
