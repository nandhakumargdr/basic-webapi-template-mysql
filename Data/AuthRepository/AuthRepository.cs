using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using webapi_basic_mysql.Dtos.Auth;
using webapi_basic_mysql.Models;
using webapi_basic_mysql.Persistence;

namespace webapi_basic_mysql.Data.AuthRepository
{
    public class AuthRepository : IAuthRepository
    {
        private DataContext _dataContext;

        public AuthRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }


        public Task<bool> IsAnAdmin(string userId)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> IsEmailAlreadyExist(string email)
        {
            throw new System.NotImplementedException();
        }

        public async Task<UserLoginResponseDto> Login(UserToLoginDto userToLogin)
        {
            var user = await _dataContext.Users
                    .Where(u => u.Email == userToLogin.Email)
                    .FirstOrDefaultAsync();

            if(user == null)
                return null;
            
            if(!IsCorrectPassword(userToLogin.Password, user.PasswordHash, user.PasswordSalt))
                return null;

            var userLoginResponseDto = new UserLoginResponseDto {
                Id = user.Id,
                Email = user.Email
            };
            
            return userLoginResponseDto;
        }

        public async Task<UserResistrationResponseDto> ResisterUser(UserToRegisterDto userToCreateDto)
        {
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(userToCreateDto.Password, out passwordHash, out passwordSalt);

            var user = new User() {
                Email = userToCreateDto.Email
            };

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            
            _dataContext.Add(user);
            await _dataContext.SaveChangesAsync();

            var userRegistrationResponse = new UserResistrationResponseDto {
                Id = user.Id,
                Email = user.Email
            };

            return userRegistrationResponse;
        }

        public Task<User> UpdatePassword(string userId, string password)
        {
            throw new System.NotImplementedException();
        }

        private bool IsCorrectPassword(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for(int i = 0; i < computedHash.Length; i++){
                    if(computedHash[i] != passwordHash[i])
                        return false;
                }
            }

            return true;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}