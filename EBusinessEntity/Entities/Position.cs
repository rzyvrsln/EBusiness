using EbusinessCore.Entities;
using System.ComponentModel.DataAnnotations;

namespace EBusinessEntity.Entities
{
    public class Position:BaseEntity
    {
        [Required]
        public string Name { get; set; }
        public ICollection<Employee>? Employees { get; set; }
    }
}
