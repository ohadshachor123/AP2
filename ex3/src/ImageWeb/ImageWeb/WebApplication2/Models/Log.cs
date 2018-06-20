using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public enum MessageType : int
    {
        INFO,
        FAIL,
        WARNING,
    }
    public class Log
    {

        public Log(String type, String content)
        {
            Type = type;
            Content = content;
        }
        [Required]
        [DataType(DataType.Text)]
        public String Type { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Content { get; set; }
    }
}