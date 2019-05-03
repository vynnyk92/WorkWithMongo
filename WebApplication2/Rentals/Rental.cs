using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Rentals
{
    public class Rental
    {
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        public string Description { get; set; }
        public int NumberOfRooms { get; set; }
        public List<string> Address { get; set; } = new List<string>();
        [BsonRepresentation(MongoDB.Bson.BsonType.Double)]
        public decimal Price { get; set; }
    }
}