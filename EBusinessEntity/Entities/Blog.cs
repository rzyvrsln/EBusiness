using EbusinessCore.Entities;

namespace EBusinessEntity.Entities
{
    public class Blog:BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Post>? Posts { get; set; }
    }
}
