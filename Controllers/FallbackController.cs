using System.IO;
using Microsoft.AspNetCore.Mvc;

namespace webapi_basic_mysql.Controllers
{
    public class FallbackController : Controller
    {
        public IActionResult Index () {
            return PhysicalFile(Path.Combine (Directory.GetCurrentDirectory (), "wwwroot", "index.html"), "text/HTML");
        }
    }
}