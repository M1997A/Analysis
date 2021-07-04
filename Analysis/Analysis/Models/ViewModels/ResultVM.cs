using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Analysis.Models.ViewModels
{
    public class ResultVM
    {
        public long ClientAnalysisId { get; set; }
        public IEnumerable<AnalysisKeyValue> AnalusisKeyValue { get; set; }
    }
    public class AnalysisKeyValue
    {
        public long AnalysisFeaturesId { get; set; }
        public string Value { get; set; }
    }
}
