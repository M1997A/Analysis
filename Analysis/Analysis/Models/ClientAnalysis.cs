using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Analysis.Models
{
    public class ClientAnalysis
    {
        public long Id { get; set; }
        public long ClientId { get; set; }
        public Client Client { get; set; }
        public long AnalysisTypeId { get; set; }
        public AnalysisType AnalysisType { get; set; }
       
        public bool Finished { get; set; }
        [Column(TypeName = "Date")]
        [NotMapped]
        public DateTime DeliveryDate { get; set; }
        [NotMapped]
        [Required(ErrorMessage ="You must select at least one analysis")]
        public IEnumerable<long> AnalysisIds { get; set; }

    }
}
