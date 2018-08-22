using System.Web.Mvc;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Capstone.Models;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Security;

namespace Capstone.Controllers
{
    public class EmailsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Emails
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Contact(Email model)
        {
            if (ModelState.IsValid)
            {
                var body = "<p>Email From: {0} ({1})</p><p>Message:</p><p>{2}</p>";
                var message = new MailMessage();
                message.To.Add(new MailAddress("ProkopCA19@gmail.com"));  // replace with valid value 
                message.From = new MailAddress(model.FromEmail);  // replace with valid value
                message.Subject = "Your email subject";
                message.Body = string.Format(body, model.FromName, model.FromEmail, model.Message);
                message.IsBodyHtml = true;

                var userId = User.Identity.GetUserId();
                var currentUser = db.Users.Where(u => u.Id == userId).FirstOrDefault();

       

                using (var smtp = new SmtpClient())
                {
                    var credential = new NetworkCredential
                    {
                        //UserName = currentUser.Email,  // replace with valid value
                        //Password = "123.Abc"  // replace with valid value
                    };

                    smtp.UseDefaultCredentials = false;
                    credential.UserName = "ChelseaProkop@Outlook.com";
                    credential.Password = "SoccerLover19";
                    //credential.Domain = "outlook.com";
                    smtp.Credentials = credential;
                    smtp.Host = "smtp.office365.com";
                    smtp.Port = 587;
                    smtp.EnableSsl = true;
                   
                    await smtp.SendMailAsync(message);
                    return RedirectToAction("Sent");
                }
            }
            return View(model);
        }

        public ActionResult Sent()
        {
            return View();
        }

    }
}