using CarDealership.BL.Serializers;
using CarDealership.Models.Configurations;
using Confluent.Kafka;
using Microsoft.Extensions.Options;

namespace CarDealership.BL.Services
{
    public class KafkaConsumerService<TKey, TValue>
    {
        private readonly IOptionsMonitor<KafkaSettings> _kafkaSettings;
        private readonly IConsumer<TKey, TValue> _consumer;
        private Action<TValue> _action;

        public KafkaConsumerService(IOptionsMonitor<KafkaSettings> kafkaSettings, Action<TValue> action)
        {
            _kafkaSettings = kafkaSettings;

            _action = action;

            var config = new ConsumerConfig()
            {
                BootstrapServers = _kafkaSettings.CurrentValue.BootstrapServers,
                AutoOffsetReset = (AutoOffsetReset?)_kafkaSettings.CurrentValue.AutoOffsetReset,
                GroupId = _kafkaSettings.CurrentValue.GroupId
            };

            _consumer = new ConsumerBuilder<TKey, TValue>(config).SetKeyDeserializer(new MsgPackDeserializer<TKey>()).SetValueDeserializer(new MsgPackDeserializer<TValue>()).Build();

            _consumer.Subscribe($"{typeof(TValue).Name}.Cache");
        }

        public void Consume()
        {
            while (true)
            {
                var receivedMessage = _consumer.Consume();
                _action.Invoke(receivedMessage.Message.Value);
                Console.WriteLine(receivedMessage.Message.Value);
                
            }
        }
    }
}
