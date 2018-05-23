using ServiceGUI.Logging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceGUI.Communication;
using Newtonsoft.Json;

namespace ServiceGUI.Models
{
    public class LogsModel : AbstractModel, ILogsModel
    {
        private IClient client;

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

            Logs = new ObservableCollection<LogItem>();
            client = ClientSingelton.GetInstance();
            client.NewPacketReceived += PacketsHandler;
            client.SendPacket(new MyPacket(CommandEnum.AllLogs, null));
        }

        private void PacketsHandler(MyPacket packet)
        {
            switch (packet.Type)
            {
                case CommandEnum.AllLogs:
                    AddListOfLogs(packet.Args[0]);
                    break;
                case CommandEnum.ReceiveNewLog:
                    AddOneLog(packet.Args[0]);
                    break;
            }
        }

        private void AddListOfLogs(String logs)
        {
            try
            {
                ObservableCollection<LogItem> logList = JsonConvert.DeserializeObject<ObservableCollection<LogItem>>(logs);
                Logs = logList;
            } catch (Exception e)
            {
                Console.WriteLine("Could not parse the log list: " + e.Message);
            }
        }

        private void AddOneLog(String log)
        {
            _logs.Add(LogItem.FromJson(log));
            NotifyPropertyChanged("Logs");
        }
    }
}
