using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.IO;
using Newtonsoft.Json;
using ImageService.Commands;

namespace ImageService.Communication
{
    public delegate string NotifyPacket(MyPacket packet);
    public class ClientLogic : IClientLogic
    {
        public event EventHandler<TcpClient> ClientExited;
        public event NotifyPacket NewPacketReceived;
        public void HandleClient(TcpClient client)
        {
            new Task(() =>
            {
                try {
                    bool isConnected = true;
                    NetworkStream stream = client.GetStream();
                    BinaryReader reader = new BinaryReader(stream);
                    BinaryWriter writer = new BinaryWriter(stream);
                    while (isConnected)
                    {
                        string input = reader.ReadString();
                        MyPacket packet = JsonConvert.DeserializeObject<MyPacket>(input);
                        if (packet.Type != CommandEnum.CloseCommand)
                        {
                            //bool success;
                            string result = NewPacketReceived?.Invoke(packet);
                            //string result = this.controlcontrol.ExecuteCommand((int)packet.Type, packet.Args, out success);
                            packet.Args = new string[1];
                            packet.Args[0] = result;
                            writer.Write(JsonConvert.SerializeObject(packet));
                        }
                        else
                        {
                            // if the command is remove client command
                            ClientExited?.Invoke(this, client);
                            isConnected = false;
                        }
                    }
                } catch (Exception){
                    ClientExited?.Invoke(this, client);
                }
            }).Start();
        } 
    }
}
