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
        private TodolistDbContext _dbConnection = new TodolistDbContext();
        private TaskRepository _taskRepository;

        public TaskService()
        {
            _taskRepository = new TaskRepository();
            _taskRepository.SetDbConnection(_dbConnection);
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
                InitTodolistModelTaskInput(todolist, taskInput);
                _taskRepository.Add(todolist);
            }
        }

        public void Edit(int id, TaskInput task)
        {
            TaskInput taskInput = task;                        
            TodolistModel todolistToUpdate = _taskRepository.Get(id);
            InitTodolistModelTaskInput(todolistToUpdate, taskInput);
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
            InitTaskInputTodolistModel(taskInput, todolist);
            return taskInput;
        }

        public void InitTodolistModelTaskInput(TodolistModel todolistModel, TaskInput taskInput)
        {
            if (todolistModel != null && taskInput != null)
            {
                todolistModel.TaskDescription = taskInput.TaskDescription;
                todolistModel.EnrollmentDate = DateTime.Now;
                todolistModel.Approved = taskInput.Approved;
            }
        }

        public void InitTaskInputTodolistModel(TaskInput taskInput, TodolistModel todolistModel)
        {
            if (taskInput != null && todolistModel != null)
            {
                taskInput.TaskDescription = todolistModel.TaskDescription;
                taskInput.EnrollmentDate = DateTime.Now;
                taskInput.Approved = todolistModel.Approved;
            }
        }

        public void InitVTaskVmTaskInput(TaskVm taskVm, TaskInput taskInput)
        {
            if (taskVm != null && taskInput != null)
            {
                taskVm.TaskDescription = taskInput.TaskDescription;
                taskVm.EnrollmentDate = DateTime.Now;
                taskVm.Approved = taskInput.Approved;
            }
        }
    }
}