using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;

namespace EBusinessViewModel.Entities.Post
{
    public class AddPostVM
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
