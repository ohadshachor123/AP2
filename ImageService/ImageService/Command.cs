using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService
{
    class Command
    {
        private int id;
        private string[] args;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        public string[] Args
        {
            get { return args; }
            set { args = value; }
        }

        public Command(int id, string[] args)
        {
            this.id = id;
            this.args = args;
        }
    }
}
