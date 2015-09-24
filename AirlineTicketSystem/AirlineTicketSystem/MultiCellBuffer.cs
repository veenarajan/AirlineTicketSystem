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

        private static string ObjectString;

        public static void setOneCell(string ObjectString_encoded)
        {
            try
            {
                sem_full.WaitOne();
                mutex_lock.WaitOne();

                //  Console.WriteLine("write position is {0}\n", wposition);
                buffer[wposition].set_string(ObjectString_encoded);
                wposition = (wposition + 1) % 3;

                mutex_lock.ReleaseMutex();
                sem_empty.Release();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception occured while setting MultiBufferCell" + e.Message.ToString());
            }
            

        }

        public static string getOneCell()
        {
            try
            {
                sem_empty.WaitOne();
                mutex_lock.WaitOne();

                //Console.WriteLine("Readposition is {0}\n", rposition);

                ObjectString = buffer[rposition].get_string();
                rposition = (rposition + 1) % 3;

                mutex_lock.ReleaseMutex();
                sem_full.Release();
            }
            catch(Exception e)
            {
                Console.WriteLine("Exception occured while getting MultiBufferCell" + e.Message.ToString());
            }
            return ObjectString;
        }
    }
}
