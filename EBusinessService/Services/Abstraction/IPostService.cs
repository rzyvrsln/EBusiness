using EBusinessEntity.Entities;

namespace EBusinessService.Services.Abstraction
{
    public interface IPostService
    {
        Task AddPostAsync(Post contact);
        Task<ICollection<Post>> GetAllPostsAsync();
        Task RemovePostAsync(int id);
        Task<Post> GetPostByIdAsync(int id);
    }
}
