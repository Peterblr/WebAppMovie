using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppMovie.Data.ViewModels
{
    public class CreateRoleViewModels
    {
        [Required]
        [Display(Name = "Role")]
        public string RoleName { get; set; }
    }
}
