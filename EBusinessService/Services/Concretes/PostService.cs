using EBusinessData.DAL;
using EBusinessData.UnitOfWorks;
using EBusinessEntity.Entities;
using EBusinessService.Services.Abstraction;
using Microsoft.AspNetCore.Hosting;

namespace EBusinessService.Services.Concretes
{
    public class PostService : IPostService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IHostingEnvironment environment;
        private readonly AppDbContext dbContext;

        public PostService(IUnitOfWork unitOfWork, IHostingEnvironment environment, AppDbContext dbContext)
        {
            this.unitOfWork = unitOfWork;
            this.environment = environment;
            this.dbContext = dbContext;
        }


        public Task AddPostAsync(Post contact)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<Post>> GetAllPostsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Post> GetPostByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task RemovePostAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
