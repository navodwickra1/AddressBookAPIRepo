using AddressBookAPI.Data.Interfaces;
using AddressBookAPI.Data.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AddressBookAPI.Data.Implementations
{
    public class FileDataRepo : IDataRepo
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<FileDataRepo> _logger;
        private readonly string _fileName;
        public FileDataRepo(IConfiguration configuration, ILogger<FileDataRepo> logger)
        {
            _configuration = configuration;
            _logger = logger;
            _fileName = _configuration.GetValue<string>("DataFileName");
        }
        
        public async Task<IEnumerable<AddressBookRecord>> GetAll()
        {
            var retValue = new List<AddressBookRecord>();
            var data = await ReadJsonFile();
            if (!string.IsNullOrEmpty(data))
            {
                retValue.AddRange(JsonConvert.DeserializeObject<IEnumerable<AddressBookRecord>>(data));
            }
            return retValue;
        }

        public async Task Save(IEnumerable<AddressBookRecord> records)
        {
            var recordsString = JsonConvert.SerializeObject(records);
            await WriteJsonData(recordsString);
        }
        
        private async Task<string> ReadJsonFile()
        {
            var retValue = string.Empty;
            if (!string.IsNullOrEmpty(_fileName))
            {
                if (File.Exists(_fileName))
                {
                    retValue = await File.ReadAllTextAsync(_configuration.GetValue<string>("DataFileName"));
                }
                else
                {
                    _logger.LogError($"Data file not found : {_fileName}");
                }
            }
            return retValue;
        }

        private async Task WriteJsonData(string data)
        {
            if (File.Exists(_fileName))
            {
                await File.WriteAllTextAsync(_configuration.GetValue<string>("DataFileName"), data);
            }
            else
            {
                 _logger.LogError($"Data file not found : {_fileName}");
            }
        }

    }
}
