using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Enums;
using Microsoft.AspNetCore.Mvc;
using Services.Account;
using Swishark.Models;
using Swishark.Util;

namespace Swishark.Controllers
{
    [Route("[controller]")]
    public class TaskController : ControllerBase
    {
        [HttpGet]
        [Route("add")]
        public HtmlResult Add(PageModel pModel)
        {
            pModel.Num = (int)Pages.Nums.AppTaskAdd;
            return new HtmlResult($"<input class='ds-n' id='pageNum' value='{pModel.Num}'/>");
        }
    }
}