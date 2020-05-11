//
// Reference site
//  - https://devstarsj.github.io/development/2016/08/31/CSharp.ClientWebSocketExample/
// Reference code
//  - https://github.com/sta/websocket-sharp#websocket-client
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp;


namespace WebSocket_Relay
{
    class Program
    {
        static void Main(string[] args)
        {
            WebSocket ws = new WebSocket(url: "ws://localhost:8080/chat");
            ws.OnMessage += (sender, e) =>
            {
                Console.WriteLine(e.Data);
                byte[] data = e.RawData;
            };

            ws.Connect();
            ws.Send("c# Test");

            while (ws.IsAlive)
            {

            }
        }
    }
}
