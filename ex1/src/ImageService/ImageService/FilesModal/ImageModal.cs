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
        private const int SLEEP_TIME = 350;
        private string outputFolder;            // The Output folder path
        private int thumbnailSize;              // The Size Of The Thumbnail Size

        public ImageModal(string outputPath, int size)
        {
            outputFolder = outputPath;
            if (!Directory.Exists(outputPath))
            {
                DirectoryInfo dir = Directory.CreateDirectory(outputPath);
                dir.Attributes = FileAttributes.Directory | FileAttributes.Hidden;
            }
            thumbnailSize = size;
        }

        // Creates a subfolder the output file, according to the subpath given.
        private void CreateOutputSubfolder(string subPath)
        {
            if (!Directory.Exists(outputFolder + "\\" + subPath)) {
                Directory.CreateDirectory(outputFolder + "\\" + subPath);
            }
        }

        // Creates a subfolder the thumbnails file, according to the subpath given.
        private void CreateThumbnailSubfolder(string path)
        {
            // This will also create a thumbnails folder if it doesn't exist.
            CreateOutputSubfolder("thumbnails");
            CreateOutputSubfolder("thumbnails\\" + path);
        }
        public string MoveFile(string pathFrom, string year, string month, out bool result)
        {
            try
            {
                // Creating the relevant subfolders if they don't exist.
                CreateOutputSubfolder(year);
                CreateOutputSubfolder(year + "\\" + month);
                CreateThumbnailSubfolder(year);
                CreateThumbnailSubfolder(year + "\\" + month);

                // Sleeping here resloves many bugs where the file is not found(even if it is there).
                Thread.Sleep(SLEEP_TIME);

                // Hangling the thumbnail
                string name = Path.GetFileNameWithoutExtension(pathFrom);
                string extension = Path.GetExtension(pathFrom);
                Image thumbnail = Tools.CreateThumbnailFromPath(pathFrom, thumbnailSize);
                thumbnail.Save(outputFolder + "\\" + "thumbnails" + "\\" + year + "\\" + month + "\\" + name + extension);
                thumbnail.Dispose();

                // Moving the file.
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

