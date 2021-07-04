using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Analysis.Models.Repositories
{
    public interface IAnalysisTypeRepository
    {
        IEnumerable<AnalysisType> AllAnalysis { get; }
        long AddAnalysis(AnalysisType analysisType);
        AnalysisType GetAnalysis(long Id);

        void UpdateAnalysis(AnalysisType analysisType);
        void DeleteAnalysis(AnalysisType analysisType);
    }
    public class AnalysisTypeRepository : IAnalysisTypeRepository
    {
        private AppDbContext dbContext;
        public AnalysisTypeRepository(AppDbContext appDbContext) => dbContext = appDbContext;
        public IEnumerable<AnalysisType> AllAnalysis => dbContext.AnalysisType.ToArray();

        public long AddAnalysis(AnalysisType analysisType)
        {
            dbContext.AnalysisType.Add(analysisType);
            dbContext.SaveChanges();
            return analysisType.Id;
        }

        public void DeleteAnalysis(AnalysisType analysisType)
        {
            dbContext.AnalysisType.Remove(analysisType);
            dbContext.SaveChanges();
        }

        public AnalysisType GetAnalysis(long Id)
        {
            return dbContext.AnalysisType.Find(Id);
        }

        public void UpdateAnalysis(AnalysisType analysisType)
        {
            dbContext.AnalysisType.Update(analysisType);
            dbContext.SaveChanges();
        }
    }
}
