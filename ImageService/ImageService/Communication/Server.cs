using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageService.Commands;
using ImageService.Handlers;
using ImageService.Logging;
using System.Net.Sockets;
using ImageService.Communication;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace ImageService
{
    public class Server
    {
        private const string IP = "127.0.0.1";
        private const int PORT = 44444;
        private IlogService logger;
        private IController controller;
        private IClientLogic clientsLogic;

        private TcpListener clientsListener;
        private List<TcpClient> clients;
        // ReceiveCommand is the event that is raised whenever the server gets a command from the service.
        public event EventHandler<Commands.CommandReceivedArgs> ReceiveCommand;
        // Close all- is the event that is raised whenever the image service stops.
        public event EventHandler<EventArgs> CloseAll;
        public Server(IlogService logger, IController controller)
        {
            this.logger = logger;
            this.logger.MessageRecieved += SendLogToGUI;
            this.controller = controller;
            this.clientsLogic = new ClientLogic();
            this.clientsLogic.ClientExited += this.OnClientExit;
            this.clientsLogic.NewPacketReceived += this.HandlePacket;
            ListenToClients();
        }

        private void ListenToClients()
        {
            clients = new List<TcpClient>();
            try
            {
                IPEndPoint connectionSettings = new IPEndPoint(IPAddress.Parse(IP), PORT);
                this.clientsListener = new TcpListener(connectionSettings);
                this.clientsListener.Start();
                new Task(() => {
                    while (true)
                    {
                        try
                        {
                            TcpClient client = this.clientsListener.AcceptTcpClient();
                            this.clients.Add(client);
                            this.clientsLogic.HandleClient(client);
                        }
                        catch (Exception e)
                        {
                            logger.Log("Error accepting a client: " + e.Message, MessageType.WARNING);
                        }
                    }
                }).Start();
            }
            catch (Exception e)
            {
                logger.Log("Error listening to clients " + e.Message, MessageType.FAIL);
            }
        }

        public void AddPath(string path)
        {
            logger.Log("Handling a new path :" + path);
            IHandler handler = new DirectoryHandler(logger, controller);
            ReceiveCommand += handler.OnCommandRecieved;
            CloseAll += handler.CloseMe;
            handler.selfCloser += this.HandlerClosedEventListener;
            // Exception might be thrown if the path is invalid.
            try {  
                handler.StartHandleDirectory(path);
            } catch(Exception e)
            {  
                logger.Log("Could not handle the path: " + path + ". Error: " + e.Message);
                handler.CloseMe(null,null);
                CloseAll -= handler.CloseMe;
            }
        }

        public void PerformCommand(Commands.CommandReceivedArgs args)
        {
            ReceiveCommand?.Invoke(this, args);
        }

        public void Stop()
        {
            CloseAll?.Invoke(this, null);
        }
        private void OnClientExit(object sender, TcpClient client)
        {
            client.Close();
            try
            {
                clients.Remove(client);
            } catch (Exception)
            {
                logger.Log("Trying to remove a client that is not connected.");
            }
        }
        private void SendLogToGUI(object sender, MessageReceivedArgs args)
        {
            string[] packetArgs = new string[1];
            packetArgs[0] = JsonConvert.SerializeObject(args);
            MyPacket packet = new MyPacket(CommandEnum.ReceiveNewLog, packetArgs);
            foreach (TcpClient client in clients)
            {
                try
                {
                    NetworkStream stream = client.GetStream();
                    BinaryWriter writer = new BinaryWriter(stream);
                    writer.Write(JsonConvert.SerializeObject(packet));
                        // closing the client upon exception.
                } catch(Exception e)
                {
                    OnClientExit(this, client);
                }
            }
        }

        private string HandlePacket(MyPacket packet)
        {
            CommandReceivedArgs args = new CommandReceivedArgs((int)packet.Type, packet.Args, null);
            if (ReceiveCommand != null)
                ReceiveCommand?.Invoke(this, args);
            bool result;
            return this.controller.ExecuteCommand(args.CommandID, args.Args, out result);
        }

        private void HandlerClosedEventListener(object sender, string path)
        {
            string[] args = { path };
            MyPacket packet = new MyPacket(CommandEnum.CloseHandler, args);
            foreach(TcpClient client in clients)
            {
                try
                {
                    NetworkStream stream = client.GetStream();
                    BinaryWriter writer = new BinaryWriter(stream);
                    writer.Write(JsonConvert.SerializeObject(packet));
                    // closing the client upon exception.
                }
                catch (Exception e)
                {
                    OnClientExit(this, client);
                }
            }
        }
    }
}

