﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Commands
{
    public interface IController
    {
        string ExecuteCommand(int commandID, string[] args, out bool result);
    }
}
