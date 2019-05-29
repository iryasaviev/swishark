using Infrastructure.Entities;
using Infrastructure.Enums;
using Services.Mark;
using System;
using System.Collections.Generic;

namespace Services.ProjectTask
{
    public class TaskHelper
    {
        Json _json;
        User _user;
        TaskService _service;
        Infrastructure.Entities.ProjectTask _task;
        public TaskHelper()
        {
            _json = new Json();
            _user = new User();
            _service = new TaskService();
            _task = new Infrastructure.Entities.ProjectTask();
        }



        public Codes.States Create(string dataStr, User user)
        {
            Dictionary<string, string> data = _json.From(dataStr);

            _task.Title = data["Title"];
            _task.Description = data["Description"];
            _task.CreatedDate = DateTime.Now;
            _task.FinishDate = DateTime.Parse(data["FinishDate"]);
            _task.CretedUserId = user.Id;
            _task.ProjectId = Convert.ToInt32(data["ProjectId"]);
            _task.State = Convert.ToInt32(data["State"]);

            new MarkHelper().CreateTaskMark(data["Marks"], _task.ProjectId, _task.Id);

            _service.Add(_task);
            return Codes.States.Success;
        }

        public Codes.States Update(string dataStr, Guid id)
        {
            Dictionary<string, string> data = _json.From(dataStr);

            Infrastructure.Entities.ProjectTask upTask = _service.GetTask(id);

            upTask.Title = data["Title"];
            upTask.Description = data["Description"];
            upTask.FinishDate = DateTime.Parse(data["FinishDate"]);
            upTask.State = Convert.ToInt32(data["State"]);

            _service.Update(upTask);
            return Codes.States.Success;
        }
    }
}