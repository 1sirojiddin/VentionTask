using CsvHelper;
using CsvProject3.Data;
using CsvProject3.Models;
using CsvProject3.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace CsvProject3.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CsvController : Controller
    {
        private readonly UserService _userService;
        private readonly DataContext _dataContext;
        public CsvController(UserService userService, DataContext dataContext)
        {
            _userService = userService;
            _dataContext = dataContext;
        }
        
        [HttpPost("parse-file")]
        public async Task<IActionResult> UploadExcel(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("File is empty.");

            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                stream.Position = 0;

                await _userService.ProcessExcelFile(stream);
            }

            return Ok("File processed successfully.");
        }
        [HttpGet("Sorting")]
        public IActionResult GetUsersSortedByUsername([FromQuery] bool isAscending = true)
        {
            var sortedUsers = _userService.GetUsersSortedByUsername(isAscending);
            return Ok(sortedUsers);
        }
        [HttpGet("Limitations")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers(int maxCount = 10)
        {
            var users = await _dataContext.Users.Take(maxCount).ToListAsync();
            return Ok(users);
        }
    }
}
