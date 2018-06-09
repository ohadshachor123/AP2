using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class Photo
    {
        
        public Photo(String path, String thumbnailPath, String name, String year, String month)
        {
            Path = path;
            ThumbnailPath = thumbnailPath;
            Name = name;
            Year = year;
            Month = month;
        }
        [Required]
        [DataType(DataType.Text)]
        public String Path { get; set; }
        [Required]
        [DataType(DataType.Text)]
        public String ThumbnailPath { get; set; }
        [Required]
        [DataType(DataType.Text)]
        public String Name { get; set; }
        [Required]
        [DataType(DataType.Text)]
        public String Year { get; set; }
        [Required]
        [DataType(DataType.Text)]
        public string Month { get; set; }
    }
}