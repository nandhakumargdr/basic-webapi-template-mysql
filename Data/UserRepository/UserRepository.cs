using System;
using System.Linq;
using System.Threading.Tasks;
using webapi_basic_mysql.Models;
using webapi_basic_mysql.Persistence;

namespace webapi_basic_mysql.Data.UserRepository
{
    public class UserRepository : IUserRepository
    {
        private DataContext _dataContext;
        public UserRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public Task<User> GetUser(string id)
        {
            return null;
        }

        public async Task UpdateLastActiveTime(string userId)
        {
            var now = DateTime.UtcNow;
            var user = _dataContext.Users.Where(u => u.Id == userId).First();
            if(user != null) {
                user.LastActive = now;
            }

            await _dataContext.SaveChangesAsync();
        }
    }
}