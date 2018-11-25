using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using webapi_basic_mysql.Data.AuthRepository;
using webapi_basic_mysql.Dtos.Auth;
using webapi_basic_mysql.Helpers;

namespace webapi_basic_mysql.Controllers
{

    public class AuthController : BaseController
    {
        private IAuthRepository _authRepo;
        private IConfiguration _config;

        public AuthController(IAuthRepository authRepo, IConfiguration config)
        {
            _authRepo = authRepo;
            _config = config;
        }

        [HttpPost("registerUser")]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterUser([FromBody] UserToRegisterDto userToRegisterDto) {
            var userResistrationResponse = await _authRepo.ResisterUser(userToRegisterDto);
            return Ok(userResistrationResponse);
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] UserToLoginDto userToLoginDto) {
            var userLoginResponse = await _authRepo.Login(userToLoginDto);
            if (userLoginResponse == null)
                return Unauthorized();
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_config.GetSection("AppSettings:EncryptionKey").Value);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, userLoginResponse.Id.ToString()),
                    new Claim(ClaimTypes.Name, userLoginResponse.Email)
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha512Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            userLoginResponse.RefreshToken = tokenString;
            return Ok(userLoginResponse);
        }

        [HttpPut("ula")]
        [AllowAnonymous]
        public IActionResult UpdateLastActiveTime() {
            return Ok();
        }

    }
}