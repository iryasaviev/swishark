using System;
using System.Collections.Generic;
using System.Linq;

namespace Services.ProjectMemberRole
{
    public class RoleService
    {
        CrudRepo _repo;
        public RoleService()
        {
            _repo = new CrudRepo();
        }



        /// <summary>
        /// Возвращает роль.
        /// </summary>
        public Infrastructure.Entities.ProjectMemberRole GetItem(Guid id)
        {
            return _repo.GetItems<Infrastructure.Entities.ProjectMemberRole>().FirstOrDefault(x => x.Id.Equals(id));
        }

        /// <summary>
        /// Возвращает список ролей проекта.
        /// Если 'creator' == true, то выводим его тоже, если false то не выводим.
        /// </summary>
        public List<Infrastructure.Entities.ProjectMemberRole> GetItems(int projectId)
        {
            return _repo.GetItems<Infrastructure.Entities.ProjectMemberRole>().Where(x => x.ProjectId.Equals(projectId)).ToList();
        }
        
        /// <summary>
        /// Добавляет новую роль.
        /// </summary>
        public void Add(Infrastructure.Entities.ProjectMemberRole role)
        {
            _repo.Create(role);
        }

        /// <summary>
        /// Обновляет роли проекта.
        /// </summary>
        public void Update(Infrastructure.Entities.ProjectMemberRole role)
        {
            _repo.Update(role);
        }
    }
}