using CarDealership.Models;

namespace CarDealership.DL.Interfaces
{
    public interface IPurchaseRepository
    {
        public Task<Purchase> SavePurchase(Purchase purchase);
        public Task<IEnumerable<Purchase>> GetAllClientPurchases(int clientId);
        public Task<IEnumerable<Purchase>> GetAllPurchasesForPeriod(DateTime from, DateTime to);
    }
}
