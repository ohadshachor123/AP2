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
        private static string projectPath;
        // GET: Photos
        public ActionResult PhotosView()
        {
            // Waiting for the service to send the output dir if it hasn't sent it so fat.
            while (BackendSettings.GetInstance().OutputDir == null && BackendSettings.GetInstance().IsOnline) { Thread.Sleep(500); }

            // The projects path in the computer 
            projectPath = Server.MapPath("~");
            List<Photo> model = Tools.PhotosInDir(BackendSettings.GetInstance().OutputDir);
            foreach (Photo photo in model)
            {
                // Updating the photo object to point to the relative path so we can show it on the web page.
                photo.ThumbnailPath = "~//" + photo.ThumbnailPath.Replace(projectPath, String.Empty);
                photo.Path = "~//" + photo.Path.Replace(projectPath, String.Empty);
            }
            return View(model);
        }

        // Get: The page that views exactly one photo.
        public ActionResult ViewPhoto(string path, string thumb, string name, string month, string year)
        {
            Photo model = new Photo(path, thumb, name, year, month);
            if (path != null) {
                return View(model);
            }
            return RedirectToAction("PhotosView");
        }

        // Get: The page that asks you to confirm that you want to delete the photo. 
        public ActionResult ConfirmDeleteView(string name, string thumb, string path)
        {
            // Year/month are irrelevant here. So I gave them null.
            Photo model = new Photo(path, thumb, name, null, null);
            if (name != null)
            {
                return View(model);
            }
            return RedirectToAction("PhotosView");
        }

        // Post- delete the photo inside the path
        public ActionResult DeletePhoto(string thumb)
        {
            // manually created the thumbnail path which is exactly one after the output dir's path
            string path = thumb.Replace("Thumbnail\\", String.Empty);
            // Replacing the image path to its absolute one, and removing.
            Tools.RemoveImage(path.Replace("~//",projectPath), thumb.Replace("~//", projectPath));
            return RedirectToAction("PhotosView");
        } 
    }
}