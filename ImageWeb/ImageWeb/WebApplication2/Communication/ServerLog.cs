using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Communication
{
    public class ServerLog
    {
        public ServerLog(int type, String content)
        {
            Status = type;
            Message = content;
        }
        public int Status { get; set; }
        public String Message { get; set; }
    }
}