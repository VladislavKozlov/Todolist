﻿using Todolist.ContextDb;
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

        public TasksVm GetTasks()
        {
            var tasks = _taskRepository.GetTasks();
            TasksVm tasksVm = new TasksVm { Tasks = tasks };
            return tasksVm;
        }

        public bool SearchTaskDescription(string taskDescription)
        {            
            return _taskRepository.Search(taskDescription);
        }

        public void Add(TaskInput taskInput)
        {
            TodolistModel todolist = new TodolistModel();
            todolist.InitEntity(taskInput);
            _taskRepository.Add(todolist);
        }

        public void Edit(TaskInput taskInput)
        {
            TodolistModel todolistToUpdate = _taskRepository.Get(taskInput.TodolistId);
            todolistToUpdate.InitEntity(taskInput, taskInput.EnrollmentDate);
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