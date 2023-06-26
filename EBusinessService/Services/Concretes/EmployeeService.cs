using EBusinessData.DAL;
using EBusinessData.UnitOfWorks;
using EBusinessEntity.Entities;
using EBusinessService.Services.Abstraction;
using EBusinessViewModel.Entities.Employee;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
namespace EBusinessService.Services.Concretes
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IHostingEnvironment environment;

        public EmployeeService(IUnitOfWork unitOfWork, IHostingEnvironment environment)
        {
            this.unitOfWork = unitOfWork;
            this.environment = environment;
        }

        public async Task AddEmployeeAsync(AddEmployeeVM employeeVM)
        {
            IFormFile file = employeeVM.Image;
            string fileName = Guid.NewGuid().ToString() + file.FileName;
            using var stream = new FileStream(Path.Combine(environment.WebRootPath, "assets","img", "employee", fileName), FileMode.Create);
            await file.CopyToAsync(stream);
            await stream.FlushAsync();

            Employee employee = new Employee
            {
                Name = employeeVM.Name,
                Surname = employeeVM.Surname,
                Linkedin = employeeVM.Linkedin,
                Instagram = employeeVM.Instagram,
                Twitter = employeeVM.Twitter,
                PositionId = employeeVM.PositionId,
                ImageUrl = fileName
            };

            await unitOfWork.GetRepository<Employee>().AddAsync(employee);
            await unitOfWork.SaveChangeAsync();
            
        }

        public async Task<ICollection<Employee>> GetAllEmployeeAsync()
        {
            return await unitOfWork.GetRepository<Employee>().GetAllAsync();
        }
    }
}
