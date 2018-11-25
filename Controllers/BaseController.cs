using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using webapi_basic_mysql.Helpers;

namespace webapi_basic_mysql.Controllers
{
    [ApiController]
    [Authorize]
    [ServiceFilter(typeof(UserActivityLogger))]
    [Route("api/[controller]")]
    public class BaseController : ControllerBase
    {
        
    }
}