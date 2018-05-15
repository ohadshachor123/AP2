using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Commands
{
    public interface IController
    {
        // Translates the commandID into the correct command and executes it.
        string ExecuteCommand(int commandID, string[] args, out bool result);
    }
}
