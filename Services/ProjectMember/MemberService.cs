using System.Collections.Generic;
using System.Linq;

namespace Services.ProjectMember
{
    public class MemberService
    {
        CrudRepo _repo;
        public MemberService()
        {
            _repo = new CrudRepo();
        }

        /// <summary>
        /// Добавляет нового участника проекта.
        /// </summary>
        /// <param name="member"></param>
        public void Add(Infrastructure.Entities.ProjectMember member)
        {
            _repo.Create(member);
        }

        /// <summary>
        /// Обновляет данные участника.
        /// </summary>
        public void Update(Infrastructure.Entities.ProjectMember member)
        {
            _repo.Update(member);
        }

        /// <summary>
        /// Возвращает участника.
        /// </summary>
        public Infrastructure.Entities.ProjectMember GetItem(int id)
        {
            return _repo.GetItems<Infrastructure.Entities.ProjectMember>().FirstOrDefault(x => x.Id.Equals(id));
        }

        /// <summary>
        /// Возвращает участников проекта.
        /// </summary>
        public List<Infrastructure.Entities.ProjectMember> GetItems(int projectId)
        {
            return _repo.GetItems<Infrastructure.Entities.ProjectMember>().Where(x => x.ProjectId.Equals(projectId)).ToList();
        }
    }
}