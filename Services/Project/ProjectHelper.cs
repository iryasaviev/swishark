using Infrastructure.Entities;
using Infrastructure.Enums;
using Services.ProjectTask;
using Services.Validation;
using System;
using System.Collections.Generic;

namespace Services.Project
{
    public class ProjectHelper
    {
        Json _json;
        User _user;
        ValidationHelper _validation;
        ProjectService _service;
        Infrastructure.Entities.Project _project;
        public ProjectHelper()
        {
            _json = new Json();
            _user = new User();
            _validation = new ValidationHelper();
            _service = new ProjectService();
            _project = new Infrastructure.Entities.Project();
        }



        public Codes.States Create(string dataStr, User user)
        {
            Dictionary<string, string> data = _json.From(dataStr);

            _project.Name = data["Name"];
            _project.Description = data["Description"];
            _project.UserId = user.Id;

            _service.Add(_project);
            return Codes.States.Success;
        }


        public object Get(int id)
        {
            TaskService tService = new TaskService();

            return new { project = _service.GetProject(id), tasks = tService.GetTasks(id) };
        }
    }
}