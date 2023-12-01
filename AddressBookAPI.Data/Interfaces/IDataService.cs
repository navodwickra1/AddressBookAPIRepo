using AddressBookAPI.Data.Models;

namespace AddressBookAPI.Data.Interfaces
{
    public interface IDataService
    {
        Task AddAsync(AddressBookRecord record);
        Task DeleteAsync(AddressBookRecord record);
        Task UpdateAsync(AddressBookRecord record);
        Task<IEnumerable<AddressBookRecord>> GetAllAsync();
        Task<IEnumerable<AddressBookRecord>> SearchAsync(string query);
    }
}
