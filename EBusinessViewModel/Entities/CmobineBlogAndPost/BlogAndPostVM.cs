using EBusinessEntity.Entities;

public class BlogAndPostVM
{
    public IEnumerable<Blog> Blogs { get; set; }
    public IEnumerable<Post> Posts { get; set; }
}

