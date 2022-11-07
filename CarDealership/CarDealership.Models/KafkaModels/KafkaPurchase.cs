using CarDealership.Models.KafkaModels.Interfaces;
using MessagePack;

namespace CarDealership.Models.KafkaModels
{
    [MessagePackObject]
    public record KafkaPurchase : IItem<Guid>
    {
        [Key(0)]
        public Guid Id { get; init; }
        [Key(1)]
        public ICollection<Car> Cars { get; set; } = Enumerable.Empty<Car>().ToList();
        [Key(2)]
        public int ClientId { get; set; }
        [Key(3)]
        public decimal Price { get; set; }

        public KafkaPurchase()
        {
            Id = Guid.NewGuid();
        }

        public Guid GetKey() => Id;
    }
}
