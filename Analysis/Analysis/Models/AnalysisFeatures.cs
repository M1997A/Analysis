using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Analysis.Models
{
    public class AnalysisFeatures
    {
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string NormalRange { get; set; }
        [Required]
        public string MeasruingUnit { get; set; }
        [Required]
        public long AnalysisTypeId { get; set; }
        public AnalysisType AnalysisType { get; set; }

    }
}
