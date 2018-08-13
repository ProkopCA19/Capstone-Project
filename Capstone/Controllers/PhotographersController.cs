using Capstone.Models;
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


        public ActionResult FilterPhotographers()
        {
            var userId = User.Identity.GetUserId();
            var client = db.Clients.Where(c => c.UserId == userId).FirstOrDefault();

            if(client.PriceRange1 == true)
            {
                var photographerList = db.Photographers.Where(p => p.PriceRange1 == true);
                return View(photographerList);
            }
            else if(client.PriceRange2 == true)
            {
                var photographerList = db.Photographers.Where(p => p.PriceRange2 == true);
                return View(photographerList);
            }
            else if (client.PriceRange4 == true)
            {
                var photographerList = db.Photographers.Where(p => p.PriceRange4 == true);
                return View(photographerList);
            }
            else
            {
                return View();
            }



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
            return View();
        }

        // POST: Coupons/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Value,Name")] Photographer photographer)
        {
            if (ModelState.IsValid)
            {
                db.Photographers.Add(photographer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

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
        public ActionResult Edit([Bind(Include = "Id,Value,Name")] Photographer photographer)
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
    }
}
