using Infrastructure.Entities;
using Infrastructure.Enums;

namespace Services.ProjectMember
{
    public class MemberHelper
    {
        Infrastructure.Entities.Project _project;
        Infrastructure.Entities.ProjectMember _member;
        MemberService _service;
        public MemberHelper() {
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
    }
}