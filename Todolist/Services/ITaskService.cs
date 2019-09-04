using Todolist.Models;
using Todolist.ViewModels;

/*
 * 
 * @author Vladislav Kozlov <k2v.akosa@gmail.com>
*/
namespace Todolist.Services
{
    public interface ITaskService
    {
        void InitIModel(IModel model1, IModel model2);
        TasksVm GetTasks();
        void Add(TaskInput taskInput);
        void Edit(int id, TaskInput taskInput);
        void Remove(int id);
        TaskInput Get(int id);
    }
}
