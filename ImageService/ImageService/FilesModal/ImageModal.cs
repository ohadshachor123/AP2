using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.MemoryMappedFiles;
namespace ImageService.FilesModal
{
    public class ImageModal:IImageModal
    {
        private string outputFolder;            // The Output Folder
        private int thumbnailSize;              // The Size Of The Thumbnail Size

        public ImageModal(string outputPath, int size)
        {
            outputFolder = outputPath;
            thumbnailSize = size;
        }


        public void CreateOutputSubfolder(string path)
        {
            if (!Directory.Exists(outputFolder + "\\" + path)) {
                Directory.CreateDirectory(outputFolder + "\\" + path);
            }
        }

        public void CreateThumbnailSubfolder(string path)
        {
            CreateOutputSubfolder("thumbnails");
            CreateOutputSubfolder("thumbnails\\" + path);
        }
        public string MoveFile(string pathFrom, string year, string month, out bool result)
        {
            try
            {
                CreateOutputSubfolder(year);
                CreateOutputSubfolder(year + "\\" + month);

                string pathTo = outputFolder + "\\" + year + "\\" + month;
                File.Move(pathFrom, pathTo);
                result = true;
                return pathTo;
            }
            catch
            {
                result = false;
                return null;
            }
        }

    }
}

