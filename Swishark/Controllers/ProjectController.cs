using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Account;
using Services.Project;
using Services.ProjectTask;
using Swishark.Models;

namespace Swishark.Controllers
{
    [Route("[controller]")]
    public class ProjectController : Controller
    {
        [HttpGet]
        [Route("add")]
        [Authorize]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [Route("api/AddItem")]
        [Authorize]
        public JsonResult Add(PageModel pModel)
        {
            ProjectHelper helper = new ProjectHelper();
            AccountService aService = new AccountService();

            var code = helper.Create(pModel.Data, aService.GetCurrentUser(User.Identity.Name));

            return new JsonResult(code);
        }

        [HttpGet]
        [Route("{id:int}")]
        [Authorize]
        public ActionResult Index(int id)
        {
            ProjectService service = new ProjectService();
            if (!service.CheckProject(id))
            {
                return RedirectToAction("NotFoundPage", "Error");
            }

            return View();
        }

        [HttpPost]
        [Route("api/GetItem")]
        [Authorize]
        public JsonResult GetItem(PageModel pModel)
        {
            ProjectService service = new ProjectService();
            Services.Json json = new Services.Json();

            Dictionary<string, string> jData = json.From(pModel.Data);
            int id = Convert.ToInt32(jData["id"]);

            ProjectHelper helper = new ProjectHelper();
            
            if (service.CheckProject(id))
            {
                return new JsonResult(helper.Get(id));
            }
            else
            {
                // TODO: Сделать вывод номера ошибки.
                return new JsonResult("Такого проекта не существует.");
            }
        }
    }
}