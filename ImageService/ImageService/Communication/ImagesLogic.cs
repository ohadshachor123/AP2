using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
namespace ImageService.Communication
{
    public class ImagesLogic : IClientLogic
    {
        // This event will be raised whenever the clients want to exit.
        public event EventHandler<TcpClient> ClientExited;
        // This event will be raised whenver a new packet is received.
        public event NotifyPacket NewPacketReceived;
        public void HandleClient(TcpClient client)
        {
            new Task(() =>
            {
                try
                {
                    NetworkStream stream = client.GetStream();
                    Byte[] buffer = new Byte[6790];
                    List<Byte> finalbytes = new List<byte>();
                    String imageName;
                    List<Byte> finalName = new List<byte>();
                    Byte[] helpingBytes;
                    int i = 0, nameCounter = 0;
                    // name
                    do
                    {
                        nameCounter = stream.Read(buffer, 0, 1);
                        finalName.Add(buffer[0]);
                    } while (stream.DataAvailable);
                    imageName = Encoding.UTF8.GetString(finalName.ToArray());
                    stream.Write(new Byte[] { 1 }, 0, 1);

                    do
                    {
                        i = stream.Read(buffer, 0, buffer.Length);
                        helpingBytes = new byte[i];
                        for (int n = 0; n < i; n++)
                        {
                            helpingBytes[n] = buffer[n];
                            finalbytes.Add(helpingBytes[n]);
                        }
                        System.Threading.Thread.Sleep(200);
                    } while (stream.DataAvailable || i == buffer.Length);
                    string[] handlers = ConfigurationManager.AppSettings["Handler"].Split(';');
                    if (handlers.Length != 0)
                        File.WriteAllBytes(handlers[0] + "\\" + imageName, finalbytes.ToArray());
                    System.Threading.Thread.Sleep(500);
                    client.Close();
                    ClientExited?.Invoke(this, client);
                }
                catch (Exception ex)
                {
                    Server.logger.Log(ex.Message, Logging.MessageType.FAIL);
                    client.Close();
                    ClientExited?.Invoke(this, client);
                }
            }).Start();
        }
    }
}