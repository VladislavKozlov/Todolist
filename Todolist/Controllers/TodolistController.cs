using System;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web.Mvc;
using Todolist.Models;
using Todolist.Models.ContextDb;

/*
 * 
 * @author Vladislav Kozlov <k2v.akosa@gmail.com>
*/
namespace Todolist.Controllers
{
    public class TodolistController : Controller
    {
        private TodolistModel _todolistToUpdate;

        private TodolistDbContext _db = new TodolistDbContext();

        public ActionResult Index()
        {
            try
            {
                var tasks =
                (from item in _db.Todos
                 orderby item.EnrollmentDate descending
                 select item).ToList();

                ViewBag.Tasks = tasks;
                return View();
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
                var tasks =
                (from item in _db.Todos
                 orderby item.EnrollmentDate descending
                 select item).ToList();

                ViewBag.Tasks = tasks;
                return PartialView("_PartialContent");
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

        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public JsonResult CreatePost(TodolistModel todolist)
        {
            try
            {
                if (string.IsNullOrEmpty(todolist.TaskDescription))
                {
                    return Json(new { EnableError = true, ErrorMsg = "Пожалуйста, введите текст задачи!" });
                }
                else if (todolist.TaskDescription.Length > 100)
                {
                    return Json(new { EnableError = true, ErrorMsg = "Описание задачи не может быть больше 100 символов!" });
                }
                if (ModelState.IsValid)
                {
                    todolist.EnrollmentDate = DateTime.Now;
                    _db.Todos.Add(todolist);
                    _db.SaveChanges();
                }
                else
                {
                    return Json(new { EnableError = true, ErrorMsg = "Что-то идет неправильно, пожалуйста ещё раз проверьте введённые данные!" });
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
            if (todolist == null)
            {
                return View("~/Error");
            }
            ViewBag.Title = "Редактирование задачи";
            return PartialView("_Edit", todolist);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public JsonResult EditPost(int? id, TodolistModel todolist)
        {
            try
            {
                if (string.IsNullOrEmpty(todolist.TaskDescription))
                {
                    return Json(new { EnableError = true, ErrorMsg = "Пожалуйста, введите текст задачи!" });
                }
                else if (todolist.TaskDescription.Length > 100)
                {
                    return Json(new { EnableError = true, ErrorMsg = "Описание задачи не может быть больше 100 символов!" });
                }
                _todolistToUpdate = _db.Todos.Find(id);
                if (TryUpdateModel(_todolistToUpdate, "", new string[] { "TaskDescription", "EnrollmentDate", "Approved" }))
                {
                    _todolistToUpdate.EnrollmentDate = DateTime.Now;
                    _db.SaveChanges();
                }
                else
                {
                    return Json(new { EnableError = true, ErrorMsg = "Что-то идет неправильно, пожалуйста ещё раз проверьте введённые данные!" });
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
            if (todolist == null)
            {
                return View("~/Error");
            }
            ViewBag.Title = "Удаление задачи";
            return PartialView("_Delete", todolist);
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
