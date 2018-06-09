using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class ConfigController : Controller
    {
        // GET: ConfigControl
        public ActionResult ConfigView()
        {
            List<String> h = new List<String>() { "AAA", "BBB" };
            ViewBag.OutDir = "O";
            ViewBag.Source = "S";
            ViewBag.Log = "L";
            ViewBag.Thumb = 50;
            return View(h);
        }
    }
}