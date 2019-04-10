using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Enums;
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
        public ActionResult Add(PageModel pModel)
        {
            return View();
        }

        [HttpGet]
        [Route("index")]
        public ActionResult Index(PageModel pModel)
        {
            return View();
        }

        //[HttpGet]
        //[Route("add")]
        //public HtmlResult Add(PageModel pModel)
        //{
        //    pModel.Num = (int)Pages.Nums.AppProjectAdd;
        //    return new HtmlResult($"<input class='ds-n' id='pageNum' value='{pModel.Num}'/>");
        //}

        //[HttpPost]
        //[Route("add")]
        //public JsonResult Add(PageModel pModel, string s)
        //{
        //    ProjectHelper helper = new ProjectHelper();
        //    var code = helper.Create(pModel.Data);

        //    return new JsonResult(code);
        //}
    }
}