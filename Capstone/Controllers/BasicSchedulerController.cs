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


        public ActionResult Index(int? id)
        {

            var sched = new DHXScheduler(this);
            sched.Skin = DHXScheduler.Skins.Flat;
            sched.EnableDynamicLoading(SchedulerDataLoader.DynamicalLoadingMode.Month);

            if (id != null)
            {
                sched.Data.Loader.AddParameter("photographerId", id);

            }

            sched.Data.DataProcessor.AddParameter("photographerId", id);

            sched.LoadData = true;
            sched.EnableDataprocessor = true;


            sched.InitialDate = DateTime.Now;

            sched.TimeSpans.Add(new DHXBlockTime()
            {
                StartDate = new DateTime(2018, 1, 1),
                EndDate = new DateTime(2018, 8, 24)
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
            //var photographer = db.Photographers.Find(photographerId);


            try
            {
                var changedEvent = DHXEventsHelper.Bind<Event>(actionValues);
                switch (action.Type)
                {
                    case DataActionTypes.Insert:
                        changedEvent.PhotographerId = photographerId;
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
    
