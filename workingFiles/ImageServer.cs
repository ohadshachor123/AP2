using ImageService.Controller;
using ImageService.Controller.Handlers;
using ImageService.Infrastructure.Enums;
using ImageService.Logging;
using ImageService.Modal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Server
{
    public class ImageServer
    {
        private ILoggingService logger;
        private IImageController controller;
        public event EventHandler<CommandRecievedEventArgs> CommandRecieved;
        public ImageServer(ILoggingService logger, IImageController controller)
        {
            this.logger = logger;
            this.controller = controller;
        }

        public void AddPath(string path)
        {
            IDirectoryHandler handler = new DirectoryHandler(logger, controller);
            handler.DirectoryClose += CloseHandler;
            CommandRecieved += handler.OnCommandRecieved;
            handler.StartHandleDirectory(path);

        }

        private void CloseHandler(object sender, DirectoryCloseEventArgs args)
        {

        }

        public void PerformCommand(CommandRecievedEventArgs args)
        {
            CommandRecieved?.Invoke(this, args);
        }
    }
}
