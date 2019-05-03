using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using WebApplication2.Properties;

namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            var person = new BsonDocument();
            person.Add("firstName", new BsonString("Lol"));
            person.Add("Age", new BsonInt32(12));
            person.Add("Age2", new BsonDateTime(DateTime.Now));
            person.Add("Hooby", new BsonArray((new string[] { "Football", "Music" })));

            return Json(person, JsonRequestBehavior.AllowGet);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}