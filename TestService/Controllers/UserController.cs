using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestService.Models;


namespace TestService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _context; 
        private readonly IWebHostEnvironment _hostingEnvironment;
        public UserController(ApplicationDbContext context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;

        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpPost("insert")]
        public async Task<IActionResult> InsertUserData()
        {
            try
            {
                var filePath = Path.Combine(_hostingEnvironment.ContentRootPath, "data.txt");

                var userDataList = DataInsertionSimulator.ReadUserDataFromFile(filePath);

                foreach (var userData in userDataList)
                {
                    var existingURN = await _context.UserData.AnyAsync(u => u.URN == userData.URN);

                    if (existingURN)
                    {
                        return BadRequest($"URN {userData.URN} already exists. Please initiate a new URN for the request.");
                    }
                    userData.EncryptedBankAccountNumber = AesEncryptionHelper.Encrypt(userData.EncryptedBankAccountNumber);
                    await _context.UserData.AddAsync(userData);
                }

                await _context.SaveChangesAsync();

                return Ok("Data inserted successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }
}
