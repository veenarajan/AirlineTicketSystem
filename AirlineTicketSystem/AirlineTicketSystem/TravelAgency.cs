using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Threading;


namespace AirlineTicketSystem
{
    class TravelAgency
    {
        private int id;
        private string name;
        private int thread_price;
        private bool flag;
        private string AirlineName;
        private string encodedOrderString;
        static Random rnd = new Random();
        private static int i = 0;
        Stopwatch stopWatch = new Stopwatch();
        TimeSpan timeSpan = new TimeSpan();

        public TravelAgency(string n, int my_id)
        {
            this.name = n;
            this.id = my_id;
            flag = false;
        }

        private OrderClass generate_obj()
        {
            OrderClass obj = new OrderClass();
            timeSpan = TimeSpan.Zero;

            // string[] s = { "JetAirways", "BritishAir", "Luftansa" };
            obj.set_senderId(name);
            obj.set_cardNo(rnd.Next(1000, 9000));
            // obj.set_receiverId(s[(i++)%3]);
            obj.set_receiverId(AirlineName);
            obj.set_amount(rnd.Next(1, 5));
            obj.set_unitprice(thread_price);
            obj.set_totalamount(0);
            obj.set_confirmationstatus(false);

            return obj;
        }

        public void TravelAgencyFun()
        {
            while (true)
            {
                if (!(Program.isAliveStatus[0] || Program.isAliveStatus[1] || Program.isAliveStatus[2]))
                {
                   break;
                }
                
                if (flag)
                {
                    OrderClass obj = new OrderClass();
                    obj = generate_obj();
                    //Console.WriteLine("Travel Agency Sender id {0} card no{1} receiver id {2} Amount {3} Unitprice {4}", obj.get_senderId(), obj.get_cardNo(), obj.get_receiverId(), obj.get_amount(), obj.get_unitprice());
                    // Console.WriteLine("Travel Agency Sender id {0} receiver id {1} Amount {2} Unitprice {3}", obj.get_senderId(), obj.get_receiverId(), obj.get_amount(), obj.get_unitprice());

                    Encoder encode_test = new Encoder();
                    encodedOrderString = encode_test.encryptString(obj);
                    // Console.WriteLine("Encoded string is {0}\n", encodedOrderString);
                    stopWatch.Start();

                    MultiCellBuffer.setOneCell(encodedOrderString);

                    flag = false;
                }
            }
            Console.WriteLine("THE THREAD WILL NOW ABORT !! ALL DONE !! " + name);
            //Thread.CurrentThread.Abort();

        }

        public void EventHandler(int p, string n) //Event handler call back function
        {

            //Console.WriteLine("Inside event handler {0} {1}", n, p);
            thread_price = p;
            AirlineName = n;
            flag = true;
        }

        public void EventHandler_ConfirmationStatus()
        {
            string Cell = ConfirmationBuffer.getOneCell();
            Decoder decode_test = new Decoder();
            OrderClass obj = new OrderClass();
            obj = decode_test.decryptString(Cell);
            //Console.WriteLine("\n\n\n\n");
            /*Console.WriteLine("***************  ORDER STATUS  ***************");
            Console.WriteLine("Travel {0} placed an order of {1} ticktes to Airline {2}", obj.get_senderId(), obj.get_amount(), obj.get_receiverId());
            Console.WriteLine("The price of each ticket is ${0}", obj.get_unitprice());
            Console.WriteLine("The total amount charged is : ${0}", obj.get_totalamount());
            Console.WriteLine("The order is charged to card number  : {0}", obj.get_cardNo());
            
            if(obj.get_confirmationstatus())
            {
                Console.WriteLine("Congratulations !! the order has been processed. Have a safe Flight !");
            }
            else
            {
                Console.WriteLine("Your order could not be processed at this time !");
            }
            */
            stopWatch.Stop();
            timeSpan = stopWatch.Elapsed;
            /*Console.WriteLine("Total time taken for order confirmation: " + timeSpan.Milliseconds);

            Console.WriteLine("**************************************************");
            Console.WriteLine("\n\n\n\n");*/
            //Console.WriteLine("THE THREAD WILL NOW ABORT !! ALL DONE !!  travel agency event handler " + Thread.CurrentThread.Name);
            //Thread.CurrentThread.Abort();

        }
    }
}
