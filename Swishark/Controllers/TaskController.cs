using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Account;
using Services.Mark;
using Services.ProjectTask;
using Swishark.Models;
using System;

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
            return new JsonResult(new TaskHelper().Create(pModel.Data, new AccountService().GetCurrentUser(User.Identity.Name)));
        }

        [HttpGet]
        [Route("{id}/api/GetItem")]
        [Authorize]
        public JsonResult GetItem(string id)
        {
            if (id != "null")
                return new JsonResult(new TaskService().GetTask(new Guid(id)));

            return new JsonResult(null);
        }

        [HttpPost]
        [Route("{id}/api/Update")]
        [Authorize]
        public JsonResult Update(PageModel pModel, string id)
        {
            TaskHelper helper = new TaskHelper();
            AccountService aService = new AccountService();

            var code = helper.Update(pModel.Data, new Guid(id));

            return new JsonResult(code);
        }



        [HttpGet]
        [Route("{id}/api/GetMarks")]
        [Authorize]
        public JsonResult GetMarks(string id)
        {
            return new JsonResult(new MarkService().GetItemsOnTasks(new Guid(id)));
        }
    }
}