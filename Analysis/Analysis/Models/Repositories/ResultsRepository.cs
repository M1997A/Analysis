using Analysis.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Analysis.Models.Repositories
{
    public interface IResultsRepository
    {
        bool CheckClientResult(long ClientCode);
        void AddResults(long ClientAnalysisId,List<AnalysisKeyValue> analysisKeyValue);
        List<IEnumerable<CResult>> GetClientResult(long ClientCode);
    }
    public class ResultsRepository : IResultsRepository
    {
        private AppDbContext dbContext;
        public ResultsRepository(AppDbContext context) => dbContext = context;

        public bool CheckClientResult(long ClientCode)
        {
            Client Client = dbContext.Clients.FirstOrDefault(c => c.ClientCode == ClientCode);
            if(Client != null)
            {
                IEnumerable<ClientAnalysis> clientAnalysis = dbContext.ClientAnalysis.Where(ci => ci.ClientId == Client.Id).ToArray();
                foreach(ClientAnalysis CA in clientAnalysis)
                {
                    if (dbContext.Results.Any(r => r.ClientAnalysisId == CA.Id)) return true;
                }
            }

            return false;
        }
        public void AddResults(long ClientAnalysisId, List<AnalysisKeyValue> analysisKeyValue)
        {
            foreach(var item in analysisKeyValue)
            {
                Results results = new Results
                {
                    AnalysisFeaturesId = item.AnalysisFeaturesId,
                    Value = item.Value,
                    ClientAnalysisId = ClientAnalysisId
                };
                dbContext.Results.Add(results);
                
            }
            ClientAnalysis clientAnalysis = dbContext.ClientAnalysis.Find(ClientAnalysisId);
            if(clientAnalysis != null)
            {
                clientAnalysis.Finished = true;
                dbContext.Attach(clientAnalysis);
                dbContext.Entry(clientAnalysis).Property(ca => ca.Finished).IsModified = true;
            }
            
            dbContext.SaveChanges();
        }

        public List<IEnumerable<CResult>> GetClientResult(long ClientCode)
        {
            List<IEnumerable<CResult>> resultsList = new List<IEnumerable<CResult>>();
            Client Client = dbContext.Clients.FirstOrDefault(c => c.ClientCode == ClientCode);
            if (Client != null)
            {
                long ClientId = Client.Id;

                var analysis = dbContext.ClientAnalysis.Where(ca => ca.ClientId == ClientId);
                if (analysis != null)
                {
                    foreach (var item in analysis)
                    {
                        var result = from cresult in dbContext.Results
                                     join afeatures in dbContext.AnalysisFeatures
                                     on cresult.AnalysisFeaturesId equals afeatures.Id
                                     where cresult.ClientAnalysisId == item.Id
                                     select new CResult
                                     {
                                         ClientDoctor = Client.Doctor,
                                         ClientName = Client.Name,
                                         Name = afeatures.Name,
                                         Value = cresult.Value,
                                         MeasuringUnit = afeatures.MeasruingUnit,
                                         NormalRange = afeatures.NormalRange
                                     };
                        resultsList.Add(result);
                    }
                }
            }
            return resultsList;
        }
       
    }
    //public class CResult
    //{
    //    public string Name { get; set; }
    //    public string Value { get; set; }
    //    public string MeasuringUnit { get; set; }
    //    public string NormalRange { get; set; }
    //}
}
