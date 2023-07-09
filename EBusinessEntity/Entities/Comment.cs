using EbusinessCore.Entities;
using System.ComponentModel.DataAnnotations;

namespace EBusinessEntity.Entities
{
    public class Comment:BaseEntity
    {
        [Required]
        public string Name { get; set; }
        [EmailAddress, Required]
        public string Email { get; set; }
        [Required]
        public string Comments { get; set; }

        public int PostId { get; set; }
        public Post? Post { get; set; }
    }
}
