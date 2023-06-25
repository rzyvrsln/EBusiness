using EBusinessEntity.Entities;

namespace EBusinessService.Services.Abstraction
{
    public interface IPositionService
    {
        Task AddPositionAsync(Position position);
        Task<ICollection<Position>> GetAllPositions();
    }
}
