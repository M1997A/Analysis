using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Analysis.Models
{
    public class AnalysisType
    {
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
       [Required]
        public string SampleType { get; set; }
        [Required]
        [Range(1,int.MaxValue)]
        public decimal Price { get; set; }
       
        public int Duration { get; set; }
        
        public string Precautions { get; set; }
        
        public IEnumerable<AnalysisFeatures> AnalysisFeatures { get; set; }
    }
}
