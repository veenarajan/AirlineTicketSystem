using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace AirlineTicketSystem
{
    /// <summary>
    /// Class provides methods to get and set values in the Multi cell buffer 
    /// </summary>
    static class MultiCellBuffer
    {
        public static string[] buffer = new string[3];
        static bool[] IsEmpty = new bool[3];
        
        static MultiCellBuffer()
        {
            for (int i = 0; i < 3; i++)
            {
                buffer[i] = null;
                IsEmpty[i] = true;
            }
            
        }
        private static Mutex mutex_lock = new Mutex();

        private static Semaphore sem_full = new Semaphore(3, 3);
        private static Semaphore sem_empty = new Semaphore(0, 3);

        public static void setOneCell(string ObjectString)
        {
            if (sem_full.WaitOne(300))
            {
                for (int i = 0; i < 3; i++)
                {
                    mutex_lock.WaitOne();
                    if (IsEmpty[i] == true)
                    {
                        buffer[i] = ObjectString;
                        IsEmpty[i] = false;
                        //Console.WriteLine("Wposition is {0}", i);
                        mutex_lock.ReleaseMutex();
                        sem_empty.Release();
                        return;
                    }
                    mutex_lock.ReleaseMutex();
                    
                }
            }
        }

        public static string getOneCell(string AirlineName)
        {
            if (sem_empty.WaitOne(300))
            {
                for (int i = 0; i < 3; i++)
                {
                    if (IsEmpty[i] == false)
                    {
                        string ObjectString = buffer[i];
                        Decoder decode_test = new Decoder();
                        OrderClass obj = new OrderClass();
                        if (ObjectString != null)
                        {
                            obj = decode_test.decryptString(ObjectString);
                            //Console.WriteLine("string name passed is {0} actual name is {1}", AirlineName, obj.get_receiverId());
                            if (obj.get_receiverId() == AirlineName)
                            {
                                //Console.WriteLine("Readposition is {0}\n", i);
                                mutex_lock.WaitOne();
                                buffer[i] = null;
                                IsEmpty[i] = true;
                                mutex_lock.ReleaseMutex();
                                sem_full.Release();
                                return ObjectString;
                            }
                        }

                    }
                }
                sem_empty.Release();
            }
            return null;

        }
    }
}