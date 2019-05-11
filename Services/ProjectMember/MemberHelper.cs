using Infrastructure.Entities;
using Infrastructure.Enums;
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
        /// 
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
        /// 
        /// </summary>
        /// <param name="dataStr"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public Codes.States AddToProject(string dataStr, int projectId)
        {
            Dictionary<string, string> data = _json.From(dataStr);

            User user = new Account.AccountService().GetUser(Convert.ToInt32(data["Id"]));

            if (user == null)
            {
                return Codes.States.ErrorAccountDoesNotExist;
            }

            _member.UserId = user.Id;
            _member.FirstName = user.FirstName;
            _member.LastName = user.LastName;
            _member.Photo = user.Photo;
            _member.ProjectId = projectId;

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
    }
}