using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace ImageService
{
    class ImageModal : IImageServiceModal
    {
        public void CreateFolder(string path)
        {
            if(!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
        public void MoveFile(string from, string to)
        {
            File.Move(from, to);
        }
    }
}
