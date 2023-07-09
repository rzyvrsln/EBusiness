using EBusinessEntity.Entities;
using EBusinessViewModel.Entities.Pagination;

public class BlogAndPostVM
{
    public IEnumerable<Blog>? Blogs { get; set; }
    public IEnumerable<Post>? Posts { get; set; }
    public Post? Post { get; set; }
    public IEnumerable<Employee>? Employees { get; set; }
    public Comment? Comment { get; set; }
    public IEnumerable<Comment>? Comments { get; set; }
    public PaginationVM<Post>? PaginationVM { get; set; }
}

