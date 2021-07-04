using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Analysis.Models.Repositories
{
    public interface IAnalysisFeaturesRepository
    {
        IEnumerable<AnalysisFeatures> GetAnalysisFeatures(long AnalysisTypeId);

        void AddFeature(AnalysisFeatures analysisFeatures);
        void UpdateFeature(AnalysisFeatures analysisFeature);
        void DeleteFeature(AnalysisFeatures analysisFeatures);
    }
    public class AnalysisFeaturesRepository : IAnalysisFeaturesRepository
    {
        private AppDbContext dbContext;
        public AnalysisFeaturesRepository(AppDbContext appDbContext) => dbContext = appDbContext;

        public void AddFeature(AnalysisFeatures analysisFeatures)
        {
            if(analysisFeatures != null)
            {
                dbContext.AnalysisFeatures.Add(analysisFeatures);
                dbContext.SaveChanges();
            }
        }

        public void DeleteFeature(AnalysisFeatures analysisFeatures)
        {
            dbContext.AnalysisFeatures.Remove(analysisFeatures);
            dbContext.SaveChanges();
        }

        public IEnumerable<AnalysisFeatures> GetAnalysisFeatures(long AnalysisTypeId)
        {
            return dbContext.AnalysisFeatures.Where(af => af.AnalysisTypeId == AnalysisTypeId).Include(af => af.AnalysisType);
        }

        public void UpdateFeature(AnalysisFeatures analysisFeature)
        {
            dbContext.AnalysisFeatures.Update(analysisFeature);
            dbContext.SaveChanges();
        }
    }
}
