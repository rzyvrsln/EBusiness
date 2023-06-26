using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBusinessViewModel.Entities.Employee
{
    public class UpdateEmployeeVM
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public IFormFile Image { get; set; }
        public string? Linkedin { get; set; }
        public string? Instagram { get; set; }
        public string? Twitter { get; set; }
        [Required]
        public int PositionId { get; set; }
    }
}
