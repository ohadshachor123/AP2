﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ServiceGUI.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication2.Models;

namespace WebApplication2.Communication
{
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
                client.NewPacketReceived += handlePacket;
                client.SendPacket(new MyPacket(CommandEnum.GetConfig, null));
                client.SendPacket(new MyPacket(CommandEnum.AllLogs, null));
            }
        }

        private void handlePacket(MyPacket packet) {
            if (packet.Type == CommandEnum.AllLogs)
            {
                try
                {
                    List<Log> logList = JsonConvert.DeserializeObject<List<Log>>(packet.Args[0]);
                    foreach (Log item in logList)
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
                    AddOneLog(JsonConvert.DeserializeObject<Log>(packet.Args[0]));
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

        private void AddOneLog(Log item)
        {
            Logs.Add(item);
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