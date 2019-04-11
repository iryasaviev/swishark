using System.Linq;

namespace Services.Project
{
    public class ProjectService
    {
        CrudRepo _repo;
        public ProjectService()
        {
            _repo = new CrudRepo();
        }

        /// <summary>
        /// Добавляет новый проект.
        /// </summary>
        public void Add(Infrastructure.Entities.Project project)
        {
            _repo.Create(project);
        }

        /// <summary>
        /// Обновляет данные проекта.
        /// </summary>
        public void Update(Infrastructure.Entities.Project project)
        {
            _repo.Update(project);
        }

        /// <summary>
        /// Возвращает проект.
        /// </summary>
        public Infrastructure.Entities.Project GetProject(int id)
        {
            return _repo.GetItems<Infrastructure.Entities.Project>().FirstOrDefault(x => x.Id.Equals(id));
        }

        /// <summary>
        /// Проверяет наличие проекта в базе.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool CheckProject(int id)
        {
            bool result = true;

            if (_repo.GetItems<Infrastructure.Entities.Project>().FirstOrDefault(p => p.Id == id) == null)
                result = false;

            return result;
        }
    }
}