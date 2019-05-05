using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.App_Start;
using WebApplication2.Models;
using WebApplication2.Rentals;

namespace WebApplication2.Controllers
{
    public class RentalsController : Controller
    {
        public readonly RealEstateContext realEstateContext = new RealEstateContext();

        public ActionResult Index()
        {
            var rentals = realEstateContext.Rentals.Find(_=>true).ToList();
            return View(rentals);
        }


        // GET: Rentals
        public ActionResult Post()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Post(PostRental postRental)
        {
            var rental = new Rentals.Rental();
            rental.Description = postRental.Description;
            rental.NumberOfRooms = postRental.NumberOfRooms;
            rental.Price = postRental.Price;
            rental.Address = (postRental.Address ?? string.Empty).Split('\n').ToList();

            realEstateContext.Rentals.InsertOne(rental);

           return RedirectToAction("Index");
        }

        public ActionResult AdjustPrice(string id)
        {
            var query_id = Builders<Rental>.Filter.Eq("_id", ObjectId.Parse(id));
            var rental = realEstateContext.Rentals.Find(query_id, new FindOptions()).FirstOrDefault();
            return View(rental);
        }

        [HttpPost]
        public ActionResult AdjustPrice(string id, AdjustPrice adjustPrice)
        {
            var query_id = Builders<Rental>.Filter.Eq("_id", ObjectId.Parse(id));
            var rental = realEstateContext.Rentals.Find(query_id, new FindOptions()).FirstOrDefault();
            var updateDef =  rental.AdjustPriceUsingModif(adjustPrice);
            realEstateContext.Rentals.UpdateOne(query_id, updateDef);
            return RedirectToAction("Index");
        }


        public ActionResult Delete(string id)
        {
            var query_id = Builders<Rental>.Filter.Eq("_id", ObjectId.Parse(id));
            realEstateContext.Rentals.FindOneAndDelete(query_id);

            return RedirectToAction("Index");
        }
    }
}