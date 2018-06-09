using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;
using WebApplication2.Communication;
namespace WebApplication2.Controllers
{
    public class LogsController : Controller
    {
        // GET: Logs
        public ActionResult LogsView()
        {
            List<Log> logs = BackendSettings.GetInstance().Logs;
            return View(logs);
        }
    }
}