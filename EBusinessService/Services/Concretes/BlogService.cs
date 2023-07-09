using EBusinessData.DAL;
using EBusinessData.UnitOfWorks;
using EBusinessEntity.Entities;
using EBusinessService.Services.Abstraction;
using EBusinessViewModel.Entities.Pagination;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace EBusinessService.Services.Concretes
{
    public class BlogService : IBlogService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly AppDbContext dbContext;

        public BlogService(IUnitOfWork unitOfWork, AppDbContext dbContext)
        {
            this.unitOfWork = unitOfWork;
            this.dbContext = dbContext;
        }

        public async Task AddBlogAsync(Blog blog)
        {
            await unitOfWork.GetRepository<Blog>().AddAsync(blog);
            await unitOfWork.SaveChangeAsync();
        }

        public async Task<Blog> EditBlogAsync(int id)
        {
            var blogId = await unitOfWork.GetRepository<Blog>().GetByIdAsync(id);
            if (blogId != null)
            {
                return blogId;
            }
            return blogId;
        }

        public async Task EditPostBlogAsync(int id, Blog blog)
        {
            var blogId = await unitOfWork.GetRepository<Blog>().GetByIdAsync(id);
            if (blogId != null)
            {
                blogId.Name = blog.Name;
                blogId.UpdateAt = DateTime.Now;
                await unitOfWork.GetRepository<Blog>().UpdatedAsync(blogId);
                await unitOfWork.SaveChangeAsync();
            }

        }

        public async Task<ICollection<Blog>> GetAllBlogsAsync()
        {
            return await unitOfWork.GetRepository<Blog>().GetAllAsync();
        }

        public async Task RemoveBlogAsync(int id)
        {
            var blogId = await unitOfWork.GetRepository<Blog>().GetByIdAsync(id);
            await unitOfWork.GetRepository<Blog>().DeleteAsync(blogId);
            await unitOfWork.SaveChangeAsync();
        }

        public async Task<PaginationVM<Blog>> PaginationForBlogAsync(int page)
        {
            PaginationVM<Blog> paginationVM = new PaginationVM<Blog>();
            paginationVM.MaxPageCount = (int)Math.Ceiling((decimal)dbContext.Blogs.Count() / 5);
            paginationVM.CurrentPage = page;
            if (paginationVM.CurrentPage > paginationVM.MaxPageCount || page < 1) return null;
            paginationVM.Items = await dbContext.Blogs.Skip((page - 1) * 5).Take(5).ToListAsync();

            return paginationVM;

        }
    }
}
