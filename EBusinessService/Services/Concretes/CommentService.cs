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

        public async Task AddCommentAsync(int id, Comment comment)
        {
            var post = await dbContext.Posts.FirstOrDefaultAsync(p => p.Id == id);
            if (comment is not null || post is not null)
            {
                Comment nComment = new Comment
                {
                    Name = comment.Name,
                    Email = comment.Email,
                    Comments = comment.Comments,
                    PostId = post.Id
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
