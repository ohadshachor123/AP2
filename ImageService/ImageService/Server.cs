using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService
{
    class Server
    {
        private Ilogging logger;
        private Dictionary<string, Ihandler> handlers;
        public Server() {
            handlers = new Dictionary<string, Ihandler>();
            logger = new LoggingModal();
        }

        public void ListenToPath(string path) {
            handlers.Add(path, new DirectoryHandler(path));
        }

        public void Close()
        {
            foreach(KeyValuePair<string, Ihandler> entry in handlers)
            {
                entry.Value.Close();
            }
        }

        public void SendCommand(Command command, string path)
        {
            handlers[path].PerformCommand(command);
        }
    }
}
