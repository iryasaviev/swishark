using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Services
{
    public class Crud
    {
        protected DbContext _context;
        public Crud(DbContext db)
        {
            _context = db;
        }

        /// <summary>
        /// Метод сохранения.
        /// </summary>
        public void Save()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                var mess = ex.Message;
            }
        }


        /// <summary>
        /// Метод создания.
        /// </summary>
        public void Create<T>(T item) where T : class
        {
            //Add(item);
            _context.Set<T>().AddRange(item);
            Save();
        }

        /// <summary>
        /// Метод обновления.
        /// </summary>
        public void Update<T>(T item) where T : class
        {
            // TODO: доделать.

            if (item == null)
                return;

            _context.Entry(item).State = EntityState.Modified;
            Save();
        }

        /// <summary>
        /// Метод удаления.
        /// </summary>
        public void Delete<T>(T item) where T : class
        {
            _context.Set<T>().Remove(item);
            Save();
        }

        public IQueryable<T> GetItems<T>() where T : class
        {
            //_context.Users.ToList();
            return _context.Set<T>();
        }
    }
}