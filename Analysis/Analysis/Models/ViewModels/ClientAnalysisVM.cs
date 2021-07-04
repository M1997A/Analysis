using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Analysis.Models.ViewModels
{
    public class ClientAnalysisVM
    {
        public Client Client { get; set; }
        public ClientAnalysis ClientAnalysis { get; set; } = new ClientAnalysis();
        public IEnumerable<AnalysisType> AllAnalysis { get; set; }
    }

}
