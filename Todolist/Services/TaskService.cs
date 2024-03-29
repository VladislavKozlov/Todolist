﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Todolist.ContextDb;
using Todolist.Models;
using Todolist.Repositories;
using Todolist.ViewModels;


/*
 * 
 * @author Vladislav Kozlov <k2v.akosa@gmail.com>
*/
namespace Todolist.Services
{
    public class TaskService : ITaskService
    {
        private ITaskRepository _taskRepository;

        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public TasksVm GetTasksPaging(int page, string sortColumn = "", bool descending = false)
        {
            int pageSize = Convert.ToInt32(ConfigurationManager.AppSettings["DefaultPageSize"]);
            var pagiList = _taskRepository.GetTasks(sortColumn, descending);
            IEnumerable<TodolistModel> tasksPerPages = pagiList.Skip((page - 1) * pageSize).Take(pageSize);
            PagingInfo pagingInfo = new PagingInfo { PageNumber = page, PageSize = pageSize, TotalItems = pagiList.Count };
            TasksVm tasksVm = new TasksVm { PagingInfoVm = pagingInfo, TasksPage = tasksPerPages };
            return tasksVm;
        }

        public TasksVm GetTasks(string sortColumn = "", bool descending = false)
        {
            var tasks = _taskRepository.GetTasks(sortColumn, descending);
            TasksVm tasksVm = new TasksVm { Tasks = tasks };
            return tasksVm;
        }

        public bool SearchTaskDescription(string taskDescription, int todolistIdOrZero)
        {
            return _taskRepository.Search(taskDescription, todolistIdOrZero);
        }

        public void Add(TaskInput taskInput)
        {
            TodolistModel todolist = new TodolistModel();
            todolist.EnrollmentDate = DateTime.Now;
            todolist.TaskDescription = taskInput.TaskDescription;
            _taskRepository.Add(todolist);
        }

        public void Edit(TaskInput taskInput)
        {
            TodolistModel todolistToUpdate = _taskRepository.Get(taskInput.TodolistId);
            todolistToUpdate.TaskDescription = taskInput.TaskDescription;
            todolistToUpdate.Approved = taskInput.Approved;
            _taskRepository.Save();
        }

        public void Remove(int id)
        {
            TodolistModel todolist = _taskRepository.Get(id);
            _taskRepository.Remove(todolist);
        }

        public TaskVm Get(int id)
        {
            TodolistModel todolist = _taskRepository.Get(id);
            TaskVm taskVm = new TaskVm(todolist);
            return taskVm;
        }
    }
}