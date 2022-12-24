using System;
using System.Collections.Generic;

namespace QtProtocol
{
    public static class QtPacketTypeManager
    {
        private static readonly Dictionary<QtPacketType, Tuple<byte, byte>> TypeDictionary =
            new Dictionary<QtPacketType, Tuple<byte, byte>>();

        static QtPacketTypeManager()
        {
            RegisterType(QtPacketType.Handshake, 1, 0);
        }

        public static void RegisterType(QtPacketType type, byte btype, byte bsubtype)
        {
            if (TypeDictionary.ContainsKey(type))
            {
                throw new Exception($"Packet type {type:G} is already registered.");
            }

            TypeDictionary.Add(type, Tuple.Create(btype, bsubtype));
        }

        public static Tuple<byte, byte> GetType(QtPacketType type)
        {
            if (!TypeDictionary.ContainsKey(type))
            {
                throw new Exception($"Packet type {type:G} is not registered.");
            }

            return TypeDictionary[type];
        }

        public static QtPacketType GetTypeFromPacket(QtPacket packet)
        {
            var type = packet.PacketType;
            var subtype = packet.PacketSubtype;

            foreach (var tuple in TypeDictionary)
            {
                var value = tuple.Value;

                if (value.Item1 == type && value.Item2 == subtype)
                {
                    return tuple.Key;
                }
            }

            return QtPacketType.Unknown;
        }
    }
}
