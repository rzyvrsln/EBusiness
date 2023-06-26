using EBusinessEntity.Entities;

namespace EBusinessService.Services.Abstraction
{
    public interface IContactService
    {
        Task AddContactAsync(Contact contact);
        Task<ICollection<Contact>> GetAllContactsAsync();
        Task RemoveContactAsync(int id);
        //Task<Contact> GetContactByIdAsync(int id);
    }
}
