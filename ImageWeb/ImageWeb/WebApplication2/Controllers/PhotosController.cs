using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class PhotosController : Controller
    {
        // GET: Photos
        public ActionResult PhotosView()
        {
            List<Photo> model = new List<Photo>()
            {
                new Photo("~/Output/2011/11/d3.jpg", "~/Output/Thumbnail/2011/11/d3.jpg", "d2", "2011", "2"),
                new Photo("~/Output/2011/2/d2.jpg", "~/Output/Thumbnail/2011/2/d2.jpg", "d3", "2011", "11")
        };
            return View(model);
        }
    }
}