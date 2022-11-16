public class QtPacket
{
    public byte PacketType { get; private set; }
    public byte PacketSubtype { get; private set; }
    public List<QtPacketField> Fields { get; set; } = new List<QtPacketField>();
}
