using Infrastructure.Entities;
using Infrastructure.Enums;
using Services.ProjectMemberRole;
using System;
using System.Collections.Generic;

namespace Services.ProjectMember
{
    public class MemberHelper
    {
        Json _json;
        Infrastructure.Entities.Project _project;
        Infrastructure.Entities.ProjectMember _member;
        MemberService _service;
        public MemberHelper() {
            _json = new Json();
            _project = new Infrastructure.Entities.Project();
            _member = new Infrastructure.Entities.ProjectMember();
            _service = new MemberService();
        }



        /// <summary>
        /// Создает нового участника.
        /// </summary>
        /// <param name="dataStr"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public Codes.States Create(int projectId, User user)
        {
            _member.UserId = user.Id;
            _member.FirstName = user.FirstName;
            _member.LastName = user.LastName;
            _member.ProjectId = projectId;

            _service.Add(_member);

            return Codes.States.Success;
        }

        /// <summary>
        /// Добавляет нового участника в проект.
        /// </summary>
        /// <param name="dataStr"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public Codes.States AddToProject(string dataStr, int projectId)
        {
            Dictionary<string, string> data = _json.From(dataStr);

            User user = new Account.AccountService().GetUser(Convert.ToInt32(data["Id"]));
            
            // Если аккаунт пользвателя с таким Id не создан
            if (user == null)
            {
                return Codes.States.ErrorAccountDoesNotExist;
            }

            // Если пользователь с таким Id уже добавлен в проект
            var member = _service.GetItem(user.Id);
            if (_service.GetItem(user.Id) != null)
            {
                if (member.ProjectId == projectId)
                    return Codes.States.ErrorAccountIdIsBusy;
            }
            
            _member.UserId = user.Id;
            _member.FirstName = user.FirstName;
            _member.LastName = user.LastName;
            _member.Photo = user.Photo;
            _member.ProjectId = projectId;
            _member.RoleId = new Guid(data["Role"]);

            try
            {
                _service.Add(_member);
                return Codes.States.Success;
            }
            catch
            {
                // TODO: Сделать вывод ошибки.
                return Codes.States.Success;
            }
        }

        /// <summary>
        /// Обновляет роль участника при создании проекта.
        /// Роль участника называется "СОздатель" и имеет постоянно у каждого проекта уникальный id.
        /// </summary>
        /// <param name="user">Пользователь</param>
        /// <param name="projectId">Id проекта</param>
        /// <returns>Возвращает рузельтат выполнения.</returns>
        public Codes.States UpdateRole(User user, int projectId)
        {
            Infrastructure.Entities.ProjectMember member = new Infrastructure.Entities.ProjectMember();

            // Поиск участника проекта
            foreach (var m in _service.GetItems(projectId))
            {
                if (m.UserId == user.Id)
                {
                    member = m;
                    break;
                }
            }

            // Поиск роли участника
            foreach (var r in new RoleService().GetItems(projectId))
            {
                member.RoleId = r.Id;
                _service.Update(member);

                return Codes.States.Success;
            }

            // TODO: Переделать вывод ошибки!
            return Codes.States.ErrorAccountDoesNotExist;
        }
    }
}