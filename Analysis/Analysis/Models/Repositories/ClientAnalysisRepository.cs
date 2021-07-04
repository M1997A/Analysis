using Analysis.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Analysis.Models.Repositories
{
    public interface IClientAnalysisRepository
    {
        IEnumerable<ClientsListVM> ClientAnalysis { get; }
        long AddClient(Client client);
        void AddClientAnalysis(ClientAnalysis clientAnalysis);
    }
    public class ClientAnalysisRepository : IClientAnalysisRepository
    {
        private AppDbContext dbContext;
        public ClientAnalysisRepository(AppDbContext context)
        {
            dbContext = context;
        }

        public IEnumerable<ClientsListVM> ClientAnalysis =>
            dbContext.ClientAnalysis.Where(ca => ca.Finished == false)
            .Select(s => new ClientsListVM
            {
                AnalysisName = s.AnalysisType.Name,
                AnalysisTypeId = s.AnalysisTypeId,
                CAId = s.Id,
                ClientCode = s.Client.ClientCode,
                ClientName = s.Client.Name,
                RecievedDate = s.Client.RecievedDate
                
            }).ToArray();
            //.Include(ca => ca.Client).Include(ca => ca.AnalysisType).OrderBy(ca => ca.Id);

        public long AddClient(Client client)
        {
            dbContext.Clients.Add(client);
            dbContext.SaveChanges();
            return client.Id;
        }

        public void AddClientAnalysis(ClientAnalysis clientAnalysis)
        {
            dbContext.ClientAnalysis.Add(clientAnalysis);
            dbContext.SaveChanges();
        }
    }
}
