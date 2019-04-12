using System.Linq;

namespace Services.ProjectTask
{
    public class TaskService
    {
        CrudRepo _repo;
        public TaskService()
        {
            _repo = new CrudRepo();
        }

        /// <summary>
        /// Добавляет новую задачу.
        /// </summary>
        public void Add(Infrastructure.Entities.ProjectTask task)
        {
            _repo.Create(task);
        }

        /// <summary>
        /// Обновляет данные задачи.
        /// </summary>
        public void Update(Infrastructure.Entities.ProjectTask task)
        {
            _repo.Update(task);
        }

        /// <summary>
        /// Возвращает задачу.
        /// </summary>
        public Infrastructure.Entities.ProjectTask GetTask(int id)
        {
            return _repo.GetItems<Infrastructure.Entities.ProjectTask>().FirstOrDefault(x => x.Id.Equals(id));
        }
    }
}