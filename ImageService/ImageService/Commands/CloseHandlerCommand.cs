using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Commands
{
    public class CloseHandlerCommand : ICommand
    {
        public string Execute(string[] Args, out bool result)
        {
            result = true;
            return "";
        }
    }
}
