using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Analysis.Models
{
    public class Results
    {
        public long Id { get; set; }
        public long ClientAnalysisId { get; set; }
        public ClientAnalysis ClientAnalysis { get; set; }
        public long AnalysisFeaturesId { get; set; }
        public AnalysisFeatures AnalysisFeatures { get; set; }
        public string Value { get; set; }
    }
}
