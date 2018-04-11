using ImageService.Infrastructure;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace ImageService.Modal
{
    public class ImageServiceModal : IImageServiceModal
    {
        private string m_OutputFolder;            // The Output Folder
        private int m_thumbnailSize;              // The Size Of The Thumbnail Size


        public ImageServiceModal(string outputPath, int size)
        {
            m_OutputFolder = outputPath;
            m_thumbnailSize = size;
        }


        public void OutputSubfolder(string path)
        {
            Directory.CreateDirectory(m_OutputFolder + "\\" + path);
        }
        public void ThumbnailSubfolder(string path)
        {
            if(!Directory.Exists(m_OutputFolder + "\\thumbnails"))
            {
                Directory.CreateDirectory(m_OutputFolder + "\\thumbnails");
            }
            Directory.CreateDirectory(m_OutputFolder + "\\thumbnails\\" + path);
        }
        public string MoveFile(string pathFrom,string subPathTo, out bool result)
        {
            try
            {
                File.Move(pathFrom, m_OutputFolder + "\\" + subPathTo);
                result = true;
                return m_OutputFolder + "\\" + subPathTo;
            }
            catch
            {
                result = false;
                return null;
            }
        }

    }
}
