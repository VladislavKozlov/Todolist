using System;

/*
 * 
 * @author Vladislav Kozlov <k2v.akosa@gmail.com>
*/
namespace Todolist.ContextDb
{
    public class TodolistDbColumnAttribute : Attribute
    {
        private string Name { get; set; }

        public TodolistDbColumnAttribute()
        {
        }

        public TodolistDbColumnAttribute(string name)
        {
            Name = name;
        }

        public static void GetAttribute()
        {
            GetAttribute(typeof(TodolistDbColumnAttribute));
        }

        public static string GetAttribute(Type t)
        {
            // Get instance of the attribute.
            TodolistDbColumnAttribute columnAttribute =
                (TodolistDbColumnAttribute)Attribute.GetCustomAttribute(t, typeof(TodolistDbColumnAttribute));

            if (columnAttribute == null)
            {
                return "The attribute was not found.";
            }
            else
            {
                return columnAttribute.Name;
            }
        }
    }
}