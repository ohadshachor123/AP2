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

        // ReceiveCommand is the event that is raised whenever the server gets a command from the service.
        public event EventHandler<Commands.CommandReceivedArgs> ReceiveCommand;
        // Close all- is the event that is raised whenever the image service stops.
        public event EventHandler<EventArgs> CloseAll;
        public Server(IlogService logger,IController controller)
        {
            this.logger = logger;
            this.controller = controller;
        }

        public void AddPath(string path)
        {
            logger.Log("Handling a new path :" + path);
            IHandler handler = new DirectoryHandler(logger, controller);
            ReceiveCommand += handler.OnCommandRecieved;
            CloseAll += handler.CloseMe;
            // Exception might be thrown if the path is invalid.
            try { 
                handler.StartHandleDirectory(path);
            } catch(Exception e)
            {  
                logger.Log("Could not handle the path: " + path + ". Error: " + e.Message);
                handler.CloseMe(null,null);
                CloseAll -= handler.CloseMe;
            }
        }

        public void PerformCommand(Commands.CommandReceivedArgs args)
        {
            ReceiveCommand?.Invoke(this, args);
        }

        public void Stop()
        {
            CloseAll?.Invoke(this, null);
        }
    }
}

