using AddressBookAPI.Data.Interfaces;
using AddressBookAPI.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace AddressBookAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AddressBook : ControllerBase
    {
        private readonly IDataService _dataService;
        private readonly ILogger<AddressBook> _logger;

        public AddressBook(IDataService dataService, ILogger<AddressBook> logger)
        {
            _dataService = dataService;
            _logger = logger;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var allRecords = await _dataService.GetAll();
                return Ok(allRecords);
            }
            catch (Exception ex)
            {
                return Problem(ex.ToString());
            }
        }

        [HttpGet]
        [Route("Search")]
        public async Task<IActionResult> Search(string query)
        {
            try
            {
                var result = await _dataService.Search(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Problem(ex.ToString());
            }
        }

        [HttpDelete]
        [Route("DeleteRecord")]
        public async Task<IActionResult> DeleteRecord(AddressBookRecord record)
        {
            try
            {
                await _dataService.Delete(record);
                return Ok(new { message = $"record Id {record.Id} deleted" });
            }
            catch (Exception ex)
            {
                return Problem(ex.ToString());
            }
        }

        [HttpPost]
        [Route("Addrecord")]
        public async Task<IActionResult> AddRecord(AddressBookRecord record)
        {
            try
            {
                await _dataService.Add(record);
                return Ok(new { message = $"record added" });
            }
            catch (Exception ex)
            {
                return Problem(ex.ToString());
            }
        }

        [HttpPut]
        [Route("UpdateRecord")]
        public async Task<IActionResult> UpdateRecord(AddressBookRecord record)
        {
            try
            {
                await _dataService.Update(record);
                return Ok(new { message = $"record updated" });
            }
            catch (Exception ex)
            {
                return Problem(ex.ToString());
            }
        }
    }
}
