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
    }
}
