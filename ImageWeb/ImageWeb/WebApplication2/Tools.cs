using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using WebApplication2.Models;

namespace WebApplication2
{
    public class Tools
    {
        private static string[] filters = { ".png", ".jpg", ".bmp", ".gif" };
        public static int CountImagesInDir(string dir)
        { 
            int count = 0;
            foreach(string year in Directory.GetDirectories(dir))
            {
                foreach (string month in Directory.GetDirectories(year))
                {
                    foreach(string file in Directory.GetFiles(month))
                    {
                        if (filters.Contains(Path.GetExtension(file)))
                            count++;
                    }
                }
            }
            return count;
        }

        public static List<Photo> PhotosInDir(string dir)
        {
            List<Photo> ans = new List<Photo>();
            foreach (string yearPath in Directory.GetDirectories(dir))
            {
                foreach (string monthPath in Directory.GetDirectories(yearPath))
                {
                    string year = new DirectoryInfo(yearPath).Name;
                    foreach (string file in Directory.GetFiles(monthPath))
                    {
                        string month = new DirectoryInfo(monthPath).Name;
                        if (filters.Contains(Path.GetExtension(file)))
                        {
                            string thumbnail = dir + "\\Thumbnail\\" + year + "\\" + month + "\\" + Path.GetFileName(file);
                            ans.Add(new Photo(file, thumbnail, Path.GetFileName(file), year, month));
                        }
                    }
                }
            }
            return ans;
        }
    }
}