using QtProtocol.Serializator;

namespace QtProtocol
{
    public class QtPacketHandshake
    {
        [QtField(1)]
        public int MagicHandshakeNumber;
    }
}
