using EBusinessData.DAL;
using EBusinessData.UnitOfWorks;
using EBusinessEntity.Entities;
using EBusinessService.Services.Abstraction;
using EBusinessViewModel.Entities.Post;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;

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

        public async Task AddPostAsync(AddPostVM postVM)
        {
            IFormFile file = postVM.Image;
            string fileName = Guid.NewGuid().ToString() + file.FileName;
            using var stream = new FileStream(Path.Combine(environment.WebRootPath, "assets", "img", "post", fileName), FileMode.Create);
            await file.CopyToAsync(stream);
            await stream.FlushAsync();

            Post post = new Post
            {
                Title = postVM.Title,
                BlogId = postVM.BlogId,
                Description = postVM.Description,
                ImageUrl = fileName
            };

            await unitOfWork.GetRepository<Post>().AddAsync(post);
            await unitOfWork.SaveChangeAsync();
        }

        public Task<EditPostVM> EditPostAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task EditPostPostAsync(int id, EditPostVM postVM)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<Post>> GetAllPostAsync()
        {
            return await dbContext.Posts.Include(p => p.Blog).ToListAsync();
        }

        public async Task RemovePostAsync(int id)
        {
            var postId = await unitOfWork.GetRepository<Post>().GetByIdAsync(id);
            await unitOfWork.GetRepository<Post>().DeleteAsync(postId);
            string filePath = Path.Combine(environment.WebRootPath, "assets", "img", "post", postId.ImageUrl);
            File.Delete(filePath);
            await unitOfWork.SaveChangeAsync();
        }
    }
}
