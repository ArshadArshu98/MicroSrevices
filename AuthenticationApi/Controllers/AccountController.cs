using JWTAuthentication;
using JWTAuthentication.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly JwtTokenHandler _jwtTokenHandler;
        private readonly UserAccountsDbContext _context;
        public AccountController(JwtTokenHandler jwtTokenHandler, UserAccountsDbContext context)
        {
            _jwtTokenHandler = jwtTokenHandler;
            _context = context;
        }
        [HttpPost]
        public ActionResult<AuthenticationResponse> Authenticate([FromBody] AuthenticationRequest authenticationRequest)
        {
            if (string.IsNullOrWhiteSpace(authenticationRequest.UserName) || string.IsNullOrWhiteSpace(authenticationRequest.Password))
                return Unauthorized();
            var userAccount = _context.UserLogin.Where(x => x.UserName == authenticationRequest.UserName && x.Password == authenticationRequest.Password).FirstOrDefault();
            if (userAccount == null) return Unauthorized();

            var autheticationResponse = _jwtTokenHandler.GenerateJwtToken(authenticationRequest);
            if (autheticationResponse == null) return Unauthorized();
            return autheticationResponse;
        }
    }
}
