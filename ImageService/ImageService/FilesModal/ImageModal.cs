using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Drawing;
using System.Threading;
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


        private void CreateOutputSubfolder(string path)
        {
            if (!Directory.Exists(outputFolder + "\\" + path)) {
                Directory.CreateDirectory(outputFolder + "\\" + path);
            }
        }

        private void CreateThumbnailSubfolder(string path)
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

                CreateThumbnailSubfolder(year);
                CreateThumbnailSubfolder(year + "\\" + month);
                
                string name = Path.GetFileNameWithoutExtension(pathFrom);
                string extension = Path.GetExtension(pathFrom);

                Thread.Sleep(500);

                Image thumbnail = Tools.CreateThumbnailFromPath(pathFrom, thumbnailSize);
                thumbnail.Save(outputFolder + "\\" + "thumbnails" + "\\" + year + "\\" + month + "\\" + name + ".thumb");
                thumbnail.Dispose();

                string pathTo = outputFolder + "\\" + year + "\\" + month + "\\" + name + extension;
                File.Move(pathFrom, pathTo);
                result = true;
                return pathTo;
            }
            catch (Exception e)
            {
                result = false;
                return e.StackTrace + e.Message;
            }
        }

    }
}

