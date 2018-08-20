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
        public ActionResult Index()
        {

            var sched = new DHXScheduler(this);
            sched.Skin = DHXScheduler.Skins.Terrace;
            sched.LoadData = true;
            sched.EnableDataprocessor = true;
            sched.InitialDate = new DateTime(2016, 5, 5);
            return View(sched);
          
        }

        
        public ActionResult Data(int? Id)
        {
            var photographer = db.Photographers.Where(p => p.Id == 1).FirstOrDefault();
            var appointment = db.Events.Where(e=>e.PhotographerId == photographer.Id).ToList();

            return new SchedulerAjaxData(appointment);

        }

        [Authorize(Roles ="Photographer")]
        public ActionResult Save( int? id, FormCollection actionValues)
        {
            var action = new DataAction(actionValues);

            var userId = User.Identity.GetUserId();
            var photographer = db.Photographers.Where(p => p.UserId == userId).FirstOrDefault();


            try
            {
                var changedEvent = DHXEventsHelper.Bind<Event>(actionValues);
                switch (action.Type)
                {
                    case DataActionTypes.Insert:
                        changedEvent.PhotographerId = photographer.Id;
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
    
