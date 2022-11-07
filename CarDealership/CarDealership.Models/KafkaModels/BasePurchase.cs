using MessagePack;

namespace CarDealership.Models.KafkaModels
{
    [MessagePackObject]
    public class BasePurchase
    {
        [Key(0)]
        public int ClientId { get; set; }
        [Key(1)]
        public IEnumerable<int> CarIds { get; set; }
    }
}
