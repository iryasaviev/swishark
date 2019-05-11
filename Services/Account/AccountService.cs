using Infrastructure.Entities;
using System.Linq;

namespace Services.Account
{
    public class AccountService
    {
        CrudRepo _repo;
        public AccountService()
        {
            _repo = new CrudRepo();
        }

        /// <summary>
        /// Добавляет нового пользователя.
        /// </summary>
        public void AddUser(User user)
        {
            _repo.Create(user);
        }

        /// <summary>
        /// Обновляет данные пользователя.
        /// </summary>
        public void UpdateUser(User user)
        {
            _repo.Update(user);
        }

        /// <summary>
        /// Возвращает текущего пользователя.
        /// </summary>
        public User GetCurrentUser(string identityName)
        {
            return _repo.GetItems<User>().FirstOrDefault(x => x.Email.Equals(identityName));
        }

        /// <summary>
        /// Возвращает пользователя по id.
        /// </summary>
        public User GetUser(int id)
        {
            return _repo.GetItems<User>().FirstOrDefault(x => x.Id.Equals(id));
        }

        /// <summary>
        /// Проверяет наличие пользователя в базе.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public bool CheckEmail(string email)
        {
            bool result = true;

            if (_repo.GetItems<User>().FirstOrDefault(u => u.Email == email) == null)
                result = false;

            return result;
        }
    }
}