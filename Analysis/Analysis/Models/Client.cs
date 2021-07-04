using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Analysis.Models
{
    public class Client
    {
       
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [Range(1,120)]
        public int Age { get; set; }
        [Required]
        public string Gender { get; set; }
        public string Address { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Diseases { get; set; }
        public string Doctor { get; set; }
        [Required]
        [Column(TypeName = "Date")]
        [UIHint("Date")]
        public DateTime RecievedDate { get; set; }
        public int ClientCode { get; set; }
        public IEnumerable<ClientAnalysis> ClientAnalysis { get; set; }

    }
}
