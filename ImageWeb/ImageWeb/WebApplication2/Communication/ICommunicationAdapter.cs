using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication2.Models;

namespace WebApplication2.Communication
{
    interface ICommunicationAdapter
    {
        bool IsOnline { get; set; }
        string OutputDir { get; set; }
        string SourceName { get; set; }
        string LogName { get; set; }
        int ThumbnailSize { get; set; }
        List<Log> Logs { get; set; }
        List<String> Handlers { get; set; }
    }
}
