using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace AirlineTicketSystem
{
    static class MultiCellBuffer
    {
        static BufferCell[] buffer = new BufferCell[]{
            new BufferCell(),
            new BufferCell(),
            new BufferCell()
        };

        private static int wposition = 0;
        private static int rposition = 0;

        private static Mutex mutex_lock = new Mutex();

        private static Semaphore sem_full = new Semaphore(3, 3);
        private static Semaphore sem_empty = new Semaphore(0, 3);

        public static void setOneCell(string ObjectString)
        {

            // lock
            // semaphore lock
            sem_full.WaitOne();
            // multi thread lock
            mutex_lock.WaitOne();
            //Console.WriteLine("write position is {0}\n", wposition);
            buffer[wposition].set_string(ObjectString);

            wposition = (wposition + 1) % 3;

            // un lock
            mutex_lock.ReleaseMutex();
            sem_empty.Release();

        }

        public static string getOneCell(string AirlineName)
        {
            // lock
            // semaphore lock

            // multi thread lock

            for (int i = 0; i < 3; i++)
            {
                string ObjectString = buffer[rposition].get_string();
                Decoder decode_test = new Decoder();
                OrderClass obj = new OrderClass();
                if (ObjectString != null)
                {
                    obj = decode_test.decryptString(ObjectString);
                    //Console.WriteLine("string name passed is {0} actual name is {1}", AirlineName, obj.get_receiverId());
                    if (obj.get_receiverId() == AirlineName)
                    {
                        sem_empty.WaitOne();
                        //Console.WriteLine("Readposition is {0}\n", rposition);
                        mutex_lock.WaitOne();
                        rposition = (rposition + 1) % 3;
                        mutex_lock.ReleaseMutex();
                        sem_full.Release();
                        return ObjectString;
                    }
                    else
                    {
                        mutex_lock.WaitOne();
                        //Console.WriteLine("Readposition is {0}\n", rposition);
                        rposition = (rposition + 1) % 3;
                        mutex_lock.ReleaseMutex();

                    }
                }

            }
            return null;
            // unlock

        }

    }
}
