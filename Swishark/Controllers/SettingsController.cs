using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Project;

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
        public ActionResult Project(int id)
        {
            if(!new ProjectService().CheckProject(id))
                return RedirectToAction("NotFoundPage", "Error");

            return View();
        }
    }
}