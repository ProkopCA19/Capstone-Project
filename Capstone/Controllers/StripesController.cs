using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Capstone.Controllers
{
    public class StripesController : Controller
    {
        // GET: Stripes
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Charge()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult Charge(string apikey)
        {

            return RedirectToAction("Index", "Home");
        }

    }
}