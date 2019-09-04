using System;
using System.ComponentModel.DataAnnotations;
using Todolist.ContextDb;

/*
 * 
 * @author Vladislav Kozlov <k2v.akosa@gmail.com>
*/
namespace Todolist.Models
{
    public class TaskInput : IModel
    {
        public int TodolistId { get; set; }

        [Required(ErrorMessage = "Описание задачи должно быть заполнено!")]
        [StringLength(100, ErrorMessage = "Описание не может быть более длинным чем 100 символов!")]
        public string TaskDescription { get; set; }

        public DateTime EnrollmentDate { get; set; }

        public bool Approved { get; set; }

        public TaskInput()
        {
        }

        public TaskInput(TodolistModel todolistModel)
        {
            TodolistId = todolistModel.TodolistId;
            TaskDescription = todolistModel.TaskDescription;
            EnrollmentDate = todolistModel.EnrollmentDate;
            Approved = todolistModel.Approved;
        }
    }
}