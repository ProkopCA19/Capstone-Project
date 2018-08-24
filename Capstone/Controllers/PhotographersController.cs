﻿using Capstone.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Capstone.Controllers
{
    public class PhotographersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Coupons
        public ActionResult Index()
        {
            return View(db.Photographers.ToList());
        }



        public ActionResult FilteredPhotographers(string searchString, bool? searchBy, bool? searchBy2, bool? searchBy3, bool? searchBy4)
        {
         

            var photographerList = from s in db.Photographers
                               select s;
            List<string> photoList = new List<string>();

            if (searchBy == true)
            {
                photographerList = db.Photographers.Where(p => p.PriceRange1 == searchBy);


                
            }
            else if (searchBy2 == true )
            {
                photographerList = db.Photographers.Where(p => p.PriceRange2 == searchBy2);
               
            }
            else if (searchBy3 == true)
            {
                photographerList = db.Photographers.Where(p => p.PriceRange3 == searchBy3);
               
            }
            else if (searchBy4 == true)
            {
                photographerList = db.Photographers.Where(p => p.PriceRange4 == searchBy4);
                
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                photographerList = photographerList.Where(s => s.Zipcode.ToString().Contains(searchString));

                foreach (var p in photographerList)
                {
                    string photographerAddress = p.Address + " " + p.City + " " + p.State + " " + p.Zipcode.ToString();
                    photoList.Add(photographerAddress);

                }

                photoList.ToArray();
                ViewBag.PhotoList = photoList;

                return View(photographerList);
            }


            
        
            foreach (var p in photographerList)
            {
                string photographerAddress = p.Address + " " + p.City + " " + p.State + " " + p.Zipcode.ToString();
                photoList.Add(photographerAddress);

            }

            photoList.ToArray();
            ViewBag.PhotoList = photoList;
            

            return View(photographerList.ToList());
        }
    


    public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Photographer photographer = db.Photographers.Find(id);
            if (photographer == null)
            {
                return HttpNotFound();
            }
            return View(photographer);
        }

        // GET: Coupons/Create
        public ActionResult Create()
        {

            ViewBag.AppointmentId = new SelectList(db.Appointments, "Id", "Id");
            return View();
            
        }

        // POST: Coupons/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserId,FirstName,LastName,Email,Address,City,State,Zipcode,PriceRange1,PriceRange2,PriceRange3,PriceRange4,AppointmentId,Bio")] Photographer photographer)
        {
            if (ModelState.IsValid)
            {
                photographer.UserId = User.Identity.GetUserId();
                db.Photographers.Add(photographer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AppointmentId = new SelectList(db.Appointments, "Id", "Id", photographer.AppointmentId);
            return View(photographer);
        }

        // GET: Coupons/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Photographer photographer = db.Photographers.Find(id);
            if (photographer == null)
            {
                return HttpNotFound();
            }
            return View(photographer);
        }

        // POST: Coupons/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,FirstName,LastName,Email,Address,City,State,Zipcode,PriceRange1,PriceRange2,PriceRange3,PriceRange4,Bio")] Photographer photographer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(photographer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(photographer);
        }

        // GET: Coupons/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Photographer photographer = db.Photographers.Find(id);
            if (photographer == null)
            {
                return HttpNotFound();
            }
            return View(photographer);
        }

        // POST: Coupons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Photographer photographer = db.Photographers.Find(id);
            db.Photographers.Remove(photographer);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


        public void UpdateAccountBalance()
        {
            var userId = User.Identity.GetUserId();
            var thisPhotographer = db.Photographers.Where(p => p.UserId == userId).FirstOrDefault();
            var clients = db.Events.Include(inc => inc.Client).Where(e => e.PhotographerId == thisPhotographer.Id).Select(m => m.Client);

            foreach (var c in clients)
            {
                if (c.AppointmentStatus == true)
                {
                    thisPhotographer.AccountBalance += 50;
                    db.SaveChanges();
                }
            }
        }


        public ActionResult Myinformation()
        {
            var userId = User.Identity.GetUserId();
            var thisPhotographer = db.Photographers.Where(p => p.UserId == userId);
            return View(thisPhotographer);

        }


    }
}
