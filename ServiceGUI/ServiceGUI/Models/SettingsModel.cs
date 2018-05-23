using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using ServiceGUI.Communication;
namespace ServiceGUI.Models
{
    public class SettingsModel : AbstractModel, ISettingsModel
    {

        private IClient client;
        private ObservableCollection<String> _handlersList;
        private string _outputDirectory;
        private string _sourceName;
        private string _logName;
        private int _thumbnailSize;

        public ObservableCollection<String> HandlersList
        {
            get{ return _handlersList;}
            set
            {
                _handlersList = value;
                NotifyPropertyChanged("HandlersList");
            }
        }

        public String OutputDirectory
        {
            get { return _outputDirectory; }
            set { _outputDirectory = value; NotifyPropertyChanged("OutputDirectory"); }
        }

        public String SourceName
        {
            get { return _sourceName;}
            set
            {
                _sourceName = value;
                NotifyPropertyChanged("SourceName");
            }
        }

        public String LogName
        {
            get { return _logName; }
            set
            {
                _logName = value;
                NotifyPropertyChanged("LogName");
            }
        }

        public int ThumbnailSize
        {
            get { return _thumbnailSize; }
            set
            {
                _thumbnailSize = value;
                NotifyPropertyChanged("ThumbnailSize");
            }
        }
        public SettingsModel()
        {
            HandlersList = new ObservableCollection<String>();
            client = ClientSingelton.GetInstance();
            client.NewPacketReceived += PacketsHandler;
            client.SendPacket(new MyPacket(CommandEnum.GetConfig, null));
        }

        public void PacketsHandler(MyPacket packet)
        {
            switch(packet.Type)
            {
                case CommandEnum.GetConfig:
                    LoadConfigurations(packet.Args[0]);
                    break;
                case CommandEnum.CloseHandler:
                    CloseHandler(packet.Args[0]);
                    break;
            }
        }

        private void LoadConfigurations(String config)
        {
            try
            {
                JObject jsonObject = JObject.Parse(config);
                OutputDirectory = (string)jsonObject["Output"];
                SourceName = (string)jsonObject["Source"];
                LogName = (string)jsonObject["Log"];
                ThumbnailSize = int.Parse((string)jsonObject["Thumbnail"]);
                string handlers = (string)jsonObject["Handler"];
                string[] lst = handlers.Split(';');
                foreach (string handler in lst)
                {
                    AddHandler(handler);
                }
            } catch(Exception e)
            {
                Console.WriteLine("Could not parse config: " + e.Message);
            }
        }
        
        private void AddHandler(String handler)
        {
            _handlersList.Add(handler);
            NotifyPropertyChanged("HandlersList");
        }
        private void CloseHandler(String handler)
        {

        }

    }
}
