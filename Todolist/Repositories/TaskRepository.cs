using System.Collections.Generic;
using System.Linq;
using Todolist.ContextDb;

/*
 * 
 * @author Vladislav Kozlov <k2v.akosa@gmail.com>
*/
namespace Todolist.Repositories
{
    public class TaskRepository
    {
        private TodolistDbContext _db = new TodolistDbContext();

        public TaskRepository()
        {
        }

        public List<TodolistModel> OrderByDescending()
        {
            if (_db != null)
            {
                return _db.Todos.OrderByDescending(item => item.EnrollmentDate).ToList();
            }
            return null;
        }

        public void Add(TodolistModel todolist)
        {
            if (_db != null && todolist != null)
            {
                _db.Todos.Add(todolist);
                _db.SaveChanges();
            }
        }

        public void Save()
        {
            if (_db != null)
            {
                _db.SaveChanges();
            }
        }

        public void Remove(TodolistModel todolist)
        {
            _db.Todos.Remove(todolist);
            _db.SaveChanges();
        }

        public TodolistModel Find(int id)
        {
            if (_db != null && id != 0)
            {
                return _db.Todos.Find(id);
            }
            return null;
        }
    }
}