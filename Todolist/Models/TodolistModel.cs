using System;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/*
 * 
 * @author Vladislav Kozlov <k2v.akosa@gmail.com>
*/
namespace Todolist.Models
{
    public class TodolistModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TodolistId { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Описание не может быть более длинным чем 100 символов.")]
        [Display(Name = "Описание задачи")]
        public string TaskDescription { get; set; }
		
        public DateTime EnrollmentDate { get; set; }

        [Display(Name = "Задача решена")]
        public bool Approved { get; set; }
    }
	
	public class TodolistDbContext : DbContext
    {         
        public DbSet<TodolistModel> Todos { get; set; }            
    }
}