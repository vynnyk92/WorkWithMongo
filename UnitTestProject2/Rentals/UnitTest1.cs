using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Bson;
using WebApplication2.Rentals;

namespace UnitTestProject2.Rentals
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void ToDocumentRentalWithPrice()
        {
            var rental = new Rental();
            rental.Price = 1;
            rental.Id = ObjectId.GenerateNewId().ToString();

            var doc = rental.ToBsonDocument();
            Assert.IsTrue(doc["Price"].IsDouble);
            Assert.IsTrue(doc["_id"].IsObjectId);
        }
    }
}
