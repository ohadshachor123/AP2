using ImageService.Modal;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageService.Infrastructure;
using ImageService.Infrastructure.Enums;
using ImageService.Logging;
using ImageService.Logging.Modal;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Drawing.Imaging;
namespace ImageService.Controller.Handlers
{
    public class DirectoryHandler : IDirectoryHandler
    {
        private IImageController m_controller;              // The Image Processing Controller
        private ILoggingService m_logging;
        private FileSystemWatcher[] m_dirWatcher;             // The Watcher of the Dir
        private string m_path;                              // The Path of directory

        public event EventHandler<DirectoryCloseEventArgs> DirectoryClose;              // The Event That Notifies that the Directory is being closed

        public DirectoryHandler(ILoggingService logging, IImageController controller)
        {
            m_logging = logging;
            m_controller = controller;
        }
        public void StartHandleDirectory(string path)
        {

            m_path = path;
            m_dirWatcher = new FileSystemWatcher[4];
            string[] filters = { "*.png", "*.jpg", "*.gif", "*.bmp" };
            for (int i = 0; i <4;i++)
            {
                m_dirWatcher[i] = new FileSystemWatcher(path, filters[i]);
                m_dirWatcher[i].Created += new FileSystemEventHandler(FileCreated);
            }
        }

        public void FileCreated(object sender, FileSystemEventArgs args)
        {
            string path = args.FullPath;
            DateTime date = GetDateTakenFromImage(path);
            int year = date.Year;
            int month = date.Month;
            string[] commandArgs = { path, year.ToString(), month.ToString() };
            m_controller.ExecuteCommand((int)CommandEnum.NewFileCommand, commandArgs, out bool result);
        }

        public void OnCommandRecieved(object sender, CommandRecievedEventArgs args)
        {
            m_controller.ExecuteCommand(args.CommandID, args.Args, out bool result);
        }
        private static Regex r = new Regex(":");

        public static DateTime GetDateTakenFromImage(string path)
        {
            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            using (Image myImage = Image.FromStream(fs, false, false))
            {
                PropertyItem propItem = myImage.GetPropertyItem(36867);
                string dateTaken = r.Replace(Encoding.UTF8.GetString(propItem.Value), "-", 2);
                return DateTime.Parse(dateTaken);
            }
        }

    }
}
