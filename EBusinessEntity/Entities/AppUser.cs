using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace EBusinessEntity.Entities
{
    public class AppUser:IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public bool IsParsistance { get; set; }
    }
}
