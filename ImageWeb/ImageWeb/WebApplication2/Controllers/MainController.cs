using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class MainController : Controller
    {
        // GET: Main
        public ActionResult MainView()
        {
            var model = new List<Student>()
            {
                new Student(000, "first here", "Enter last"),
                new Student(322952433, "Ohad", "Shachor")
            };
            ViewBag.Status = "Offline";
            ViewBag.ImageCount = 50;
            return View(model);
        }

        public ActionResult Index()
        {
            return MainView();
        }
    }
}