using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Account;
using Services.Settings;
using Swishark.Models;

namespace Swishark.Controllers
{
    [Route("[controller]")]
    public class SettingsController : Controller
    {
        [HttpGet]
        [Route("account")]
        [Authorize]
        public ActionResult Account()
        {
            return View();
        }

        [HttpPost]
        [Route("api/account/Update")]
        [Authorize]
        public JsonResult Account(PageModel pModel)
        {
            return new JsonResult("");
        }

        [HttpGet]
        [Route("project/{id:int}")]
        [Authorize]
        public ActionResult Project()
        {
            return View();
        }

        [HttpPost]
        [Route("api/project/{id:int}/Update")]
        [Authorize]
        public JsonResult Project(PageModel pModel)
        {
            SettingsHelper helper = new SettingsHelper();
            helper.ProjectUpdate(pModel.Data, new AccountService().GetCurrentUser(User.Identity.Name));

            return new JsonResult("");
        }
    }
}