using System;

namespace QtProtocol.Serializator
{
    [AttributeUsage(AttributeTargets.Field)]
    public class OwOFieldAttribute : Attribute
    {
        public byte FieldID { get; }

        public QtFieldAttribute(byte fieldId)
        {
            FieldID = fieldId;
        }
    }
}
