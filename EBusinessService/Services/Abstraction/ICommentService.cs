using EBusinessEntity.Entities;

namespace EBusinessService.Services.Abstraction
{
    public interface ICommentService
    {
        Task AddCommentAsync(Comment comment);
        Task<ICollection<Comment>> GetAllIncludeCommentsAsync();
    }
}
