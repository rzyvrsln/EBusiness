using EBusinessData.DAL;
using EBusinessData.UnitOfWorks;
using EBusinessEntity.Entities;
using EBusinessService.Services.Abstraction;
using EBusinessViewModel.Entities.Employee;
using EBusinessViewModel.Entities.Pagination;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace EBusinessService.Services.Concretes
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IHostingEnvironment environment;
        private readonly AppDbContext dbContext;

        public EmployeeService(IUnitOfWork unitOfWork, IHostingEnvironment environment, AppDbContext dbContext)
        {
            this.unitOfWork = unitOfWork;
            this.environment = environment;
            this.dbContext = dbContext;
        }

        public async Task AddEmployeeAsync(AddEmployeeVM employeeVM)
        {
            IFormFile file = employeeVM.Image;
            string fileName = Guid.NewGuid().ToString() + file.FileName;
            using var stream = new FileStream(Path.Combine(environment.WebRootPath, "assets", "img", "employee", fileName), FileMode.Create);
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
            return await dbContext.Employees.Include(e => e.Position).ToListAsync();
        }

        public async Task RemoveEmployeeAsync(int id)
        {
            var employeeId = await unitOfWork.GetRepository<Employee>().GetByIdAsync(id);
            await unitOfWork.GetRepository<Employee>().DeleteAsync(employeeId);
            string filePath = Path.Combine(environment.WebRootPath, "assets", "img", "employee", employeeId.ImageUrl);
            File.Delete(filePath);
            await unitOfWork.SaveChangeAsync();
        }

        public async Task<UpdateEmployeeVM> EditEmployeeAsync(int id)
        {
            var employeeId = await unitOfWork.GetRepository<Employee>().GetByIdAsync(id);

            UpdateEmployeeVM vM = new UpdateEmployeeVM
            {
                Name = employeeId.Name,
                Surname = employeeId.Surname,
                Linkedin = employeeId.Linkedin,
                Instagram = employeeId.Instagram,
                Twitter = employeeId.Twitter,
                PositionId = employeeId.PositionId,
            };

            return vM;

        }

        public async Task EditPostEmployeeAsync(int id, UpdateEmployeeVM employeeVM)
        {
            var employeeId = await unitOfWork.GetRepository<Employee>().GetByIdAsync(id);

            if(employeeId != null)
            {

                IFormFile file = employeeVM.Image;
                string fileName = Guid.NewGuid().ToString() + file.FileName;
                using var stream = new FileStream(Path.Combine(environment.WebRootPath,"assets","img","employee",fileName),FileMode.Create);
                await file.CopyToAsync(stream);
                await stream.FlushAsync();

                employeeId.Name = employeeVM.Name;
                employeeId.Surname = employeeVM.Surname;
                employeeId.Linkedin = employeeVM.Linkedin;
                employeeId.Instagram = employeeVM.Instagram;
                employeeId.Twitter = employeeVM.Twitter;
                employeeId.PositionId = employeeVM.PositionId;
                employeeId.UpdateAt = DateTime.Now;
                employeeId.ImageUrl = fileName;

                await unitOfWork.GetRepository<Employee>().UpdatedAsync(employeeId);
                await unitOfWork.SaveChangeAsync();
            }
        }

        public async Task<PaginationVM<Employee>> PaginationForEmployeeAsync(int page = 1)
        {
            PaginationVM<Employee> paginationVM = new PaginationVM<Employee>();
            paginationVM.MaxPageCount = (int)Math.Ceiling((double)dbContext.Employees.Count() / 5);
            paginationVM.CurrentPage = page;
            if (page > paginationVM.MaxPageCount || page < 1) return null;
            paginationVM.Items = await dbContext.Employees.Skip((page - 1) * 5).Take(5).ToListAsync();

            return paginationVM;
        }
    }
}
