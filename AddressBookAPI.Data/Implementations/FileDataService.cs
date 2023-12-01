using AddressBookAPI.Data.Interfaces;
using AddressBookAPI.Data.Models;
using Microsoft.Extensions.Logging;

namespace AddressBookAPI.Data.Implementations
{
    public class FileDataService : IDataService
    {
        private readonly ILogger<FileDataService> _logger;
        private readonly IDataRepo _dataRepo;

        public FileDataService(ILogger<FileDataService> logger, IDataRepo dataRepo)
        {
            _logger = logger;
            _dataRepo = dataRepo;
        }

        public async Task Add(AddressBookRecord record)
        {
            try
            {
                var records = await GetAll();
                List<AddressBookRecord> exisitingRecords = records.ToList();
                var maxId = exisitingRecords.Max(r => r.Id);
                record.Id = maxId + 1;
                exisitingRecords.Add(record);

                exisitingRecords = exisitingRecords.OrderBy(r => r.Id).ToList();
                await _dataRepo.Save(exisitingRecords);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred while adding records");
            }
        }

        public async Task Delete(AddressBookRecord record)
        {
            try
            {
                var records = await GetAll();
                var exisitingRecord = records.Where(r => r.Id == record.Id).FirstOrDefault();
                if (exisitingRecord != null)
                {
                    List<AddressBookRecord> exisitingRecords = records.ToList();
                    exisitingRecords.Remove(exisitingRecord);
                    exisitingRecords = exisitingRecords.OrderBy(r => r.Id).ToList();
                    _dataRepo.Save(exisitingRecords);
                    _logger.LogInformation($"Record with Id {record.Id} has been deleted");
                }
                else
                {
                    _logger.LogWarning($"Record with Id {record.Id} not found");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred while deleting records");
            }
        }

        public async Task<IEnumerable<AddressBookRecord>> GetAll()
        {
            var retValue = new List<AddressBookRecord>();
            try
            {
                var data = await _dataRepo.GetAll();
                retValue.AddRange(data);
                _logger.LogInformation($"found {data.Count()} records");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred while getting records");
            }
            return retValue;
        }

        public async Task Update(AddressBookRecord record)
        {
            try
            {
                var records = await GetAll();
                var exisitingRecord = records.Where(r => r.Id == record.Id).FirstOrDefault();
                if (exisitingRecord != null)
                {
                    List<AddressBookRecord> exisitingRecords = records.ToList();
                    exisitingRecords.Remove(exisitingRecord);

                    exisitingRecord.FirstName = record.FirstName;
                    exisitingRecord.LastName = record.LastName;
                    exisitingRecord.Email = record.Email;
                    exisitingRecord.Phone = record.Phone;

                    exisitingRecords.Add(exisitingRecord);
                    exisitingRecords = exisitingRecords.OrderBy(r => r.Id).ToList();
                    _dataRepo.Save((exisitingRecords));
                    _logger.LogInformation($"Record with Id {record.Id} has been deleted");
                }
                else
                {
                    _logger.LogWarning($"Record with Id {record.Id} not found");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred while updating records");
            }
        }

        public async Task<IEnumerable<AddressBookRecord>> Search(string query)
        {
            var retValue = new List<AddressBookRecord>();
            try
            {
                var records = await GetAll();
                retValue = records.Where(r => r.Email.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                                              r.FirstName.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                                              r.LastName.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                                              r.Phone.Contains(query, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred while searching for records");
            }

            return retValue;
        }


    }
}
