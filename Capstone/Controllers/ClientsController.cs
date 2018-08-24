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
    public class ClientsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Coupons
        public ActionResult Index()
        {
            return View(db.Clients.ToList());
        }

        // GET: Coupons/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
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
        public ActionResult Create([Bind(Include = "Id,UserId,FirstName,LastName,Email,Address,City,State,Zipcode")] Client client)
        {
            if (ModelState.IsValid)
            {
                client.UserId = User.Identity.GetUserId();
                db.Clients.Add(client);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(client);
        }

        // GET: Coupons/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // POST: Coupons/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,FirstName,LastName,Email,Address,City,State,Zipcode,AppointmentStatus")] Client client)
        {



            if (ModelState.IsValid)
            {
                db.Entry(client).State = EntityState.Modified;
                db.SaveChanges();
                if (User.IsInRole("Photographer"))
                {
                    var controller = DependencyResolver.Current.GetService<PhotographersController>();
                    controller.ControllerContext = new ControllerContext(this.Request.RequestContext, controller);
                    controller.UpdateAccountBalance();

                    return RedirectToAction("PhotographersClientList");
                }
                return RedirectToAction("Index","Home");
            }
            return View(client);
        }


       




        // GET: Coupons/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // POST: Coupons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Client client = db.Clients.Find(id);
            db.Clients.Remove(client);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult Email()
        {
            return View();
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


        public ActionResult PhotographersClientList()
        {
            var userId = User.Identity.GetUserId();
            var thisPhotographer = db.Photographers.Where(p => p.UserId == userId).FirstOrDefault();
            var myEvents = db.Events.Include(inc=> inc.Client).Where(e => e.PhotographerId == thisPhotographer.Id).Select(m=>m.Client).ToList();
            
            return View(myEvents);

        }

        public ActionResult ClientInformation()
        {
            var userId = User.Identity.GetUserId();
            var thisClient = db.Clients.Where(p => p.UserId == userId);
            return View(thisClient);
        }

        public ActionResult MyAppointments()
        {
            var userId = User.Identity.GetUserId();
            var thisClient = db.Clients.Where(p => p.UserId == userId).FirstOrDefault();
            var myEvent = db.Events.Where(e => e.ClientId == thisClient.Id).FirstOrDefault();

            DateTime eventDay = myEvent.StartDate;
            return View(eventDay);

        }


    }
}
