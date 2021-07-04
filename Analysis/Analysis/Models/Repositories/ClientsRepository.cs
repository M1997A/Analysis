using Analysis.Models.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Analysis.Models.Repositories
{
    public interface IClientsRepository 
    {
        IEnumerable<Client> GetClients(QueryOptions options);
        Client GetClient(long ClientId);
        void DeleteClient(Client client);
        void UpdateClient(Client client);
    }
    public class ClientsRepository : IClientsRepository
    {
        private AppDbContext dbContext;
        public ClientsRepository(AppDbContext context) => dbContext = context;
        public IEnumerable<Client> GetClients(QueryOptions options)
        {
            return new PagedList<Client>(dbContext.Clients, options);
        }
        public void DeleteClient(Client client)
        {
            dbContext.Clients.Remove(client);
            dbContext.SaveChanges();
        }
        public void UpdateClient(Client client)
        {
            dbContext.Clients.Update(client);
            dbContext.SaveChanges();
        }
        public Client GetClient(long ClientId)
        {
            return dbContext.Clients.Find(ClientId);

        }
    }
}
