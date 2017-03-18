using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;


namespace SF.Module.Demo.Controllers
{
    [Route("Demo")]
    public class DemoController : Controller
    {

        public DemoController()
        {

        }

        public IActionResult Index()
        {

            return View();
        }
 

    }
}
