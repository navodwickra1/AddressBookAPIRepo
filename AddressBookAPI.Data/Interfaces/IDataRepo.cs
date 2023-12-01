using AddressBookAPI.Data.Models;

namespace AddressBookAPI.Data.Interfaces;

public interface IDataRepo
{
    Task<IEnumerable<AddressBookRecord>> GetAll();
    Task Save(IEnumerable<AddressBookRecord> records);
    
}