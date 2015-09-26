using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;


namespace AirlineTicketSystem
{
    static class ConfirmationBuffer
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
            // Console.WriteLine("write position is {0}\n", wposition);
            buffer[wposition].set_string(ObjectString);
            wposition = (wposition + 1) % 3;

            // un lock
            mutex_lock.ReleaseMutex();
            sem_empty.Release();
        }

        public static string getOneCell()
        {
            // lock
            // semaphore lock

            // multi thread lock
            sem_empty.WaitOne();
            mutex_lock.WaitOne();
            //Console.WriteLine("Readposition is {0}\n", rposition);
            string ObjectString = buffer[rposition].get_string();
            rposition = (rposition + 1) % 3;

            // unlock
            mutex_lock.ReleaseMutex();
            sem_full.Release();
            return ObjectString;
        }
    }
}
