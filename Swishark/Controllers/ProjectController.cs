using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Project;
using Swishark.Models;
using Swishark.Util;

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
        [Route("add")]
        [Authorize]
        public JsonResult Add(PageModel pModel)
        {
            ProjectHelper helper = new ProjectHelper();
            var code = helper.Create(pModel.Data);

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
        public JsonResult GetData(PageModel pModel)
        {
            ProjectService service = new ProjectService();
            Services.Json json = new Services.Json();

            Dictionary<string, string> jData = json.From(pModel.Data);
            int id = Convert.ToInt32(jData["id"]);

            if (service.CheckProject(id))
            {
                return new JsonResult(service.GetProject(id));
            }

            return new JsonResult("");
        }
    }
}