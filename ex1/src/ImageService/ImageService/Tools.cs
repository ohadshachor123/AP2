using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text.RegularExpressions;
using System.IO;

namespace ImageService
{
    public class Tools
    {
        // Some useful functions for this project.
        private static Regex expression = new Regex(":");

        // Gets the taken time from an image(or the creation time if it does not exist).
        public static DateTime GetDateTakenFromImage(string path)
        {
            try
            {
                using (FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read))
                using (Image myImage = Image.FromStream(stream, false, false))
                {
                    PropertyItem property = myImage.GetPropertyItem(36867);
                    string dateTaken = expression.Replace(Encoding.UTF8.GetString(property.Value), "-", 2);
                    return DateTime.Parse(dateTaken);
                }
            } catch (Exception e)
            {
                return File.GetCreationTime(path);
            }
        }

        // Creates a thumbnail from an image. The caller needs to remember to dispose of the thumbnail.
        public static Image CreateThumbnailFromPath(string path, int size)
        {
            Image image = Image.FromFile(path);
            Image thumbnail = image.GetThumbnailImage(size, size, () => false, IntPtr.Zero);
            image.Dispose();
            return thumbnail;
        }
    }
}
