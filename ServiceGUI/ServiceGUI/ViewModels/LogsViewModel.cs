using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceGUI.Models;
namespace ServiceGUI.ViewModels
{
    class LogsViewModel : AbstractViewModel
    {
        private ILogsModel model;


        public LogsViewModel(ILogsModel model) : base(model)
        {
            this.model = model;
        }
    }
}
