
namespace AddressBookAPI.Tests;

public class FileDataServiceTests
{
    private FileDataService _fileDataService;
    private readonly Mock<IDataRepo> _mockDataRepo;
    private readonly Mock<ILogger<FileDataService>> _mockLogger;
    private StringBuilder _jsonData;
    
    public FileDataServiceTests()
    {
        //initiate mocks
        _mockDataRepo = new Mock<IDataRepo>();
        _mockLogger = new Mock<ILogger<FileDataService>>();

        //setup mocks
        _mockLogger.Setup(x => x.Log(LogLevel.Information, 0, It.IsAny<object>(), It.IsAny<Exception>(), It.IsAny<Func<object, Exception, string>>())).Verifiable();

        _mockDataRepo.Setup(d => d.GetAllAsync()).Returns(async () =>
        {
            return await GetDataFromJsonStringAsync();
        });

        _mockDataRepo.Setup(d => d.SaveAsync(It.IsAny<IEnumerable<AddressBookRecord>>())).Callback(
            async (IEnumerable<AddressBookRecord> records) =>
            {
                await ChangeJsonStringDataAsync(JsonConvert.SerializeObject(records));
            });
        
        _fileDataService = new FileDataService(_mockLogger.Object, _mockDataRepo.Object);
        _jsonData = new StringBuilder();
        _jsonData.Append(JsonConvert.SerializeObject(GetInitialRecords()));
    }

    private List<AddressBookRecord> GetInitialRecords()
    {
        var records = new List<AddressBookRecord>();
        records.Add(new AddressBookRecord { Id=1, FirstName = "David", LastName = "Platt", Email = "david.platt@corrie.co.uk", Phone = "01913478234" });
        records.Add(new AddressBookRecord { Id=2, FirstName = "Jason", LastName = "Grimshaw", Email = "jason.grimshaw@corrie.co.uk", Phone = "01913478123" });
        records.Add(new AddressBookRecord { Id=3, FirstName = "Navod", LastName = "Barlow", Email = "ken.barlow@corrie.co.uk", Phone = "019134784929" });
        records.Add(new AddressBookRecord { Id=4, FirstName = "Ken", LastName = "Sullivan", Email = "rita.sullivan@corrie.co.uk", Phone = "01913478555" });
        records.Add(new AddressBookRecord { Id=5, FirstName = "Steve", LastName = "McDonald", Email = "steve.mcdonald@corrie.co.uk", Phone = "01913478555" });
        return records;
    }

    [Fact]
    public async Task Adding_new_record_works_successfully()
    {
        await _fileDataService.AddAsync(new AddressBookRecord {FirstName = "Mark", LastName = "Nathan", Email = "navodwickra@gmail.com", Phone = "07912040290"});

        var modifiedData = await _fileDataService.GetAllAsync();

        Assert.Equal(6, modifiedData.Count());
        Assert.NotNull(modifiedData.Where(m=>m.FirstName == "Mark").FirstOrDefault());
    }

    [Fact]
    public async Task Deleting_exisitng_record_works_successfully()
    {
        await _fileDataService.DeleteAsync(GetInitialRecords()[0]);

        var modifiedData = await _fileDataService.GetAllAsync();

        Assert.Equal(4, modifiedData.Count());
        Assert.Null(modifiedData.Where(m => m.FirstName == "David").FirstOrDefault());
    }

    [Fact]
    public async Task Updating_exisitng_record_works_successfully()
    {
        var recordToBeModified = GetInitialRecords()[0];
        recordToBeModified.FirstName = "Peter";
        await _fileDataService.UpdateAsync(recordToBeModified);

        var modifiedData = await _fileDataService.GetAllAsync();

        Assert.Equal(5, modifiedData.Count());
        Assert.NotNull(modifiedData.Where(m => m.FirstName == "Peter").FirstOrDefault());
    }

    [Fact]
    public async Task Getting_all_records_works_successfully()
    {
        var allRecords = await _mockDataRepo.Object.GetAllAsync();

        Assert.Equal(5, allRecords.Count());
        Assert.NotNull(allRecords.Where(m => m.FirstName == "David").FirstOrDefault());
    }

    private async Task ChangeJsonStringDataAsync(string data)
    {
        _jsonData.Clear();
        _jsonData.Append(data);
    }

    private async Task<IEnumerable<AddressBookRecord>> GetDataFromJsonStringAsync()
    {
        var records = _jsonData.ToString();
        return JsonConvert.DeserializeObject<IEnumerable<AddressBookRecord>>(records);
    }
}