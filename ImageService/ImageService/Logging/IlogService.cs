using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Logging
{

    public interface IlogService
    {
        event EventHandler<MessageReceivedArgs> MessageRecieved;
        void Log(string message, MessageType type = MessageType.INFO);           // Logging the Message
        ObservableCollection<MessageReceivedArgs> getAllLogs();
    }
}

