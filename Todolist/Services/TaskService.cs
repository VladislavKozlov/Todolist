using System;
using Todolist.ContextDb;
using Todolist.Models;

/*
 * 
 * @author Vladislav Kozlov <k2v.akosa@gmail.com>
*/
namespace Todolist.Services
{
    public class TaskService
    {
        public void InitTaskService(TodolistModel todolistModel, TaskInput taskInput)
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