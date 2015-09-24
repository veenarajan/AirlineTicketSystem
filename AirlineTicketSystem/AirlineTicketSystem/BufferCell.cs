using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineTicketSystem
{
    /// <summary>
    /// This Class is the implememtation of each cell in the multibuffer cell.
    /// provides 2 methods: one to set the value of the cell and the other to retrive it's value.
    /// </summary>
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
