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
        private static Regex r = new Regex(":");

        public static DateTime GetDateTakenFromImage(string path)
        {
            try
            {
                using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                using (Image myImage = Image.FromStream(fs, false, false))
                {
                    PropertyItem propItem = myImage.GetPropertyItem(36867);
                    string dateTaken = r.Replace(Encoding.UTF8.GetString(propItem.Value), "-", 2);
                    return DateTime.Parse(dateTaken);
                }
            } catch (Exception e)
            {
                return File.GetCreationTime(path);
            }
        }

        public static Image CreateThumbnailFromPath(string path, int size)
        {
            Image image = Image.FromFile(path);
            Image thumbnail = image.GetThumbnailImage(size, size, () => false, IntPtr.Zero);
            image.Dispose();

            return thumbnail;
        }
    }
}
