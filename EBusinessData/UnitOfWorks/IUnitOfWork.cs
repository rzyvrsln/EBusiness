using EBusinessData.Repositories;

namespace EBusinessData.UnitOfWorks
{
    public interface IUnitOfWork
    {
        IRepository<T> GetRepository<T>() where T : class, new();
        Task SaveChangeAsync();
    }
}
