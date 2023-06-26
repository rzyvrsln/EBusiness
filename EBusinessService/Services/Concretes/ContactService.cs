using EBusinessData.UnitOfWorks;
using EBusinessEntity.Entities;
using EBusinessService.Services.Abstraction;

namespace EBusinessService.Services.Concretes
{
    public class ContactService : IContactService
    {
        private readonly IUnitOfWork unitOfWork;

        public ContactService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task AddContactAsync(Contact contact)
        {
            await unitOfWork.GetRepository<Contact>().AddAsync(contact);
            await unitOfWork.SaveChangeAsync();
        }

        public async Task<ICollection<Contact>> GetAllContactsAsync()
        {
            return await unitOfWork.GetRepository<Contact>().GetAllAsync();
        }

        public async Task RemoveContactAsync(int id)
        {
            var contactId = await unitOfWork.GetRepository<Contact>().GetByIdAsync(id);
            await unitOfWork.GetRepository<Contact>().DeleteAsync(contactId);
            await unitOfWork.SaveChangeAsync();
        }

        //public async Task<Contact> GetContactByIdAsync(int id)
        //{
        //    return await unitOfWork.GetRepository<Contact>().GetByIdAsync(id);
        //}
    }
}
