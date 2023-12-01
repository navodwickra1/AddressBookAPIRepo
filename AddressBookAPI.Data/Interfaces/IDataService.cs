using AddressBookAPI.Data.Models;

namespace AddressBookAPI.Data.Interfaces
{
    public interface IDataService
    {
        Task Add(AddressBookRecord record);
        Task Delete(AddressBookRecord record);
        Task Update (AddressBookRecord record);
        Task<IEnumerable<AddressBookRecord>> GetAll();
        Task<IEnumerable<AddressBookRecord>> Search(string query);
    }
}
