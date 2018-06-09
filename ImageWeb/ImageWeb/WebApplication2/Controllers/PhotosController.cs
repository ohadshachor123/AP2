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
                new Photo("C:\\Users\\Ohad\\Desktop\\Advanced2\\Output\\2011\\2\\d2.jpg", "C:\\Users\\Ohad\\Desktop\\Advanced2\\Output\\Thumbnail\\2011\\2\\d2.jpg", "d2", "2011", "2"),
                new Photo("C:\\Users\\Ohad\\Desktop\\Advanced2\\Output\\2011\\11\\d3.jpg", "C:\\Users\\Ohad\\Desktop\\Advanced2\\Output\\Thumbnail\\2011\\11\\d3.jpg", "d3", "2011", "11")
        };
            return View(model);
        }
    }
}