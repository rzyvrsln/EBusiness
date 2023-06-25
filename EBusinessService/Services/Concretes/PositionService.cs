using EBusinessData.UnitOfWorks;
using EBusinessEntity.Entities;
using EBusinessService.Services.Abstraction;

namespace EBusinessService.Services.Concretes
{
    public class PositionService : IPositionService
    {
        private readonly IUnitOfWork unitOfWork;

        public PositionService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task AddPositionAsync(Position position)
        {
            await unitOfWork.GetRepository<Position>().AddAsync(position);
            await unitOfWork.SaveChangeAsync();
        }

        public async Task<ICollection<Position>> GetAllPositions()
        {
            return await unitOfWork.GetRepository<Position>().GetAllAsync();
        }
    }
}
