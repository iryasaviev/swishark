using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Services.Mark
{
    public class MarkService
    {
        CrudRepo _repo;
        public MarkService()
        {
            _repo = new CrudRepo();
        }

        /// <summary>
        /// Добавляет новый марку в проект.
        /// </summary>
        public void AddToProject(Infrastructure.Entities.ProjectMark mark)
        {
            _repo.Create(mark);
        }

        /// <summary>
        /// Возвращает метки проекта.
        /// </summary>
        public List<Infrastructure.Entities.ProjectMark> GetItemsOnProject(int projectId)
        {
            return _repo.GetItems<Infrastructure.Entities.ProjectMark>().Where(x => x.ProjectId.Equals(projectId)).ToList();
        }

        /// <summary>
        /// Обновляет метки задачи.
        /// </summary>
        public void UpdateInProject(Infrastructure.Entities.ProjectMark mark)
        {
            _repo.Update(mark);
        }



        /// <summary>
        /// Добавляет новый марку в задачу.
        /// </summary>
        public void AddToTask(Infrastructure.Entities.TaskMark mark)
        {
            _repo.Create(mark);
        }

        /// <summary>
        /// Возвращает метки задачи.
        /// </summary>
        public List<Infrastructure.Entities.TaskMark> GetItemsOnTasks(Guid taskId)
        {
            return _repo.GetItems<Infrastructure.Entities.TaskMark>().Where(x => x.TaskId.Equals(taskId)).ToList();
        }

        /// <summary>
        /// Обновляет метки задачи.
        /// </summary>
        public void UpdateInTask(Infrastructure.Entities.TaskMark mark)
        {
            _repo.Update(mark);
        }
    }
}