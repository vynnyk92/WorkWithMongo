using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication2.Properties;

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
    }
}