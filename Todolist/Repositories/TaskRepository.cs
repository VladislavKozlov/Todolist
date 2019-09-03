using System.Collections.Generic;
using System.Data;
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
        private TodolistDbContext _dbConnection;

        public void SetDbConnection(TodolistDbContext dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public TaskRepository()
        {
        }

        public List<TodolistModel> GetTasks()
        {
            return _dbConnection.EntityModels.OrderByDescending(item => item.EnrollmentDate).ToList();
        }

        public void Add(TodolistModel todolist)
        {
            _dbConnection.EntityModels.Add(todolist);
            _dbConnection.SaveChanges();
        }

        public void Save()
        {
            _dbConnection.SaveChanges();
        }

        public void Remove(TodolistModel todolist)
        {
            _dbConnection.EntityModels.Remove(todolist);
            _dbConnection.SaveChanges();
        }

        public TodolistModel Single(int id)
        {
            return _dbConnection.EntityModels.Single(a => a.TodolistId == id);
        }
    }
}