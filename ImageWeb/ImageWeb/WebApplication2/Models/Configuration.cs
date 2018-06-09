using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class Configuration
    {

        public Configuration(string outputDir, string sourceName, string logName, int thumb, List<String> handlers)
        {
            OutputDir = outputDir;
            Source = sourceName;
            LogName = logName;
            ThumbSize = thumb;
            Handlers = handlers;
        }
        [Required]
        [DataType(DataType.Text)]
        public String OutputDir { get; set; }
        [Required]
        [DataType(DataType.Text)]
        public string Source { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string LogName { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public int ThumbSize { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Handlers:")]
        public List<String> Handlers { get; set; }
    }
}