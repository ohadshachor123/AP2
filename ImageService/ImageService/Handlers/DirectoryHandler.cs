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

        public event EventHandler<String> selfCloser;
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
            this.path = path;
            if(!Directory.Exists(path))
            {
                // Could throw an exception to whoever called the function.
                Directory.CreateDirectory(path);
            }
            dirWatcher = new FileSystemWatcher(path);
            dirWatcher.EnableRaisingEvents = true;
            dirWatcher.Created += FileCreated;
        }
        
        //The function to call whenever the watcher raises a CreatedFile event
        private void FileCreated(object sender, FileSystemEventArgs args)
        {
            string sourcePath = args.FullPath;
            string extention = Path.GetExtension(sourcePath);
            // We are only listening to specific extensions.
            if(filters.Contains(extention))
            {
                logging.Log("New file identified: " + sourcePath);
                DateTime date = Tools.GetDateTakenFromImage(sourcePath);
                int year = date.Year;
                int month = date.Month;
                // Call the command that transfers the files.
                string[] commandArgs = { sourcePath, year.ToString(), month.ToString() };
                string resultDescription = controller.ExecuteCommand((int)CommandEnum.NewFileCommand, commandArgs, out bool result);
                if (result)
                {
                    logging.Log("Successfully moved the file to: " + resultDescription);
                } else
                {
                    string toLog = "Failed moving " + Path.GetFileName(sourcePath) + " to the new path\n";
                    toLog += "The error was: " + resultDescription;
                    logging.Log(toLog, Logging.MessageType.FAIL);
                }
            }
        }

        // Perform a command which the server has sent
        public void OnCommandRecieved(object sender, CommandReceivedArgs args)
        {
            if (args.CommandID == (int)CommandEnum.CloseCommand)
            {
                if (this.path == args.Args[0])
                {
                    CloseMe(this, null);
                }
            }
            else
            {
                /**
                logging.Log("The handler received a new command, ID: " + args.CommandID.ToString());
                controller.ExecuteCommand(args.CommandID, args.Args, out bool result);
                if (result)
                    logging.Log("Successfully performed the command");
                else
                    logging.Log("Fail to perform the command"); **/
            }
        }

        //The function we call whenever we want to close the FileSystemWatcher
        public void CloseMe(object sender, EventArgs args)
        {
            logging.Log("Closing the handler: " + this.path, Logging.MessageType.WARNING);
            dirWatcher.EnableRaisingEvents = false;
            dirWatcher.Dispose();
            selfCloser?.Invoke(this, this.path);
        }

    }
}
