using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Analysis.Models.ViewModels
{
    public class ClientsListVM
    {
        public long CAId { get; set; }
        public string ClientName { get; set; }
        public string AnalysisName { get; set; }
        public DateTime RecievedDate { get; set; }
        public long ClientCode { get; set; }
        public long AnalysisTypeId { get; set; }
    }
}
