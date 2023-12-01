using AddressBookAPI.Data.Interfaces;
using AddressBookAPI.Data.Models;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace AddressBookAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AddressBook : ControllerBase
    {
        private readonly IDataService _dataService;
        private readonly ILogger<AddressBook> _logger;
        private readonly IValidator<AddressBookRecord> _validator;
        public AddressBook(IDataService dataService, ILogger<AddressBook> logger, IValidator<AddressBookRecord> validator)
        {
            _dataService = dataService;
            _logger = logger;
            _validator = validator;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var allRecords = await _dataService.GetAllAsync();
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
                var result = await _dataService.SearchAsync(query);
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
                var validation = await _validator.ValidateAsync(record);
                if (!validation.IsValid)
                {
                    return BadRequest(new { message = validation.Errors });
                }
                await _dataService.DeleteAsync(record);
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
                var validation = await _validator.ValidateAsync(record);
                if (!validation.IsValid)
                {
                    return BadRequest(new { message = validation.Errors });
                }
                await _dataService.AddAsync(record);
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
                var validation = await _validator.ValidateAsync(record);
                if (!validation.IsValid)
                {
                    return BadRequest(new { message = validation.Errors });
                }
                await _dataService.UpdateAsync(record);
                return Ok(new { message = $"record updated" });
            }
            catch (Exception ex)
            {
                return Problem(ex.ToString());
            }
        }
    }
}
