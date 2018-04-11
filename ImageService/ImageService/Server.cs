using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageService.Commands;
using ImageService.Handlers;
using ImageService.Logging;
namespace ImageService
{
    public class Server
    {
        private IlogService logger;
        private IController controller;

        public event EventHandler<Commands.CommandReceivedArgs> ReceiveCommand;
        public event EventHandler<EventArgs> CloseAll;
        public Server(IlogService logger,IController controller)
        {
            this.logger = logger;
            this.controller = controller;
        }

        public void AddPath(string path)
        {
            logger.Log("Adding path: " + path, MessageType.WARNING);
            IHandler handler = new DirectoryHandler(logger, controller);
            handler.DirectoryClose += CloseHandler;
            ReceiveCommand += handler.OnCommandRecieved;
            CloseAll += handler.closeMe;
            handler.StartHandleDirectory(path);
        }

        private void CloseHandler(object sender, DirectoryCloseArgs args)
        {

        }

        public void Stop()
        {
            CloseAll?.Invoke(this, null);
        }
        public void PerformCommand(Commands.CommandReceivedArgs args)
        {
            ReceiveCommand?.Invoke(this, args);
        }
    }
}

