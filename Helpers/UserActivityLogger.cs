using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using webapi_basic_mysql.Data.UserRepository;

namespace webapi_basic_mysql.Helpers
{
    public class UserActivityLogger : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var resultContext = await next();

            try {
                var userId = resultContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                var usersRepository = resultContext.HttpContext.RequestServices.GetService<IUserRepository>();

                await usersRepository.UpdateLastActiveTime(userId);
            } catch {

            }

            
        }
    }
}