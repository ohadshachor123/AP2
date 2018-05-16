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
    public class LogsModel : AbstractModel, ILogsModel
    {

        private ObservableCollection<LogItem> _logs;
        public ObservableCollection<LogItem> Logs
        {
            get { return _logs; }
            set
            {
                _logs = value;
                NotifyPropertyChanged("Logs");
            }
        }
        public LogsModel()
        {
            ObservableCollection<LogItem> lst = new ObservableCollection<LogItem>();
            lst.Add(new LogItem(LogEnum.ERROR, "Testing error log"));
            lst.Add(new LogItem(LogEnum.INFO, "Testing Info log"));
            lst.Add(new LogItem(LogEnum.WARNING, "Testing Warning log"));
            Logs = lst;

        }
    }
}
