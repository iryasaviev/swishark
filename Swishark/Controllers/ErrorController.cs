using Microsoft.AspNetCore.Mvc;

namespace Swishark.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ErrorController : Controller
    {
        public ActionResult NotFoundPage()
        {
            return View();
        }
    }
}