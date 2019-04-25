using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Swishark.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SettingsController : Controller
    {
        [HttpGet]
        [Route("account")]
        [Authorize]
        public ActionResult Account()
        {
            return View();
        }

        [HttpGet]
        [Route("project")]
        [Authorize]
        public ActionResult Project()
        {
            return View();
        }
    }
}