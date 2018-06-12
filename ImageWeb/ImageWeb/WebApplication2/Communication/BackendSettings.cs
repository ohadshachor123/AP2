using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ServiceGUI.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using WebApplication2.Models;

namespace WebApplication2.Communication
{
    // This is the singleton which holds all the settings we received from the service.
    public class BackendSettings : ICommunicationAdapter
    {
        private static BackendSettings instance = null;

        public static BackendSettings GetInstance()
        {
            if (instance == null) { instance = new BackendSettings(); }
            return instance;
        }
        private BackendSettings()
        {
            IClient client = ClientSingelton.GetInstance();
            if (client.IsRunning())
            {
                IsOnline = true;
                Logs = new List<Log>();
                Handlers = new List<String>();
                client.NewPacketReceived += HandlePacket;
                // Request the config and handlers.
                client.SendPacket(new MyPacket(CommandEnum.GetConfig, null));
                client.SendPacket(new MyPacket(CommandEnum.AllLogs, null));
            }
        }

        // Handle the packets received from the service.
        private void HandlePacket(MyPacket packet) {
            if (packet.Type == CommandEnum.AllLogs)
            {
                try
                {
                    List<ServerLog> logList = JsonConvert.DeserializeObject<List<ServerLog>>(packet.Args[0]);
                    foreach (ServerLog item in logList)
                        AddOneLog(item);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Could not parse the log list: " + e.Message);
                }
            } else if (packet.Type == CommandEnum.ReceiveNewLog)
            {
                try
                {
                    AddOneLog(JsonConvert.DeserializeObject<ServerLog>(packet.Args[0]));
                }
                catch (Exception e)
                {
                    Console.WriteLine("Failed adding a new log " + e.Message);
                }
            } else if (packet.Type == CommandEnum.GetConfig)
            {
                try
                {
                    JObject jsonObject = JObject.Parse(packet.Args[0]);
                    OutputDir = (string)jsonObject["Output"];
                    SourceName = (string)jsonObject["Source"];
                    LogName = (string)jsonObject["Log"];
                    ThumbnailSize = int.Parse((string)jsonObject["Thumbnail"]);
                    string handlers = (string)jsonObject["Handler"];
                    string[] lst = handlers.Split(';');
                    foreach (string handler in lst)
                    {
                        Handlers.Add(handler);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Could not parse config: " + e.Message);
                }
            } else if (packet.Type == CommandEnum.CloseHandler)
            {
                try
                {
                    Handlers.Remove(packet.Args[0]);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Failed removing a handler, error: " + e.Message);
                }
            }
        }

        private void AddOneLog(ServerLog item)
        {
            string type = ConvertIntToLogType(item.Status);
            Log myLog = new Log(type, item.Message);
            Logs.Add(myLog);
        }

        // Converting the log enum to its proper name.
        private String ConvertIntToLogType(int status)
        {
            if (status == 0)
            {
                return "INFO";
            }
            else if (status == 1)
            {
                return "FAIL";
            }
            else if (status == 2)
            {
               return "WARNING";
            }
            else
            {
                return "Unknown";
            }
        }

        // This function sends a request to delete the handler to the service.
        public bool DeleteHandler(string handler)
        {
            IClient client = ClientSingelton.GetInstance();
            String[] args = { handler };
            client.SendPacket(new MyPacket(CommandEnum.CloseHandler, args));
            Thread.Sleep(200);
            return true;
        }
        public bool IsOnline { get; set; }
        public string OutputDir { get; set; }
        public string SourceName { get; set; }
        public string LogName { get; set; }
        public int ThumbnailSize { get; set; }
        public List<Log> Logs { get; set; }
        public List<String> Handlers { get; set; }
    }
}