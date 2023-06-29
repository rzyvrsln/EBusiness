using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace EBusinessViewModel.Entities.Post
{
    public class EditPostVM
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public IFormFile Image { get; set; }

        [Required]
        public int BlogId { get; set; }
    }
}
