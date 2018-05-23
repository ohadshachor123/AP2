using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceGUI.Models
{
    interface ISettingsModel:INotifyPropertyChanged
    {
        ObservableCollection<String> HandlersList {get;set; }
        String OutputDirectory { get; set; }
        String SourceName { get; set; }
        String LogName { get; set; }
        int ThumbnailSize { get; set; }

        void RemoveHandler(string handler);
    }
}
