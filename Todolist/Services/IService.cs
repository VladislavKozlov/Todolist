using System.Collections.Generic;
using Todolist.ContextDb;
using Todolist.Models;

/*
 * 
 * @author Vladislav Kozlov <k2v.akosa@gmail.com>
*/
namespace Todolist.Services
{
    interface IService
    {
        void InitEntityModel(TodolistModel todolistModel, TaskInput taskInput);
        List<TodolistModel> GetTasks();
        void Add(TodolistModel todolist);
        void Save();
        void Remove(TodolistModel todolist);
        TodolistModel Single(int id);
    }
}
