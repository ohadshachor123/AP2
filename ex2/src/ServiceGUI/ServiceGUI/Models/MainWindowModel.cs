using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceGUI.Communication;
namespace ServiceGUI.Models
{
    public class MainWindowModel: AbstractModel, IMainWindowModel
    {
        private IClient client;
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
            client = ClientSingelton.GetInstance();
            if(client.IsRunning())
                IsConnected = true;
            else
                IsConnected = false;
        }

    }
}
