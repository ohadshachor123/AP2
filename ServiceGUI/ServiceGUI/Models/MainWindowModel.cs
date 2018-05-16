using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceGUI.Models
{
    public class MainWindowModel: AbstractModel, IMainWindowModel
    {
        private bool _isConnected;
        public bool IsConnected
        {
            get { return _isConnected; }
            set {
                _isConnected = value;
                NotifyPropertyChanged("IsConnected");
            }
        }

        public MainWindowModel()
        {
            _isConnected = true;
        }
    }
}
