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
    public class TaskRepository : ITaskRepository
    {
        private ITodolistDbContext _dbContext;

        public TaskRepository(ITodolistDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<TodolistModel> GetTasks(string sortColumn = "", bool descending = false)
        {
            if (descending)
            {
                if (sortColumn == "Description")
                {
                    return _dbContext.Todolists.OrderByDescending(a => a.TaskDescription).ToList();
                }
                if (sortColumn == "EnrollmentDate")
                {
                    return _dbContext.Todolists.OrderByDescending(a => a.EnrollmentDate).ToList();
                }
                if (sortColumn == "Approved")
                {
                    return _dbContext.Todolists.OrderByDescending(a => a.Approved).ToList();
                }
            }
            else
            {
                if (sortColumn == "Description")
                {
                    return _dbContext.Todolists.OrderBy(a => a.TaskDescription).ToList();
                }
                if (sortColumn == "EnrollmentDate")
                {
                    return _dbContext.Todolists.OrderBy(a => a.EnrollmentDate).ToList();
                }
                if (sortColumn == "Approved")
                {
                    return _dbContext.Todolists.OrderBy(a => a.Approved).ToList();
                }
            }
            return _dbContext.Todolists.OrderByDescending(a => a.EnrollmentDate).ToList();
        }

        public bool Search(string taskDescription, int todolistIdOrZero)
        {
            if (todolistIdOrZero == 0)
            {
                int countResult = _dbContext.Todolists.Count(a => a.TaskDescription.ToLower() == taskDescription.ToLower());
                return countResult >= 1 ? true : false;
            }
            else
            {
                int countResultAndId = _dbContext.Todolists.Count(a => a.TaskDescription.ToLower() == taskDescription.ToLower() && a.TodolistId != todolistIdOrZero);
                return countResultAndId >= 1 ? true : false;
            }
        }

        public void Add(TodolistModel todolist)
        {
            _dbContext.Todolists.Add(todolist);
            _dbContext.SaveChanges();
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public void Remove(TodolistModel todolist)
        {
            _dbContext.Todolists.Remove(todolist);
            _dbContext.SaveChanges();
        }

        public TodolistModel Get(int id)
        {
            return _dbContext.Todolists.SingleOrDefault(a => a.TodolistId == id);
        }
    }
}