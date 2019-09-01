using System;
using System.Linq;
using System.Web.Mvc;
using Todolist.ContextDb;
using Todolist.Models;
using Todolist.ViewModels;

/*
 * 
 * @author Vladislav Kozlov <k2v.akosa@gmail.com>
*/
namespace Todolist.Controllers
{
    public class TodolistController : Controller
    {
        private TodolistDbContext _db = new TodolistDbContext();

        public ActionResult Index()
        {
            try
            {
                var tasks = _db.Todos.OrderByDescending(item => item.EnrollmentDate).ToList();
                TasksVm tasksVm = new TasksVm { Tasks = tasks };

                return View(tasksVm);
            }
            catch (Exception)
            {
                ViewBag.Error = "Ошибка доступа к данным!";
                return View();
            }
        }

        public ActionResult PartialContent()
        {
            try
            {
                var tasks = _db.Todos.OrderByDescending(item => item.EnrollmentDate).ToList();
                TasksVm tasksVm = new TasksVm { Tasks = tasks };

                return PartialView("_PartialContent", tasksVm);
            }
            catch (Exception)
            {
                ViewBag.Error = "Ошибка доступа к данным!";
                return PartialView("_PartialContent");
            }
        }

        public ActionResult Create()
        {
            TaskVm taskVm = new TaskVm();
            taskVm.Title = "Добавление задачи";
            return PartialView("_Create", taskVm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Create(TaskInput taskInput)
        {
            try
            {
                TodolistModel todolist = new TodolistModel();
                if (ModelState.IsValid)
                {
                    todolist.TaskDescription = taskInput.TaskDescription;
                    todolist.EnrollmentDate = DateTime.Now;
                    todolist.Approved = taskInput.Approved;
                    _db.Todos.Add(todolist);
                    _db.SaveChanges();
                }
                else
                {
                    string validationErrors = string.Join(",", ModelState.Values.Where(E => E.Errors.Count > 0).SelectMany(E => E.Errors).Select(E => E.ErrorMessage).ToArray());
                    return Json(new { EnableError = true, ErrorMsg = validationErrors });
                }
                return Json(new { EnableSuccess = true, SuccessMsg = "Задача успешно создана!" });
            }
            catch (Exception)
            {
                return Json(new { EnableError = true, ErrorMsg = "Что-то идет неправильно, попробуйте ещё раз или обратитесь к системному администратору!" });
            }
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return View("~/Error");
            }
            TodolistModel todolist = _db.Todos.Find(id);
            if (todolist == null)
            {
                return View("~/Error");
            }
            TaskVm taskVm = new TaskVm(todolist);
            taskVm.Title = "Редактирование задачи";
            return PartialView("_Edit", taskVm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Edit(int? id, TaskInput taskInput)
        {
            try
            {
                var todolistToUpdate = _db.Todos.Find(id);
                if (ModelState.IsValid)
                {
                    todolistToUpdate.TaskDescription = taskInput.TaskDescription;
                    todolistToUpdate.EnrollmentDate = DateTime.Now;
                    todolistToUpdate.Approved = taskInput.Approved;
                    _db.SaveChanges();
                }
                else
                {
                    string validationErrors = string.Join(",", ModelState.Values.Where(E => E.Errors.Count > 0).SelectMany(E => E.Errors).Select(E => E.ErrorMessage).ToArray());
                    return Json(new { EnableError = true, ErrorMsg = validationErrors });
                }
                return Json(new { EnableSuccess = true, SuccessMsg = "Задача успешно отредактирована!" });
            }
            catch (Exception)
            {
                return Json(new { EnableError = true, ErrorMsg = "Что-то идет неправильно, попробуйте ещё раз или обратитесь к системному администратору!" });
            }
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return View("~/Error");
            }
            TodolistModel todolist = _db.Todos.Find(id);
            if (todolist == null)
            {
                return View("~/Error");
            }
            TaskVm taskVm = new TaskVm(todolist);
            taskVm.Title = "Удаление задачи";
            return PartialView("_Delete", taskVm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Delete(int id)
        {
            try
            {
                TodolistModel todolist = _db.Todos.Find(id);
                _db.Todos.Remove(todolist);
                _db.SaveChanges();
            }
            catch (Exception)
            {
                return Json(new { EnableError = true, ErrorMsg = "Удаление не произошло, попробуйте ещё раз или обратитесь к системному администратору!" });
            }
            return Json(new { EnableSuccess = true, SuccessMsg = "Задача успешно удалена!" });
        }
    }
}
