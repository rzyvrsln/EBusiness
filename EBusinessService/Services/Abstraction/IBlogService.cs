using EBusinessEntity.Entities;

namespace EBusinessService.Services.Abstraction
{
    public interface IBlogService
    {
        Task AddBlogAsync(Blog blog);
        Task<ICollection<Blog>> GetAllBlogsAsync();
        Task RemoveBlogAsync(int id);
        Task<Blog> EditBlogAsync(int id);
        Task EditPostBlogAsync(int id, Blog blog);
    }
}
