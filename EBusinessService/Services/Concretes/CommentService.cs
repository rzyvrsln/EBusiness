using EBusinessData.DAL;
using EBusinessData.UnitOfWorks;
using EBusinessEntity.Entities;
using EBusinessService.Services.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace EBusinessService.Services.Concretes
{
    public class CommentService : ICommentService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly AppDbContext dbContext;

        public CommentService(IUnitOfWork unitOfWork, AppDbContext dbContext)
        {
            this.unitOfWork = unitOfWork;
            this.dbContext = dbContext;
        }

        public async Task AddCommentAsync(Comment comment)
        {
            if (comment is not null)
            {
                Comment nComment = new Comment
                {
                    Name = comment.Name,
                    Email = comment.Email,
                    Comments = comment.Comments,
                    PostId = comment.PostId
                };
                await unitOfWork.GetRepository<Comment>().AddAsync(nComment);
                await unitOfWork.SaveChangeAsync();
            }
        }

        public async Task<ICollection<Comment>> GetAllIncludeCommentsAsync()
        {
            return await dbContext.Comments.Include(c => c.Post).ToListAsync();

        }
    }
}
