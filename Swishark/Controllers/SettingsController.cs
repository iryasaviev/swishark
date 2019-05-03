using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Swishark.Controllers
{
    [Route("[controller]")]
    public class SettingsController : Controller
    {
        [HttpGet]
        [Route("account/{id:int}")]
        [Authorize]
        public ActionResult Account()
        {
            return View();
        }

        [HttpGet]
        [Route("project/{id:int}")]
        [Authorize]
        public ActionResult Project()
        {
            return View();
        }
    }
}