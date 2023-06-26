using EBusinessEntity.Entities;
using EBusinessViewModel.Entities.Employee;

namespace EBusinessService.Services.Abstraction
{
    public interface IEmployeeService
    {
        Task AddEmployeeAsync(AddEmployeeVM employeeVM);
        Task<ICollection<Employee>> GetAllEmployeeAsync();

    }
}
