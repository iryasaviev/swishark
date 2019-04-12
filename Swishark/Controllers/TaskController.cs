﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Account;
using Services.ProjectTask;
using Swishark.Models;

namespace Swishark.Controllers
{
    [Route("[controller]")]
    public class TaskController : ControllerBase
    {
        [HttpPost]
        [Route("api/AddItem")]
        [Authorize]
        public JsonResult Add(PageModel pModel)
        {
            TaskHelper helper = new TaskHelper();
            AccountService aService = new AccountService();

            var code = helper.Create(pModel.Data, aService.GetCurrentUser(User.Identity.Name));

            return new JsonResult(code);
        }
    }
}