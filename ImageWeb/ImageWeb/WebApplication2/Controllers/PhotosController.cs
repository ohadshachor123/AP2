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
    public class PhotosController : Controller
    {
        // GET: Photos
        public ActionResult PhotosView()
        {
            while (BackendSettings.GetInstance().OutputDir == null && BackendSettings.GetInstance().IsOnline) { Thread.Sleep(500); }
            string projectPath = Server.MapPath("~");
            List<Photo> model = Tools.PhotosInDir(BackendSettings.GetInstance().OutputDir);
            foreach (Photo photo in model)
            {
                photo.ThumbnailPath = "~//" + photo.ThumbnailPath.Replace(projectPath, String.Empty);
                photo.Path = "~//" + photo.Path.Replace(projectPath, String.Empty);
            }
            return View(model);
        }

        public ActionResult ViewPhoto(string path, string thumb, string name, string month, string year)
        {
            Photo model = new Photo(path, thumb, name, year, month);
            if (path != null) {
                return View(model);
            }
            return RedirectToAction("PhotosView");
        }

        public ActionResult ConfirmDeleteView(string name, string thumb, string path)
        {
            Photo model = new Photo(path, thumb, name, null, null);
            if (name != null)
            {
                return View(model);
            }
            return RedirectToAction("PhotosView");
        }

        public ActionResult DeletePhoto(string path, string thumb)
        {
            Thread.Sleep(200);
            return RedirectToAction("PhotosView");
        } 
    }
}