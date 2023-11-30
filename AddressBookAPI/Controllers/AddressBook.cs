using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AddressBookAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AddressBook : ControllerBase
    {
        [HttpGet]
        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            return Ok(new {result = "good"});
        }
    }
}
