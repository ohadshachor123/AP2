﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Logging
{
    public class MessageReceivedArgs : EventArgs
    {
        public MessageReceivedArgs(MessageType status, string message)
        {
            Message = message;
            Status = status;
        }
        public MessageType Status { get; set; }
        public string Message { get; set; }
    }
}
