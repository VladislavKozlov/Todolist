using System;
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

        public string SwitchDescending(string descending)
        {
            if (descending == "true")
            {
                return "false";
            }
            if (descending == "false")
            {
                return "true";
            }
            return "";
        }

        public TasksVm GetTasks()
        {
            var tasks = _taskRepository.GetTasks();
            TasksVm tasksVm = new TasksVm { Tasks = tasks };
            return tasksVm;
        }

        public TasksVm GetTasks(string sortOrder, string descending)
        {
            var tasks = _taskRepository.GetTasks(sortOrder, descending);
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