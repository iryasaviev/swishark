using Infrastructure.Entities;
using Infrastructure.Enums;
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



        public Codes.States Create(string dataStr)
        {
            Dictionary<string, string> data = _json.From(dataStr);

            _project.Name = data["Name"];
            _project.Description = data["Description"];

            _service.Add(_project);
            return Codes.States.Success;
        }
    }
}