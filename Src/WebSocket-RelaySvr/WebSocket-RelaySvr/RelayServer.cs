using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;


namespace WebSocket_RelaySvr
{
    class RelayServer
    {

        //
        // start relay server
        // port: server bind port
        //
        public bool Start(int port, Func<byte[], int, bool> handler)
        {
            TcpListener listener = new TcpListener(IPAddress.Any, port);
            listener.Start();

            byte[] header = new byte[12];
            byte[] buff = new byte[1024];

            while (true)
            {
                TcpClient tc = listener.AcceptTcpClient();
                NetworkStream stream = tc.GetStream();

                CircularQueue q = new CircularQueue();
                int remainSize = q.SIZE - q.Size();

                // todo: packet crack!
                while (tc.Connected)
                {
                    int nbytes = 0;
                    try {
                        nbytes = stream.Read(buff, 0, remainSize);
                    } catch (System.Exception e) { 
                        break;
                    }

                    if (nbytes <= 0)
                        break;

                    q.Push(buff, nbytes);

                    // packet header size : 12
                    while (q.Size() >= 12)
                    {
                        q.FrontCopy(header, header.Length);
                        int packetLen = GetPacketLength(header);
                        if (q.Size() < packetLen)
                            break;

                        q.Pop(buff, packetLen);

                        int protocolId = BitConverter.ToInt32(buff, 0);
                        int packetId = BitConverter.ToInt32(buff, 4);

                        // Check Relay Packet
                        // 1334027723 : 'Relay' hashcode
                        if ((protocolId == 100)
                            && (packetId == 1334027723))
                        {
                            handler(buff, packetLen);
                        }
                    }
                }

                stream.Close();
                tc.Close();
            }

        }


        // return packet length
        int GetPacketLength(byte[] buffer)
        {
            return BitConverter.ToInt32(buffer, 8);
        }

    }
}
