using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Commands
{
    public enum CommandEnum : int
    {
        NewFileCommand,
        GetConfig,
        AllLogs,
        CloseHandler,
        ReceiveNewLog,
        CloseCommand
    }
}
