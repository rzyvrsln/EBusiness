using EBusinessEntity.Entities;

namespace EBusinessService.Services.Abstraction
{
    public interface ICommentService
    {
        Task AddCommentAsync(int id, Comment comment);
        Task<ICollection<Comment>> GetAllIncludeCommentsAsync();
    }
}
