using System.Threading.Tasks;
using webapi_basic_mysql.Models;

namespace webapi_basic_mysql.Data.UserRepository
{
    public interface IUserRepository
    {
         Task<User> GetUser(string id);
         Task UpdateLastActiveTime(string userId);
    }
}