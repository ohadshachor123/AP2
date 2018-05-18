using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ServiceGUI.Communication
{
    public class ClientSingelton : IClient
    {
        private const String IP = "127.0.0.1";
        private const int PORT = 44444;
        private static ClientSingelton instance = null;

        private bool isConnected;
        private TcpClient socket;
        public event PacketsHandler NewPacketReceived;

        // Static get instance method
        public static ClientSingelton GetInstance()
        {
            if (instance == null) { instance = new ClientSingelton(); }
            return instance;
        }

        // Private constructor that connects to the server(if possible)
        private ClientSingelton()
        {
            try
            {
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(IP), PORT);
                socket = new TcpClient();
                socket.Connect(endPoint);
                isConnected = true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Could not connect to the server. Error: " + e.Message);
                isConnected = false;
            }            if (isConnected)                StartReceivingPackets();
        }

        public bool IsRunning() {
            return isConnected;
        }

        // Alert the server that the client is exiting.
        public void Close()
        {
            SendPacket(new MyPacket(CommandEnum.Exit, null));
            isConnected = false;
        }

        public void SendPacket(MyPacket packet)
        {
            try
            {
                BinaryWriter writer = new BinaryWriter(socket.GetStream());
                writer.Write(JsonConvert.SerializeObject(packet));
            } catch (Exception e)
            {
                // TODO
            }
        }
        private void StartReceivingPackets()
        {
            new Task(() =>
            {
                try
                {
                    BinaryReader reader = new BinaryReader(this.socket.GetStream());
                    while (this.isConnected)
                    {
                        string packet = reader.ReadString();
                        Console.WriteLine("I read in...." + packet);
                        MyPacket parsedPacket = JsonConvert.DeserializeObject<MyPacket>(packet);
                        this.NewPacketReceived?.Invoke(parsedPacket);
                    }
                }
                catch (Exception e)
                {
                   // TODO
                };
            }).Start();
        }
    }
}
