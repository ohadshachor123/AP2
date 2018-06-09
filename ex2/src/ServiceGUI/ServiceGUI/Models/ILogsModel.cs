using ServiceGUI.Logging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceGUI.Models
{
    public interface ILogsModel : INotifyPropertyChanged
    {
        ObservableCollection<LogItem> Logs { get; set; }
    }
}
