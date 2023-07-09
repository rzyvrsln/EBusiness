using EBusinessData.DAL;
using EBusinessData.UnitOfWorks;
using EBusinessEntity.Entities;
using EBusinessService.Services.Abstraction;
using EBusinessViewModel.Entities.Pagination;
using Microsoft.EntityFrameworkCore;

namespace EBusinessService.Services.Concretes
{
    public class PositionService : IPositionService
    {
        private readonly IUnitOfWork unitOfWork;
        AppDbContext DbContext { get; set; }

        public PositionService(IUnitOfWork unitOfWork, AppDbContext dbContext)
        {
            this.unitOfWork = unitOfWork;
            DbContext = dbContext;
        }

        public async Task AddPositionAsync(Position position)
        {
            await unitOfWork.GetRepository<Position>().AddAsync(position);
            await unitOfWork.SaveChangeAsync();
        }


        public async Task<ICollection<Position>> GetAllPositionsAsync()
        {
            return await unitOfWork.GetRepository<Position>().GetAllAsync();
        }

        public async Task RemovePositionAsync(int id)
        {
            var positionId = await unitOfWork.GetRepository<Position>().GetByIdAsync(id);
            if (positionId != null)
            {
                await unitOfWork.GetRepository<Position>().DeleteAsync(positionId);
                await unitOfWork.SaveChangeAsync();
            }
        }

        public async Task<Position> EditPositionAsync(int id)
        {
            var positionId = await unitOfWork.GetRepository<Position>().GetByIdAsync(id);
            if (positionId != null)
            {
                return positionId;
            }
            return positionId;
        }

        public async Task EditPositionPostAsync(int id, Position position)
        {
            var positionId = await unitOfWork.GetRepository<Position>().GetByIdAsync(id);
            if (positionId != null)
            {
                positionId.Name = position.Name;
                positionId.UpdateAt = DateTime.Now;

                await unitOfWork.GetRepository<Position>().UpdatedAsync(positionId);
                await unitOfWork.SaveChangeAsync();
            }
        }

        public async Task<PaginationVM<Position>> PaginationForPositionAsync(int page = 1)
        {
            PaginationVM<Position> paginationVM = new PaginationVM<Position>();
            paginationVM.MaxPageCount = (int)Math.Ceiling((decimal)DbContext.Positions.Count() / 5);
            paginationVM.CurrentPage = page;
            if (page > paginationVM.MaxPageCount || page < 1) return null;
            paginationVM.Items = await DbContext.Positions.Skip((page - 1) * 5).Take(5).ToListAsync();
            return paginationVM;
        }
    }
}
