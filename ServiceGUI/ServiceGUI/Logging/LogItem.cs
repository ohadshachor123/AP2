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
        public LogEnum Type { get; set; }
        public String Message { get; set; }

        public LogItem() { }
        public LogItem(LogEnum type, String message)
        {
            Type = type;
            Message = message;
        }

        public static LogItem FromJson(String serialized)
        {
            LogItem ans = new LogItem();
            JObject logObj = JObject.Parse(serialized);
            int x = (int)(logObj["Type"]);
            ans.Type = (LogEnum)x;
            ans.Message = (String)logObj["Message"];            return ans;

        }

    }
}
