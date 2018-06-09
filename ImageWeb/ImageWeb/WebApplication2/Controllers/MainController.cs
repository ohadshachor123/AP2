using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;
using WebApplication2.Communication;
namespace WebApplication2.Controllers
{
    public class MainController : Controller
    {
        // GET: Main
        public ActionResult MainView()
        {
            ICommunicationAdapter data = BackendSettings.GetInstance();
            var model = new List<Student>()
            {
                new Student(322952433, "Ohad", "Shachor")
            };
            if (data.IsOnline)
                ViewBag.Status = "Online";
            else
                ViewBag.Status = "Offline";
            ViewBag.ImageCount = Tools.countImagesInDir(data.OutputDir);
            return View(model);
        }

        public ActionResult Index()
        {
            return MainView();
        }

    }
}