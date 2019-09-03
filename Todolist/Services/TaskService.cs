using System;
using System.Collections.Generic;
using Todolist.ContextDb;
using Todolist.Models;
using Todolist.Repositories;

/*
 * 
 * @author Vladislav Kozlov <k2v.akosa@gmail.com>
*/
namespace Todolist.Services
{
    public class TaskService : IService
    {
        private TodolistDbContext _dbConnection = new TodolistDbContext();
        private TaskRepository _taskRepository;

        public TaskService()
        {
            _taskRepository = new TaskRepository();
            _taskRepository.SetDbConnection(_dbConnection);
        }

        public List<TodolistModel> GetTasks()
        {
            if (_taskRepository != null)
            {
                return _taskRepository.GetTasks();
            }
            return null;
        }

        public void Add(TodolistModel todolist)
        {
            if (todolist != null)
            {
                _taskRepository.Add(todolist);
            }
        }

        public void Save()
        {
            _taskRepository.Save();
        }

        public void Remove(TodolistModel todolist)
        {
            if (todolist != null)
            {
                _taskRepository.Remove(todolist);
            }
        }

        public TodolistModel Single(int id)
        {
            if (id != 0)
            {
                return _taskRepository.Single(id);
            }
            return null;
        }

        public void InitEntityModel(TodolistModel todolistModel, TaskInput taskInput)
        {
            if (todolistModel != null && taskInput != null)
            {
                todolistModel.TaskDescription = taskInput.TaskDescription;
                todolistModel.EnrollmentDate = DateTime.Now;
                todolistModel.Approved = taskInput.Approved;
            }
        }
    }
}