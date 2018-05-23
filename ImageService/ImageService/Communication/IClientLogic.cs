using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace ImageService.Communication
{
    public interface IClientLogic
    {
        event EventHandler<TcpClient> ClientExited;
        event NotifyPacket NewPacketReceived;
        void HandleClient(TcpClient client);
    }
}
