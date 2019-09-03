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
        private TodolistDbContext _db;

        public TaskRepository()
        {
            _db = new TodolistDbContext();
        }

        public List<TodolistModel> GetTasks()
        {
            return _db.Todos.OrderByDescending(item => item.EnrollmentDate).ToList();
        }

        public void Add(TodolistModel todolist)
        {
            _db.Todos.Add(todolist);
            _db.SaveChanges();
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Remove(TodolistModel todolist)
        {
            _db.Todos.Remove(todolist);
            _db.SaveChanges();
        }

        public TodolistModel Single(int id)
        {
            return _db.Todos.Single(a => a.TodolistId == id);
        }
    }
}