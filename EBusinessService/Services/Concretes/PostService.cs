using EBusinessData.DAL;
using EBusinessData.UnitOfWorks;
using EBusinessEntity.Entities;
using EBusinessService.Services.Abstraction;
using EBusinessViewModel.Entities.Pagination;
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

        public async Task<EditPostVM> EditPostAsync(int id)
        {
            var postId = await unitOfWork.GetRepository<Post>().GetByIdAsync(id);
            EditPostVM postVM = new EditPostVM
            {
                Title = postId.Title,
                BlogId = postId.BlogId,
                Description = postId.Description,
            };

            return postVM;
        }

        public async Task EditPostPostAsync(int id, EditPostVM postVM)
        {
            var postId = await unitOfWork.GetRepository<Post>().GetByIdAsync(id);

            if(postVM is not null)
            {
                IFormFile file = postVM.Image;
                string fileName = Guid.NewGuid().ToString() + file.FileName;
                using var stream = new FileStream(Path.Combine(environment.WebRootPath, "assets", "img", "post", fileName), FileMode.Create);
                await file.CopyToAsync(stream);
                await stream.FlushAsync();

                postId.Title = postVM.Title;
                postId.BlogId = postVM.BlogId;
                postId.Description = postVM.Description;
                postId.ImageUrl = fileName;

                await unitOfWork.GetRepository<Post>().UpdatedAsync(postId);
                await unitOfWork.SaveChangeAsync();
            }
        }

        public async Task<ICollection<Post>> GetAllPostAsync()
        {
            return await dbContext.Posts.Include(p => p.Blog).ToListAsync();
        }

        public async Task<Post> GetPostByIdAsync(int id)
        {
            var postId = await unitOfWork.GetRepository<Post>().GetByIdAsync(id);
            if(postId is not null)
            {
                return postId;
            }

            return postId;
        }

        public async Task RemovePostAsync(int id)
        {
            var postId = await unitOfWork.GetRepository<Post>().GetByIdAsync(id);
            await unitOfWork.GetRepository<Post>().DeleteAsync(postId);
            string filePath = Path.Combine(environment.WebRootPath, "assets", "img", "post", postId.ImageUrl);
            File.Delete(filePath);
            await unitOfWork.SaveChangeAsync();
        }

        public async Task<PaginationVM<Post>> PaginationForPostAsync(int page)
        {
            PaginationVM<Post> paginationVM = new PaginationVM<Post>();
            paginationVM.MaxPageCount = (int)Math.Ceiling((decimal)dbContext.Posts.Count() / 5);
            paginationVM.CurrentPage = page;
            if (paginationVM.CurrentPage > paginationVM.MaxPageCount || page < 1) return null;
            paginationVM.Items = await dbContext.Posts.Skip((page - 1) * 5).Take(5).Include(p => p.Blog).ToListAsync();
            return paginationVM;
        }
    }
}
