using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService
{
    interface IImageModel
    {
        void CreateFolder(string path);
        void MoveFile(string from, string to);
    }
}
