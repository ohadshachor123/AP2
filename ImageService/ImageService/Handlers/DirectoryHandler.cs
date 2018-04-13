﻿using System;
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
            dirWatcher = new FileSystemWatcher(path);
            dirWatcher.EnableRaisingEvents = true;
            dirWatcher.Created += FileCreated;
        }
        
        /** The function to call whenever a file is create in the server **/
        private void FileCreated(object sender, FileSystemEventArgs args)
        {
            string sourcePath = args.FullPath;
            string extention = Path.GetExtension(sourcePath);
            if(filters.Contains(extention))
            {
                logging.Log("New file identified: " + sourcePath);
                DateTime date = Tools.GetDateTakenFromImage(sourcePath);
                int year = date.Year;
                int month = date.Month;
                string[] commandArgs = { sourcePath, year.ToString(), month.ToString() };
                string newPath = controller.ExecuteCommand((int)CommandEnum.NewFileCommand, commandArgs, out bool result);
                if (result)
                {
                    logging.Log("Successfully moved the file to: " + newPath);
                } else
                {
                    logging.Log("Failed moving " + Path.GetFileName(sourcePath) + " to the new path", Logging.MessageType.FAIL);
                    logging.Log("Error : " + newPath, Logging.MessageType.FAIL);
                }
            }
        }

        /** Perform a command which the server has sent **/
        public void OnCommandRecieved(object sender, CommandReceivedArgs args)
        {
            logging.Log("The handler received a new command, ID: " + args.CommandID.ToString());
            controller.ExecuteCommand(args.CommandID, args.Args, out bool result);
            if (result)
                logging.Log("Successfully performed the command");
            else
                logging.Log("Fail to perform the command");
        }

        /** The function to call whenever we want to close the FileSystemWatcher **/
        public void CloseMe(object sender, EventArgs args)
        {
            logging.Log("Closing the handler: " + this.path, Logging.MessageType.WARNING);
            dirWatcher.EnableRaisingEvents = false;
        }

    }
}