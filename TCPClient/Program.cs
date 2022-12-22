using System;
using System.Threading;
using QtProtocol;
using QtProtocol.Serializator;

namespace TCPClient
{
    internal class Program
    {
        private static int _handshakeMagic;

        private static void Main()
        {
            Console.Title = "QtClient";
            Console.ForegroundColor = ConsoleColor.White;
            
            var client = new QtClient();
            client.OnPacketRecieve += OnPacketRecieve;
            client.Connect("127.0.0.1", 4910);

            var rand = new Random();
            _handshakeMagic = rand.Next();

            Thread.Sleep(1000);
            
            Console.WriteLine("Sending handshake packet..");

            client.QueuePacketSend(
                QtPacketConverter.Serialize(
                    QtPacketType.Handshake,
                    new QtPacketHandshake
                    {
                        MagicHandshakeNumber = _handshakeMagic
                    })
                    .ToPacket());

            while(true) {}
        }

        private static void OnPacketRecieve(byte[] packet)
        {
            var parsed = QtPacket.Parse(packet);

            if (parsed != null)
            {
                ProcessIncomingPacket(parsed);
            }
        }

        private static void ProcessIncomingPacket(QtPacket packet)
        {
            var type = QtPacketTypeManager.GetTypeFromPacket(packet);

            switch (type)
            {
                case QtPacketType.Handshake:
                    ProcessHandshake(packet);
                    break;
                case QtPacketType.Unknown:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private static void ProcessHandshake(QtPacket packet)
        {
            var handshake = XPacketConverter.Deserialize<QtPacketHandshake>(packet);

            if (_handshakeMagic - handshake.MagicHandshakeNumber == 15)
            {
                Console.WriteLine("Handshake successful!");
            }
        }
    }
}
