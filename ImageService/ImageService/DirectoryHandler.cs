﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService
{
    class DirectoryHandler : Ihandler

    {
        private Ilogging logger;
        private string path;
        public DirectoryHandler(string path)
        {
            this.path = path;
        }

        public void Close()
        {

        }

        public void PerformCommand(Command command)
        {

        }
    }
}
