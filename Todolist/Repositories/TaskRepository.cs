//using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
//using System.Data.SqlClient;
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
        private string tableName;

        public TaskRepository(ITodolistDbContext dbContext)
        {
            _dbContext = dbContext;
            tableName = Convert.ToString(ConfigurationManager.AppSettings["TableName"]);
        }

        public List<TodolistModel> GetTasks()
        {
            return _dbContext.Todolists.OrderByDescending(item => item.EnrollmentDate).ToList();
        }

        public bool Search(string taskDescription, int todolistIdOrZero)
        {
            int countResult = _dbContext.Todolists.Count(a => a.TaskDescription == taskDescription);
            //string sql = "SELECT COUNT * FROM TodolistModels WHERE TaskDescription == @taskDescription";
            //"SELECT COUNT(*) FROM TodolistModels WHERE TaskDescription LIKE %TaskDescription% == @taskDescription";
            //var sqlParameter = new SqlParameter("@taskDescription", taskDescription);
            //var countResultSql = _dbContext.Todolists.FromSql(sql, sqlParameter).ToString();
            //int countResult = Convert.ToInt32(countResultStr);            
            if (todolistIdOrZero == 0)
            {
                if (countResult >= 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                int countResultAndId = _dbContext.Todolists.Count(a => a.TaskDescription == taskDescription && a.TodolistId != todolistIdOrZero);
                if (countResultAndId >= 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
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