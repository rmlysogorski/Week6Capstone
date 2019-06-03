using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Week6Capstone.Models;

namespace Week6Capstone.Controllers
{
    public class TaskController : Controller
    {
        // GET: Task
        public ActionResult Index()
        {
            if (TempData["SearchResults"] != null)
            {
                ViewBag.TaskList = TempData["SearchResults"];
            }
            else
            {
                List<Task> taskList = DAO.GetTasksAsList((int)Session["UserId"]);
                taskList.Sort((x, y) => x.Description.CompareTo(y.Description));
                ViewBag.TaskList = taskList;
            }
            return View();
        }

        public ActionResult AddTask()
        {
            if (TempData["Message"] != null)
            {
                ViewBag.Message = TempData["Message"];
            }
            return View();
        }

        public ActionResult AddTaskToDb(Task newTask)
        {
            if (ModelState.IsValid)
            {
                DAO.AddTaskToDb(newTask);
                TempData["Message"] = "Task saved successfully.";

            }
            else
            {
                TempData["Message"] = "Something went wrong.";
            }

            return RedirectToAction("Index");
        }

        public ActionResult DeleteTask(int TaskId)
        {
            DAO.DeleteTaskFromDb(TaskId);
            return RedirectToAction("Index");
        }

        public ActionResult MarkComplete(int Id)
        {
            if (Id != -1)
            {
                DAO.MarkCompleted(Id);
            }
            return RedirectToAction("Index");
        }

        public ActionResult SearchDesc(string searchString)
        {
            int UserId;
            if (Session["UserId"] != null)
            {
                UserId = (int)Session["UserId"];
            }
            else
            {
                UserId = -1;
            }

            if (!string.IsNullOrEmpty(searchString))
            {
                TempData["SearchResults"] = DAO.SearchDesc(searchString, UserId);
            }

            return RedirectToAction("Index");
        }

        public ActionResult SortByDate()
        {
            List<Task> taskList = DAO.GetTasksAsList((int)Session["UserId"]);
            taskList.Sort((x, y) => x.DueDate.CompareTo(y.DueDate));
            ViewBag.TaskList = taskList;
            return View("Index");
        }

        public ActionResult SortByComplete()
        {
            List<Task> taskList = DAO.GetTasksAsList((int)Session["UserId"]);
            taskList.Sort((x, y) => x.Complete.CompareTo(y.Complete));
            ViewBag.TaskList = taskList;
            return View("Index");
        }

    }
}