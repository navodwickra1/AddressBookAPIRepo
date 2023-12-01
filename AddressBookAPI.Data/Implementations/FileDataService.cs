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

        public async Task AddAsync(AddressBookRecord record)
        {
            try
            {
                var records = await GetAllAsync();
                List<AddressBookRecord> exisitingRecords = records.ToList();
                var maxId = exisitingRecords.Max(r => r.Id);
                record.Id = maxId + 1;
                exisitingRecords.Add(record);

                exisitingRecords = exisitingRecords.OrderBy(r => r.Id).ToList();
                await _dataRepo.SaveAsync(exisitingRecords);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred while adding records");
            }
        }

        public async Task DeleteAsync(AddressBookRecord record)
        {
            try
            {
                var records = await GetAllAsync();
                var exisitingRecord = records.Where(r => r.Id == record.Id).FirstOrDefault();
                if (exisitingRecord != null)
                {
                    List<AddressBookRecord> exisitingRecords = records.ToList();
                    exisitingRecords.Remove(exisitingRecord);
                    exisitingRecords = exisitingRecords.OrderBy(r => r.Id).ToList();
                    _dataRepo.SaveAsync(exisitingRecords);
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

        public async Task<IEnumerable<AddressBookRecord>> GetAllAsync()
        {
            var retValue = new List<AddressBookRecord>();
            try
            {
                var data = await _dataRepo.GetAllAsync();
                retValue.AddRange(data);
                _logger.LogInformation($"found {data.Count()} records");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred while getting records");
            }
            return retValue;
        }

        public async Task UpdateAsync(AddressBookRecord record)
        {
            try
            {
                var records = await GetAllAsync();
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
                    _dataRepo.SaveAsync((exisitingRecords));
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

        public async Task<IEnumerable<AddressBookRecord>> SearchAsync(string query)
        {
            var retValue = new List<AddressBookRecord>();
            try
            {
                var records = await GetAllAsync();
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
