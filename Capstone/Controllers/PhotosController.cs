using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using Capstone.Models;
using Microsoft.AspNet.Identity;

namespace Capstone.Controllers
{
    public class PhotosController : Controller
    {
        // GET: Photos

        ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]

        public ActionResult Index(HttpPostedFileBase file)
        {
            try
            {
                Photo photo = new Photo();

                var currentUserId = User.Identity.GetUserId();
                var thePhotographer = db.Photographers.Where(p => p.UserId == currentUserId).FirstOrDefault();
                photo.PhotographerId = thePhotographer.Id;

                
                photo.Title = Path.GetFileName(file.FileName);
                photo.PhotoPath = Path.Combine(Server.MapPath("~/Images"), photo.Title);


                file.SaveAs(Server.MapPath("Images/" + photo.Title));


                db.Photos.Add(photo);
                db.SaveChanges();
           
                ViewBag.Message = "Uploaded file successfully saved!";
                return View();
            }
            catch
            {
                ViewBag.Message = "Uploaded file not saved :(";
                return View();
            }



        }
    }
}