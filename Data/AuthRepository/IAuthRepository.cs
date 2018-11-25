using System.Threading.Tasks;
using webapi_basic_mysql.Dtos.Auth;
using webapi_basic_mysql.Models;

namespace webapi_basic_mysql.Data.AuthRepository
{
    public interface IAuthRepository
    {
        Task<bool> IsAnAdmin(string userId);
        Task<bool> IsEmailAlreadyExist(string email);
        Task<UserResistrationResponseDto> ResisterUser(UserToRegisterDto userToCreateDto);
        Task<UserLoginResponseDto> Login(UserToLoginDto userToLoginDto);
        Task<User> UpdatePassword(string userId, string password);
    }
}