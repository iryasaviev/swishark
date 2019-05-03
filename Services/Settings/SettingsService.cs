using Infrastructure.Entities;

namespace Services.Settings
{
    public class SettingsService
    {
        CrudRepo _repo;
        public SettingsService()
        {
            _repo = new CrudRepo();
        }

        /// <summary>
        /// Обновляет данные пользователя.
        /// </summary>
        public void UpdateUser(User user)
        {
            _repo.Update(user);
        }

        /// <summary>
        /// Обновляет данные проекта.
        /// </summary>
        public void UpdateProject(Infrastructure.Entities.Project project)
        {
            _repo.Update(project);
        }
    }
}