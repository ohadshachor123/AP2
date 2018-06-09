using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.FilesModal
{
    public interface IImageModal
    {
        string MoveFile(string pathFrom, string year, string month, out bool result);
    }
}
