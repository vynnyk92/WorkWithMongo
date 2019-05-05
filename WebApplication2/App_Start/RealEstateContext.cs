using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication2.Properties;
using WebApplication2.Rentals;

namespace WebApplication2.App_Start
{
    public class RealEstateContext
    {
        public IMongoDatabase MongoDatabase;
        public RealEstateContext()
        {
            var client = new MongoClient(Settings.Default.RealEstateConnectionString);
            MongoDatabase = client.GetDatabase(Settings.Default.RealEstateDatabaseName);
        }

        public IMongoCollection<Rental> Rentals
        {
            get { return MongoDatabase.GetCollection<Rental>("rentals"); }
        }
    }
}