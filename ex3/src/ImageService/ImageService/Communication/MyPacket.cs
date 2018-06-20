using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageService.Commands;
namespace ImageService.Communication
{
    public class MyPacket
    {
        public CommandEnum Type { get; set; }
        public String[] Args { get; set; }

        public MyPacket(CommandEnum type, String[] args)
        {
            Type = type;
            Args = args;
        }
    }
}
