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
        // Handle a client that connected to the image receiving port.
        public void HandleClient(TcpClient client)
        {
            new Task(() =>
            {
                try
                {
                    NetworkStream stream = client.GetStream();
                    Byte[] buffer = new Byte[6790];
                    List<Byte> finalbytes = new List<byte>();
                    List<Byte> nameInBytes = new List<byte>();
                    Byte[] helpingBytes;
                    int i = 0, nameCounter = 0;
                    // reading the name from the client
                    do
                    {
                        nameCounter = stream.Read(buffer, 0, 1);
                        nameInBytes.Add(buffer[0]);
                    } while (stream.DataAvailable);
                    String imageName = Encoding.UTF8.GetString(nameInBytes.ToArray());
                    //Sending confirmation to the client.
                    stream.Write(new Byte[] { 1 }, 0, 1);
                    //Reading the file itself
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
                    // Writing the file in the first handler.
                    string[] handlers = ConfigurationManager.AppSettings["Handler"].Split(';');
                    if (handlers.Length != 0)
                        File.WriteAllBytes(handlers[0] + "\\" + imageName, finalbytes.ToArray());
                    // sleeping so the system writes the file enitrely before moving on.
                    System.Threading.Thread.Sleep(500);
                    // Closing the client.
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