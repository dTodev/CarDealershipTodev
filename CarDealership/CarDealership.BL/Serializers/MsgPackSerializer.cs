using Confluent.Kafka;
using MessagePack;

namespace CarDealership.BL.Serializers
{
    public class MsgPackSerializer<T> : ISerializer<T>
    {
        public byte[] Serialize(T data, SerializationContext context)
        {
            return MessagePackSerializer.Serialize(data);
        }
    }
}
