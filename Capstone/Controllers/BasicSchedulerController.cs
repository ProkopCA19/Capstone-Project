using Capstone.Models;
using DHTMLX.Common;
using DHTMLX.Scheduler;
using DHTMLX.Scheduler.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using Microsoft.AspNet.Identity;

namespace Capstone.Controllers
{
    public class BasicSchedulerController : Controller
    {

      
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: BasicScheduler
        //public ActionResult Index(int?id)
        //{

        //    var sched = new DHXScheduler(this);
        //    sched.Skin = DHXScheduler.Skins.Flat;

        //    sched.Data.Loader.AddParameter("photographerId", id);


        //    sched.LoadData = true;
        //    sched.EnableDataprocessor = true;
        //    sched.InitialDate = DateTime.Now;

        //    sched.TimeSpans.Add(new DHXBlockTime()
        //    {
        //        StartDate = new DateTime(2018, 1, 1),
        //        EndDate = new DateTime(2018, 8, 24)
        //    });

        //    return View(sched);

        //}

        public ActionResult Index(int? id)
        {
            
            var sched = new DHXScheduler(this);
            sched.Skin = DHXScheduler.Skins.Flat;
            sched.EnableDynamicLoading(SchedulerDataLoader.DynamicalLoadingMode.Month);

            if (id != null)
            {
                sched.Data.Loader.AddParameter("photographerId", id);
                sched.Data.DataProcessor.AddParameter("photographerId", id);
            }
            else
            {
                var userId = User.Identity.GetUserId();
                var thisPhotographer = db.Photographers.Where(p => p.UserId == userId).FirstOrDefault();

                sched.Data.Loader.AddParameter("photographerId", thisPhotographer.Id);
                sched.Data.DataProcessor.AddParameter("photographerId", thisPhotographer.Id);
            }
            

            sched.LoadData = true;
            sched.EnableDataprocessor = true;


            sched.InitialDate = DateTime.Now;


            sched.TimeSpans.Add(new DHXBlockTime()
            {
                StartDate = new DateTime(2018, 1, 1),
                EndDate = new DateTime(2018, 8, 24),

            });


            sched.TimeSpans.Add(new DHXBlockTime()
            {
                Day = DayOfWeek.Saturday,
                Zones = new List<Zone>() { new Zone { Start = 0, End = 16 * 60 }, new Zone { Start = 18 * 60, End = 24 * 60 }}

            });

            sched.TimeSpans.Add(new DHXBlockTime()
            {
                Day = DayOfWeek.Friday,
                Zones = new List<Zone>() { new Zone { Start = 0, End = 17 * 60 }, new Zone { Start = 19 * 60, End = 24 * 60 } },
            });

            sched.TimeSpans.Add(new DHXBlockTime()
            {
                Day = DayOfWeek.Sunday,
                Zones = new List<Zone>() { new Zone { Start = 0, End = 8 * 60 }, new Zone { Start = 10 * 60, End = 13 * 60 }, new Zone { Start = 15 * 60, End = 24 * 60 } },
            });



            return View(sched);

        }

        public ContentResult Data(int photographerId)
        {

            Console.WriteLine("CALL ME MAYBE!");

            var userId = User.Identity.GetUserId();
            var currentUser = db.Users.Where(u => u.Id == userId).FirstOrDefault();

            //var appointments = db.Events.Where(e => e.PhotographerId == 1);
            //return new SchedulerAjaxData(appointments);

            if (currentUser != null && currentUser.UserRole == "Photographer")
            {
                var thisPhotographer = db.Photographers.Where(p => p.UserId == userId).FirstOrDefault();
                var thisAppointment = db.Events.Where(e => e.PhotographerId == thisPhotographer.Id).ToList();
                return new SchedulerAjaxData(thisAppointment);
            }
            else
            {
                var appointments = db.Events.Where(e => e.PhotographerId == photographerId).ToList();

                return new SchedulerAjaxData(appointments);
            }



        }


      
        public ActionResult Save(int photographerId, FormCollection actionValues)
        {
            var action = new DataAction(actionValues);

            var userId = User.Identity.GetUserId();
            var client = db.Clients.Where(c => c.UserId == userId).FirstOrDefault();
            var thisPhotographer = db.Photographers.Where(p => p.Id == photographerId).FirstOrDefault();


            try
            {
                var changedEvent = DHXEventsHelper.Bind<Event>(actionValues);
                switch (action.Type)
                {
                    case DataActionTypes.Insert:
                        changedEvent.PhotographerId = photographerId;
                        changedEvent.ClientId = client.Id;
                       
                        db.Events.Add(changedEvent);
                        break;
                    case DataActionTypes.Delete:
                        db.Entry(changedEvent).State = EntityState.Deleted;
                        break;
                    default:// "update"  
                        db.Entry(changedEvent).State = EntityState.Modified;
                        break;
                }
                db.SaveChanges();
                action.TargetId = changedEvent.Id;
            }
            catch (Exception)
            {
                action.Type = DataActionTypes.Error;
            }

            return (new AjaxSaveResponse(action));
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
    
