using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Analysis.Models.ViewModels
{
    public class CreateUserVM
    {
        [Required]
        
        public string UserName { get; set; }
        [UIHint("password")]
        [Required]
        public string Password { get; set; }
        
        public bool IsAdmin { get; set; }
    }
}
