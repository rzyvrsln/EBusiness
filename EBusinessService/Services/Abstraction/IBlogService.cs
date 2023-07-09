using EBusinessEntity.Entities;
using EBusinessViewModel.Entities.Pagination;

namespace EBusinessService.Services.Abstraction
{
    public interface IBlogService
    {
        Task AddBlogAsync(Blog blog);
        Task<ICollection<Blog>> GetAllBlogsAsync();
        Task RemoveBlogAsync(int id);
        Task<Blog> EditBlogAsync(int id);
        Task EditPostBlogAsync(int id, Blog blog);
        Task<PaginationVM<Blog>> PaginationForBlogAsync(int page);
    }
}
