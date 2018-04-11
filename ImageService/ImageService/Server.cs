using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageService.Commands;
namespace ImageService
{
    public class Server
    {
        private Logging.IlogService logger;
        private IController controller;

        public event EventHandler<Commands.CommandReceivedArgs> ReceiveCommand;
        public Server(Logging.IlogService logger, IController controller)
        {
            this.logger = logger;
            this.controller = controller;
        }

        public void AddPath(string path)
        {
            IDirectoryHandler handler = new DirectoryHandler(logger, controller);
            handler.DirectoryClose += CloseHandler;
            ReceiveCommand += handler.OnCommandRecieved;
            handler.StartHandleDirectory(path);

        }

        private void CloseHandler(object sender, DirectoryCloseEventArgs args)
        {

        }

        public void PerformCommand(Commands.CommandReceivedArgs args)
        {
            ReceiveCommand?.Invoke(this, args);
        }
    }
}
}
