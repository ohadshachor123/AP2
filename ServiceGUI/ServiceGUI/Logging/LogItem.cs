using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceGUI.Logging
{
    public class LogItem
    {
        public LogEnum Status { get; set; }
        public String Message { get; set; }

        public LogItem() { }
        public LogItem(LogEnum type, String message)
        {
            Status = type;
            Message = message;
        }

        public static LogItem FromJson(String serialized)
        {
            LogItem ans = new LogItem();
            JObject logObj = JObject.Parse(serialized);
            int x = (int)(logObj["Type"]);
            ans.Status = (LogEnum)x;
            ans.Message = (String)logObj["Message"];            return ans;

        }

    }
}
