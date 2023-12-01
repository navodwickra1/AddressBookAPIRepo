using AddressBookAPI.Data.Models;

namespace AddressBookAPI.Data.Interfaces;

public interface IDataRepo
{
    Task<IEnumerable<AddressBookRecord>> GetAllAsync();
    Task SaveAsync(IEnumerable<AddressBookRecord> records);
    
}