using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
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

        public ActionResult Index(RentalsFilter rentalsFilter)
        {
            var rentals = FilterRentals(rentalsFilter);
            var model = new RentalsList()
            {
                Rentals = rentals,
                RentalsFilter = rentalsFilter
            };
            return View(model);
        }

        private List<Rental> FilterRentals(RentalsFilter rentalsFilter)
        {
            List<Rental> rentals = null;
            if (!rentalsFilter.PriceLimit.HasValue)
            {
                rentals = realEstateContext.Rentals.Find(_ => true).ToList();
            }
            else
            {
                //    var filter = Builders<Rental>.Filter.Lte("Price", rentalsFilter.PriceLimit.Value);
                //    var sorter = Builders<Rental>.Sort.Ascending(r=>r.Price);
                //    rentals = realEstateContext.Rentals.FindSync(filter, new FindOptions<Rental, Rental>() { Sort = sorter }).ToList();
                //}

                rentals = realEstateContext.Rentals.AsQueryable<Rental>().Where(r => r.Price <= rentalsFilter.PriceLimit.Value).OrderBy(r => r.Price).ToList();
            }
            return rentals;
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

        public ActionResult GroupRentals()
        {
            var ranges = QueryPriceDistribution.Run(realEstateContext.Rentals);
            return Json(ranges, JsonRequestBehavior.AllowGet);
        }
    }

    public class QueryPriceDistribution
    {
        public static IEnumerable<BsonDocument> Run(IMongoCollection<Rental> mongoCollection)
        {
            var priceRange = new BsonDocument("$subtract",
                new BsonArray {
                    "$Price", new BsonDocument("$mod", new BsonArray{ "$Price", 500})
                });

            var grouping = new BsonDocument {
                    { "_id", priceRange },
                    { "count", new BsonDocument("$sum", 1)}
                };

            //var args = new AggregateArgs
            //{

            //}

            var priceRanges = mongoCollection.Aggregate().Group(grouping).ToList();
            return priceRanges;
        }
    }
}