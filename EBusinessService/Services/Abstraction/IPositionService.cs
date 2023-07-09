using EBusinessEntity.Entities;
using EBusinessViewModel.Entities.Pagination;

namespace EBusinessService.Services.Abstraction
{
    public interface IPositionService
    {
        Task AddPositionAsync(Position position);
        Task<ICollection<Position>> GetAllPositionsAsync();
        Task RemovePositionAsync(int id);
        Task<Position> EditPositionAsync(int id);
        Task EditPositionPostAsync(int id, Position position);
        Task<PaginationVM<Position>> PaginationForPositionAsync(int page = 1);
    }
}
