using EBusinessData.DAL;
using EBusinessData.Repositories;
using EBusinessEntity.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EBusinessData.UnitOfWorks
{
    public class UnitOfWrok : IUnitOfWork
    {
        private readonly AppDbContext dbContext;

        public UnitOfWrok(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IRepository<T> GetRepository<T>() where T : class, new()
        {
            return new Repository<T>(dbContext);
        }

        public async Task SaveChangeAsync()
        {
            await dbContext.SaveChangesAsync();
        }
    }
}
