using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Analysis.Models.ViewModels
{
    public class WriteResultsVM
    {
        public IEnumerable<AnalysisFeatures> AnalysisFeatures { get; set; }
        public string ClientName { get; set; }
        public string AnalysisName { get; set; }
        public long ClientAnalysisId { get; set; }
        public Dictionary<long, string> AFDictionary { get; set; } = new Dictionary<long, string>();
    }
}
