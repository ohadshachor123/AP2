using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceGUI.Communication
{

    public delegate void PacketsHandler(MyPacket packet);
    public interface IClient
    {
        event PacketsHandler NewPacketReceived;
        bool IsRunning();
        void SendPacket(MyPacket packet);
        void Close();
        // I do not want this public so no one by mistake will call it multiple times(creating many threads)
        //void StartListeningToPackets();

    }
}
