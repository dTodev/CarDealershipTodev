namespace CarDealership.Models.Configurations
{
    public class KafkaSettings
    {
        public string BootstrapServers { get; set; }
        public int AutoOffsetReset { get; set; }
        public string GroupId { get; set; }
        public string KafkaProduceTopic { get; set; }
        public string KafkaConsumeTopicFirst { get; set; }
        public string KafkaConsumeTopicSecond { get; set; }
    }
}
