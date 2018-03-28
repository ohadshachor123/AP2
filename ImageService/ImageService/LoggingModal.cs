using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService
{
    class LoggingModal : Ilogging
    {
        public event EventHandler<string> eventLogger;
        
        public void Log(string toLog)
        {
            eventLogger?.Invoke(this, toLog);
        }
    }
}
