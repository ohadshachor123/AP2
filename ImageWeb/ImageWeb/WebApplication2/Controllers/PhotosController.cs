using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;
using WebApplication2.Communication;
namespace WebApplication2.Controllers
{
    public class PhotosController : Controller
    {
        // GET: Photos
        public ActionResult PhotosView()
        {

            List<Photo> model = Tools.PhotosInDir(BackendSettings.GetInstance().OutputDir)
            return View(model);
        }
    }
}