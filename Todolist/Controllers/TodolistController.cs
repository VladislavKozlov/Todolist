using System;
using System.Data.Entity.Infrastructure;
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
        private TodolistModel _todolistToUpdate;
        private string _validationErrors;


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
            ViewBag.Title = "Добавление задачи";
            return PartialView("_Create");
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
                    _validationErrors = string.Join(",", ModelState.Values.Where(E => E.Errors.Count > 0).SelectMany(E => E.Errors).Select(E => E.ErrorMessage).ToArray());
                    return Json(new { EnableError = true, ErrorMsg = _validationErrors });
                }
                return Json(new { EnableSuccess = true, SuccessMsg = "Задача успешно создана!" });
            }
            catch (RetryLimitExceededException)
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
            TaskVm taskVm = new TaskVm(todolist);
            if (todolist == null || taskVm == null)
            {
                return View("~/Error");
            }
            ViewBag.Title = "Редактирование задачи";
            return PartialView("_Edit", taskVm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Edit(int? id, TaskInput taskInput)
        {
            try
            {                
                _todolistToUpdate = _db.Todos.Find(id);
                if (ModelState.IsValid)
                {
                    _todolistToUpdate.TaskDescription = taskInput.TaskDescription;
                    _todolistToUpdate.EnrollmentDate = DateTime.Now;
                    _todolistToUpdate.Approved = taskInput.Approved;
                    _db.SaveChanges();
                }
                else
                {
                    _validationErrors = string.Join(",", ModelState.Values.Where(E => E.Errors.Count > 0).SelectMany(E => E.Errors).Select(E => E.ErrorMessage).ToArray());
                    return Json(new { EnableError = true, ErrorMsg = _validationErrors });
                }
                return Json(new { EnableSuccess = true, SuccessMsg = "Задача успешно отредактирована!" });
            }
            catch (RetryLimitExceededException)
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
            TaskVm taskVm = new TaskVm(todolist);
            if (todolist == null || taskVm == null)
            {
                return View("~/Error");
            }
            ViewBag.Title = "Удаление задачи";
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
            catch (RetryLimitExceededException)
            {
                return Json(new { EnableError = true, ErrorMsg = "Удаление не произошло, попробуйте ещё раз или обратитесь к системному администратору!" });
            }
            return Json(new { EnableSuccess = true, SuccessMsg = "Задача успешно удалена!" });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
