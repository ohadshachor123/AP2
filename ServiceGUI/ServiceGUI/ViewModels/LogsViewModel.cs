using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceGUI.Models;
using ServiceGUI.Logging;
namespace ServiceGUI.ViewModels
{
    class LogsViewModel : AbstractViewModel
    {
        private ILogsModel model;

        public ObservableCollection<LogItem> VM_Logs {
            get { return this.model.Logs; }
            set { this.model.Logs = value; }
        }

        public LogsViewModel(ILogsModel model) : base(model)
        {
            this.model = model;
        }
    }
}
