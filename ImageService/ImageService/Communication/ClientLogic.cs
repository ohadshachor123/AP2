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
    // This class is responsible for the communication logic with the client.
    // In out case: listen to a packet from the client, calculate the response(via an event)
    // And then answer the client accordingly.
    public delegate string NotifyPacket(MyPacket packet);
    public class ClientLogic : IClientLogic
    {
        // This event will be raised whenever the clients want to exit.
        public event EventHandler<TcpClient> ClientExited;
        // This event will be raised whenver a new packet is received.
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
                        // Read a new packet
                        string input = reader.ReadString();
                        MyPacket packet = JsonConvert.DeserializeObject<MyPacket>(input);
                        if (packet.Type == CommandEnum.CloseCommand)
                        {
                            // if the command is remove client command, inform the server of the exiting.
                            ClientExited?.Invoke(this, client);
                            isConnected = false;
                        }
                        else
                        {
                            // Received a command packet, calculate the response(with the event invoke) and send it.
                            string result = NewPacketReceived?.Invoke(packet);
                            //string result = this.controlcontrol.ExecuteCommand((int)packet.Type, packet.Args, out success);
                            packet.Args = new string[1];
                            packet.Args[0] = result;
                            writer.Write(JsonConvert.SerializeObject(packet));

                        }
                    }
                } catch (Exception){
                    ClientExited?.Invoke(this, client);
                }
            }).Start();
        } 
    }
}
