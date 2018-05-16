using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceGUI.Models
{
    public class LogsModel : ILogsModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public LogsModel()
        {

        }
        public void NotifyPropertyChanged(string propName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
