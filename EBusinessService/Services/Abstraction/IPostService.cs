using EBusinessEntity.Entities;
using EBusinessViewModel.Entities.Post;

namespace EBusinessService.Services.Abstraction
{
    public interface IPostService
    {
        Task AddPostAsync(AddPostVM postVM);
        Task<ICollection<Post>> GetAllPostAsync();
        Task RemovePostAsync(int id);
        Task<EditPostVM> EditPostAsync(int id);
        Task EditPostPostAsync(int id, EditPostVM postVM);
    }
}
