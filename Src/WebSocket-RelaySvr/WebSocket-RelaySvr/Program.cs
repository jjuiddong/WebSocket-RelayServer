using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

using WebSocketSharp;


namespace WebSocket_RelaySvr
{
    class Program
    {
        static WebSocket m_ws;

        static void Main(string[] args)
        {
            m_ws = new WebSocket(url: "ws://localhost:8080/chat");
            m_ws.OnMessage += (sender, e) =>
            {
                Console.WriteLine(e.Data);
                byte[] data = e.RawData;
            };

            m_ws.Connect();
            m_ws.Send("c# Test");
            //while (ws.IsAlive)
            //    System.Threading.Thread.Sleep(1000);

            RelayServer server = new RelayServer();
            server.Start(10002, PacketHandler);
        }


        static bool PacketHandler(byte[] buff, int size)
        {
            ASCIIEncoding ascii = new ASCIIEncoding();
            String str = ascii.GetString(buff, 12, size - 13);

            if (m_ws.IsAlive)
            {
                String msg =
                    "{\"type\":\"message\",\"text\":\""
                    + str + "\",\"id\":1,\"date\":1589201808741}";
                m_ws.Send(msg);
            }

            return true;
        }

    }
}
