using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class LogsController : Controller
    {
        // GET: Logs
        public ActionResult LogsView()
        {
            List<Log> logs = new List<Log>()
            {
                new Log("WARNING", "This is an EXAMPLE"),
                new Log("INFO", "The service is running"),
                new Log("WARNING", "This is a second warning for you")
            };
            return View(logs);
        }
    }
}