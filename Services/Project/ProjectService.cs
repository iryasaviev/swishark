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
    }
}