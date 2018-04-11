using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Logging
{
    public class LoggingService : IlogService
    {
        public event EventHandler<MessageReceivedArgs> MessageRecieved;
        public void Log(string message, MessageType type)
        {
            MessageReceivedArgs args = new MessageReceivedArgs(type, message);
            MessageRecieved?.Invoke(this, args);

        }
    }
}
