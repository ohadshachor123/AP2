using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Logging
{
    public class LoggingService : IlogService
    {
        public event EventHandler<MessageReceivedArgs> MessageRecieved;
        private ObservableCollection<MessageReceivedArgs> allLogs;
        public LoggingService()
        {
            allLogs = new ObservableCollection<MessageReceivedArgs>();
        }
        public void Log(string message, MessageType type = MessageType.INFO)
        {

            MessageReceivedArgs args = new MessageReceivedArgs(type, message);
            allLogs.Add(args);
            MessageRecieved?.Invoke(this, args);
        }

        public ObservableCollection<MessageReceivedArgs> getAllLogs()
        {
            return allLogs;
        }
    }
}
