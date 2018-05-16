using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceGUI.Logging
{
    public class LogItem
    {
        public LogEnum Type { get; set; }
        public String Message { get; set; }

        public LogItem(LogEnum type, String message)
        {
            Type = type;
            Message = message;
        }
    }
}
