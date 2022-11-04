using CarDealership.Models.Users;

namespace CarDealership.DL.Interfaces
{
    public interface IClientRepository
    {
        public Task <Client> CreateClient(Client client);
        public Task <Client> UpdateClient(Client client);
        public Task <Client> DeleteClient(int clientId);
        public Task <IEnumerable<Client>> GetAllClients();
        public Task <Client> GetClientById(int clientId);
        public Task <Client> GetClientByName(string clientName);
        public Task <Client> GetClientByEmail(string clientEmail);
        public Task <Client> AddPurchaseData();
    }
}
