using CarDealership.BL.Serializers;
using CarDealership.Models.Configurations;
using CarDealership.Models.Responses.PurchaseResponses;
using Confluent.Kafka;
using Microsoft.Extensions.Options;

namespace CarDealership.BL.Services
{
    public class KafkaProducerService<TKey, TValue>
    {
        private readonly IProducer<TKey, TValue> _producer;
        private readonly IOptionsMonitor<KafkaSettings> _kafkaSettings;

        public KafkaProducerService(IOptionsMonitor<KafkaSettings> kafkaSettings)
        {
            _kafkaSettings = kafkaSettings;

            var config = new ProducerConfig()
            {
                BootstrapServers = _kafkaSettings.CurrentValue.BootstrapServers
            };

            _producer = new ProducerBuilder<TKey, TValue>(config).SetKeySerializer(new MsgPackSerializer<TKey>()).SetValueSerializer(new MsgPackSerializer<TValue>()).Build();
        }

        public async Task<CreatePurchaseResponse> ProduceMessage(TKey key, TValue value)
        {
            try
            {
                var message = new Message<TKey, TValue>()
                {
                    Key = key,
                    Value = value
                };

                var result = await _producer.ProduceAsync($"{typeof(TValue).Name}.Cache", message);

                if (result != null)
                {
                    Console.WriteLine($"Delivered: {result.Message.Value} to {result.TopicPartitionOffset}");
                }
            }
            catch (ProduceException<int, string> e)
            {
                Console.WriteLine($"Delivery failed: {e.Error.Reason}");
                return new CreatePurchaseResponse { HttpStatusCode = System.Net.HttpStatusCode.BadRequest };
            }

            return new CreatePurchaseResponse { HttpStatusCode = System.Net.HttpStatusCode.OK };
        }

    }
}
