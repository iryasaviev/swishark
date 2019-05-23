using Infrastructure.Entities;
using Infrastructure.Enums;
using Services.Project;
using System;

namespace Services.ProjectMemberRole
{
    public class RoleHelper
    {
        Json _json;
        ProjectService _projectService;
        RoleService _service;
        public RoleHelper()
        {
            _json = new Json();
            _projectService = new ProjectService();
            _service = new RoleService();
        }

        public class Role
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public string Color { get; set; }
        }



        /// <summary>
        /// Добавляет новые роли в проект.
        /// </summary>
        /// <param name="dataStr"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public Codes.States AddToProject(Role item, int projectId, User user)
        {
            var project = _projectService.GetProject(projectId);

            if (project == null)
                // TODO: Переделать ошибку.
                return Codes.States.ErrorAccountDoesNotExist;

            if (project.UserId != user.Id)
                // TODO: Переделать ошибку.
                return Codes.States.ErrorAccountDoesNotExist;


            Infrastructure.Entities.ProjectMemberRole role = new Infrastructure.Entities.ProjectMemberRole
            {
                Id = Guid.NewGuid(),
                Name = item.Name,
                Color = item.Color,
                ProjectId = projectId
            };

            _service.Add(role);

            return Codes.States.Success;
        }

        public Codes.States Update(string dataStr, int projectId, User user)
        {
            var project = _projectService.GetProject(projectId);

            if (project == null)
                // TODO: Переделать ошибку.
                return Codes.States.ErrorAccountDoesNotExist;

            if (project.UserId != user.Id)
                // TODO: Переделать ошибку.
                return Codes.States.ErrorAccountDoesNotExist;


            var data = _json.From1<Role>(dataStr);

            foreach (var item in data)
            {
                if (item.Id != "")
                {
                    var role = _service.GetItem(new Guid(item.Id));

                    role.Name = item.Name;
                    role.Color = item.Color;
                    role.ProjectId = projectId;

                    _service.Update(role);
                }
                else
                {
                    AddToProject(item, projectId, user);
                }
            }

            return Codes.States.Success;
        }
    }
}