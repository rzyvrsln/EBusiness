using EBusinessEntity.Entities;
using EBusinessViewModel.Entities.Employee;
using EBusinessViewModel.Entities.Pagination;

namespace EBusinessService.Services.Abstraction
{
    public interface IEmployeeService
    {
        Task AddEmployeeAsync(AddEmployeeVM employeeVM);
        Task<ICollection<Employee>> GetAllEmployeeAsync();
        Task RemoveEmployeeAsync(int id);
        Task<UpdateEmployeeVM> EditEmployeeAsync(int id);
        Task EditPostEmployeeAsync(int id, UpdateEmployeeVM employeeVM);
        Task<PaginationVM<Employee>> PaginationForEmployeeAsync(int page = 1);
    }
}
