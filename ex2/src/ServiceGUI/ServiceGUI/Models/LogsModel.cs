﻿using ServiceGUI.Logging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceGUI.Communication;
using Newtonsoft.Json;
using System.Windows;

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

        // This function will be called whenever a packet is received, and parsed accordingly.
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

        // Adding a list of json-ed logs. the list itself is also encoded into a string via json.
        private void AddListOfLogs(String logs)
        {
            try
            {
                ObservableCollection<LogItem> logList = JsonConvert.DeserializeObject<ObservableCollection<LogItem>>(logs);
                foreach(LogItem item in logList)
                {
                    AddOneLog(item);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Could not parse the log list: " + e.Message);
            }


        }
        
        // Adding one log, represented as a log item class.
        private void AddOneLog(LogItem log)
        {
            Application.Current.Dispatcher.Invoke(new Action(() => {
                _logs.Add(log);
                NotifyPropertyChanged("Logs");
            }));
        }

        // Adding one log, represented as a json string.
        private void AddOneLog(String log)
        {
            try
            {
                AddOneLog(JsonConvert.DeserializeObject<LogItem>(log));
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed adding a new log " + e.Message);
            }

        }
    }
}
