using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Handlers
{
    public interface IHandler
    {
        event EventHandler<DirectoryCloseArgs> DirectoryClose;              // The Event That Notifies that the Directory is being closed
        void StartHandleDirectory(string dirPath);             // The Function Recieves the directory to Handle
        void OnCommandRecieved(object sender, Commands.CommandReceivedArgs args);     // The Event that will be activated upon new Command
        void closeMe(object sender, EventArgs args);
    }
}
