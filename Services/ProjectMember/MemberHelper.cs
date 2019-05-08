using Infrastructure.Entities;
using Infrastructure.Enums;

namespace Services.ProjectMember
{
    public class MemberHelper
    {
        User _user;
        Infrastructure.Entities.Project _project;
        Infrastructure.Entities.ProjectMember _member;
        public MemberHelper() {
        
            _user = new User();
            _project = new Infrastructure.Entities.Project();
            _member = new Infrastructure.Entities.ProjectMember();
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataStr"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public Codes.States Create(int projectId, User user)
        {
            return Codes.States.Success;
        }
    }
}