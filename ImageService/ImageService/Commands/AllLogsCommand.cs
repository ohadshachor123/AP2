using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageService.Logging;
using Newtonsoft.Json;

namespace ImageService.Commands
{
    public class AllLogsCommand : ICommand
    {

        private IlogService logger;

        public AllLogsCommand(IlogService logger)
        {
            this.logger = logger;
        }
        public string Execute(string[] args, out bool result)
        {
            result = true;
            return JsonConvert.SerializeObject(this.logger.getAllLogs());
        }
    }
}
