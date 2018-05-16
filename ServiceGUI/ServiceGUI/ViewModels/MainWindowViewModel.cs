using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceGUI.Models;
namespace ServiceGUI.ViewModels
{
    class MainWindowViewModel : AbstractViewModel
    {
        private IMainWindowModel model;
        public bool VM_IsConnected
        {
            get { return model.IsConnected; }
            set { model.IsConnected = value; }
        }

        public MainWindowViewModel(IMainWindowModel model) : base(model)
        {
            this.model = model;
        }
    }
}
