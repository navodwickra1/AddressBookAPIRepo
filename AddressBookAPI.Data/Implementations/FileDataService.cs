using AddressBookAPI.Data.Interfaces;
using AddressBookAPI.Data.Models;

namespace AddressBookAPI.Data.Implementations
{
    public class FileDataService : IDataService
    {
        public Task Add(AddressBookRecord record)
        {
            throw new NotImplementedException();
        }

        public Task Delete(AddressBookRecord record)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<AddressBookRecord>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task Update(AddressBookRecord record)
        {
            throw new NotImplementedException();
        }
    }
}
