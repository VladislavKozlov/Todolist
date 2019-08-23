using System.Data.Entity;

/*
 * 
 * @author Vladislav Kozlov <k2v.akosa@gmail.com>
*/
namespace Todolist.Models.ContextDb
{
	public class TodolistDbContext : DbContext
    {         
        public DbSet<TodolistModel> Todos { get; set; }            
    }
}