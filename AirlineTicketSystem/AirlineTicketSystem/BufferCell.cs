using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineTicketSystem
{
    class BufferCell
    {
        private string data;

        public void set_string(string order_class_string)
        {
            data = order_class_string;
        }

        public string get_string()
        {
            return data;
        }
    }
}
