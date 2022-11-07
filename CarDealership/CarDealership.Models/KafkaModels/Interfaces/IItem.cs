namespace CarDealership.Models.KafkaModels.Interfaces
{
    public interface IItem<out T>
    {
        T GetKey();
    }
}
