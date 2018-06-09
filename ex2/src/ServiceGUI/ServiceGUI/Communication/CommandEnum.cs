using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceGUI.Communication
{
    public enum CommandEnum:int
    {
        NewFile,
        GetConfig,
        AllLogs,
        CloseHandler,
        ReceiveNewLog,
        Exit

    }
}
