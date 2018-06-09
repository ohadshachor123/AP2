using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;
using WebApplication2.Communication;
using System.Threading;

namespace WebApplication2.Controllers
{
    public class MainController : Controller
    {
        private const string filePath = "App_Data\\info.txt";
        // GET: Main
        public ActionResult MainView()
        {
            ICommunicationAdapter data = BackendSettings.GetInstance();
            string projectPath = Server.MapPath("~");

            var model = Tools.StudentsFromFile(projectPath + "\\" + filePath);
            if (data.IsOnline)
                ViewBag.Status = "Online";
            else
                ViewBag.Status = "Offline";
            while (data.OutputDir == null && data.IsOnline) { Thread.Sleep(200); }
            ViewBag.ImageCount = Tools.CountImagesInDir(data.OutputDir);
            return View(model);
        }

        public ActionResult Index()
        {
            return MainView();
        }

    }
}