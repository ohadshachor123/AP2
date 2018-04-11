using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using ImageService.Commands;
namespace ImageService.Handlers
{
    public class DirectoryHandler : IHandler
    {
        public static string[] filters = { ".png", ".jpg", ".bmp", ".gif" };

        public event EventHandler<DirectoryCloseArgs> DirectoryClose;
        private Commands.IController controller;       
        private Logging.IlogService logging;
        private FileSystemWatcher dirWatcher;
        private string path;

        public DirectoryHandler(Logging.IlogService logging, Commands.IController controller)
        {
            this.logging = logging;
            this.controller = controller;
        }
        public void StartHandleDirectory(string path)
        {
            logging.Log("Starting the watcher " + path, Logging.MessageType.FAIL);
            this.path = path;
            dirWatcher = new FileSystemWatcher(path);
            dirWatcher.EnableRaisingEvents = true;
            dirWatcher.Created += FileCreated;
        }
        
        /** The function to call whenever a file is create in the server **/
        public void FileCreated(object sender, FileSystemEventArgs args)
        {
            logging.Log("NEW FILE    " + args.FullPath, Logging.MessageType.INFO);

            string path = args.FullPath;
            string extention = Path.GetExtension(path);
            if(filters.Contains(extention))
            {
                logging.Log("THE FILE MATCHES.....", Logging.MessageType.INFO);
                DateTime date = Tools.GetDateTakenFromImage(path);
                logging.Log("THE DATE IS GIVEN", Logging.MessageType.INFO);
                int year = date.Year;
                int month = date.Month;
                string[] commandArgs = { path, year.ToString(), month.ToString() };
                logging.Log("Executing the new file command", Logging.MessageType.FAIL);
                controller.ExecuteCommand((int)CommandEnum.NewFileCommand, commandArgs, out bool result);
                if (result)
                {
                    logging.Log("SUCCES!", Logging.MessageType.INFO);
                } else
                {
                    logging.Log("Fail!", Logging.MessageType.FAIL);
                }
            }

        }

        /** Perform a command which the server has sent **/
        public void OnCommandRecieved(object sender, CommandReceivedArgs args)
        {
            logging.Log("Received a command from the server", Logging.MessageType.INFO);
            controller.ExecuteCommand(args.CommandID, args.Args, out bool result);
        }

        /** The function to call whenever we want to close the FileSystemWatcher **/
        public void closeMe(object sender, EventArgs args)
        {
            logging.Log("Closing the handler right now....", Logging.MessageType.FAIL);
            dirWatcher.EnableRaisingEvents = false;
        }

    }
}
