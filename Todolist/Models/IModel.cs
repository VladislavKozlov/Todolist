using System;

/*
 * 
 * @author Vladislav Kozlov <k2v.akosa@gmail.com>
*/
namespace Todolist.Models
{
    public interface IModel
    {
        int TodolistId { get; set; }
        string TaskDescription { get; set; }
        DateTime EnrollmentDate { get; set; }
        bool Approved { get; set; }
    }
}
