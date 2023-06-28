using EbusinessCore.Entities;

namespace EBusinessEntity.Entities
{
    public class Post:BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }

        public int BlogId { get; set; }
        public Blog? Blog { get; set; }
    }
}
