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
    public class ConfigController : Controller
    {
        // GET: ConfigControl
        public ActionResult ConfigView()
        {
            ICommunicationAdapter data = BackendSettings.GetInstance();
            List<String> handlers = data.Handlers;
            while (data.OutputDir == null && data.IsOnline) { Thread.Sleep(500); }
            ViewBag.OutDir = data.OutputDir;
            ViewBag.Source = data.SourceName;
            ViewBag.Log = data.LogName;
            ViewBag.Thumb = data.ThumbnailSize;
            return View(handlers);
        }

        // Post- I want to delete a handler.
        public ActionResult HandlerPressed(string handler)
        {
            ViewBag.Handler = handler;
            //Returns the confirmation view, which asks if you want to delete the handler.
            return View();
        }

        [HttpPost]
        public ActionResult DeleteHandler(string handler)
        {
            return RedirectToAction("ConfigView");
        }

        [HttpPost]
        public ActionResult RedirectToConfig()
        {
            return RedirectToAction("ConfigView");
        }
    }
}