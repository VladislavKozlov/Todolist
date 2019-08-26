using System;
using System.ComponentModel.DataAnnotations;
using Todolist.ContextDb;

/*
 * 
 * @author Vladislav Kozlov <k2v.akosa@gmail.com>
*/
namespace Todolist.Models
{
    public class TaskInput
    {
        public int TodolistId { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Описание не может быть более длинным чем 100 символов.")]
        [Display(Name = "Описание задачи")]
        public string TaskDescription { get; set; }

        public DateTime EnrollmentDate { get; set; }

        [Display(Name = "Задача решена")]
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

        public TodolistModel Task { get; set; }
    }
}