using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Commands
{
    public class GetConfigCommand : ICommand
    {
        public string Execute(string[] args, out bool result)
        {
            JObject jsonObject = new JObject();
            jsonObject["Output"] = ConfigurationManager.AppSettings["OutputDir"];
            jsonObject["Source"] = ConfigurationManager.AppSettings["SourceName"];
            jsonObject["Log"] = ConfigurationManager.AppSettings["LogName"];            jsonObject["Thumbnail"] = ConfigurationManager.AppSettings["ThumbnailSize"];
            jsonObject["Handler"] = ConfigurationManager.AppSettings["Handler"];
            result = true;
            return jsonObject.ToString();
        }
    }
}
