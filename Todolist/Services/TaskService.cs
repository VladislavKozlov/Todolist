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

        public TaskService()
        {
            _taskRepository = new TaskRepository();
        }

        public TasksVm GetTasks()
        {
            var tasks = _taskRepository.GetTasks();
            TasksVm tasksVm = new TasksVm { Tasks = tasks };
            return tasksVm;
        }

        public void Add(TaskInput taskInput)
        {
            if (taskInput != null)
            {
                TodolistModel todolist = new TodolistModel();
                InitIModel(todolist, taskInput);
                _taskRepository.Add(todolist);
            }
        }

        public void Edit(int id, TaskInput task)
        {
            TaskInput taskInput = task;
            TodolistModel todolistToUpdate = _taskRepository.Get(id);
            InitIModel(todolistToUpdate, taskInput);
            _taskRepository.Save();
        }

        public void Remove(int id)
        {
            TodolistModel todolist = _taskRepository.Get(id);
            _taskRepository.Remove(todolist);
        }

        public TaskInput Get(int id)
        {
            TaskInput taskInput = new TaskInput();
            TodolistModel todolist = _taskRepository.Get(id);
            InitIModel(taskInput, todolist);
            return taskInput;
        }

        public void InitIModel(IModel model1, IModel model2)
        {
            if (model1 != null && model2 != null)
            {
                model1.TaskDescription = model2.TaskDescription;
                model1.EnrollmentDate = DateTime.Now;
                model1.Approved = model2.Approved;
            }
        }
    }
}