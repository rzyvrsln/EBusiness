using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace EBusinessEntity.Entities
{
    public class User:IdentityUser
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        public bool IsParsistance { get; set; }
    }
}
