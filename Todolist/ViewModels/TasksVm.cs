using System;
using System.Collections.Generic;
using Todolist.ContextDb;

/*
 * 
 * @author Vladislav Kozlov <k2v.akosa@gmail.com>
*/
namespace Todolist.ViewModels
{
    public class TasksVm
    {
        public int TodolistId;
        public string TaskDescription;
        public DateTime EnrollmentDate;
        public bool Approved;

        public TasksVm()
        {
        }

        public TasksVm(TodolistModel todolistModel)
        {

            TodolistId = todolistModel.TodolistId;
            TaskDescription = todolistModel.TaskDescription;
            EnrollmentDate = todolistModel.EnrollmentDate;
            Approved = todolistModel.Approved;
        }

        public List<TodolistModel> Tasks { get; set; }
    }
}