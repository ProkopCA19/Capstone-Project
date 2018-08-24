using Capstone.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Capstone.Controllers
{
    public class PhotosController : Controller
    {
     

            ApplicationDbContext db = new ApplicationDbContext();


            public ActionResult Create()
            {
                return View();
            }


            [HttpPost]
            public ActionResult Create(HttpPostedFileBase file)
            {
                try
                {
                    Photo photo = new Photo();

                    var currentUserId = User.Identity.GetUserId();
                    var thePhotographer = db.Photographers.Where(p => p.UserId == currentUserId).FirstOrDefault();
                    photo.PhotographerId = thePhotographer.Id;


                    photo.Title = Path.GetFileName(file.FileName);

                    photo.PhotoURLPath = "/Images/" + photo.Title;  
                  
                    photo.PhotoPath = Path.Combine(Server.MapPath("~/Images"), photo.Title);
                    

                    file.SaveAs(photo.PhotoPath);


                    db.Photos.Add(photo);
                    db.SaveChanges();

                    ViewBag.Message = "Uploaded file successfully saved!";
                    return RedirectToAction("ShowGallery");

                }
                catch
                {
                    ViewBag.Message = "Uploaded file not saved :(";
                    return View();
                }

            }

        public ActionResult ShowGallery(int?id)
        {
            if(id == null)
            {
                var currentUserId = User.Identity.GetUserId();

                var thisPhotographer = db.Photographers.Where(p => p.UserId == currentUserId).FirstOrDefault();

                var thesePhotos = db.Photos.Where(i => i.PhotographerId == thisPhotographer.Id);

                return View(thesePhotos);

            }
            else
            {
                var thesePhotos = db.Photos.Where(p => p.PhotographerId == id);
                return View(thesePhotos);
            }
         
        }







    }









}