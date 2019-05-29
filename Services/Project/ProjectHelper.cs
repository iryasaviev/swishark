using Infrastructure.Entities;
using Infrastructure.Enums;
using Services.Mark;
using Services.ProjectMember;
using Services.ProjectMemberRole;
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



        /// <summary>
        /// Создает новый проект.
        /// </summary>
        /// <param name="dataStr">JSON-строка данных.</param>
        /// <param name="user">Создатель проекта.</param>
        /// <returns>Возвращает код состояния результата.</returns>
        public Codes.States Create(string dataStr, User user)
        {
            Dictionary<string, string> data = _json.From(dataStr);

            _project.Name = data["Name"];
            _project.Description = data["Description"];
            _project.UserId = user.Id;

            _service.Add(_project);

            // - Создание меток проекта.
            // Если не удалось создать метки у проекта.
            // Прерывается. Ошибка 500.
            if (new MarkHelper().CreateProjectMark(_project.Id) != Codes.States.Success)
                return Codes.States.ServerError;


            MemberHelper memberHelper = new MemberHelper();

            // - Создание пользователя проекта.
            // Если не удалось создать метки у проекта.
            // Прерывается. Ошибка 500.
            if (memberHelper.Create(_project.Id, user) != Codes.States.Success)
                return Codes.States.ServerError;

            // - Добавление роли "Создатель"
            foreach (var role in new List<RoleHelper.Role>
                {
                    new RoleHelper.Role
                    {
                        Name = "Создатель",
                        Color = "ColorNone"
                    },

                    new RoleHelper.Role
                    {
                        Name = "Без роли",
                        Color = "ColorNone"
                    }
                })
                new RoleHelper().AddToProject(role, _project.Id, user);

            // Если не удалось обновить роль у пользователя.
            // Прерывается. Ошибка 500.
            if (memberHelper.UpdateRole(user, _project.Id) != Codes.States.Success)
                return Codes.States.ServerError;

            return Codes.States.Success;
        }

        /// <summary>
        /// Обновляет данные проекта.
        /// </summary>
        /// <param name="dataStr">JSON-строка данных.</param>
        /// <param name="id">Id проекта.</param>
        /// <returns>Возвращает код состояния результата.</returns>
        public Codes.States Update(string dataStr, int id)
        {
            Dictionary<string, string> data = _json.From(dataStr);
            var upProject = _service.GetProject(id);

            upProject.Name = data["Name"];
            upProject.Description = data["Description"];

            try
            {
                _service.Update(upProject);
                return Codes.States.Success;
            }
            catch
            {
                // TODO: Сделать вывод ошибки.
                return Codes.States.Success;
            }
        }



        public object Get(int id)
        {
            TaskService tService = new TaskService();

            return new { project = _service.GetProject(id), tasks = tService.GetTasks(id) };
        }
    }
}