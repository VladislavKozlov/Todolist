using System.Data.Entity;

/*
 * 
 * @author Vladislav Kozlov <k2v.akosa@gmail.com>
*/
namespace Todolist.ContextDb
{
    public class TodolistDbContext : DbContext
    {
        public DbSet<TodolistModel> Todolists { get; set; }
    }
}