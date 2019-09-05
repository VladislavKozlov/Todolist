using Todolist.ContextDb;
using Todolist.Models;
using Todolist.ViewModels;

/*
 * 
 * @author Vladislav Kozlov <k2v.akosa@gmail.com>
*/
namespace Todolist.Services
{
    public interface ITaskService
    {
        void InitTodolistModel(TodolistModel todolist, TaskInput taskInput);
        TasksVm GetTasks();
        void Add(TaskInput taskInput);
        void Edit(TaskInput taskInput);
        void Remove(int id);
        TaskVm Get(int id);
    }
}
