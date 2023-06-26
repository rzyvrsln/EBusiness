using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace EBusinessViewModel.Entities.Employee
{
    public class AddEmployeeVM
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
