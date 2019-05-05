using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication2.Models;
using MongoDB.Bson;
using MongoDB.Driver;

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

        public List<PriceAdjusment> PriceAdjusments { get; set; } = new List<PriceAdjusment>();

        public void AdjustPrice(AdjustPrice adjustPrice)
        {
            this.PriceAdjusments.Add(new PriceAdjusment(adjustPrice, Price));
            Price = adjustPrice.NewPrice;
        }

        public UpdateDefinition<Rental> AdjustPriceUsingModif(AdjustPrice adjustPrice)
        {
            var adj = new PriceAdjusment(adjustPrice, Price);
            var modUpd = new UpdateDefinitionBuilder<Rental>().Push(r => r.PriceAdjusments, adj)
                .Set(r => r.Price, adj.NewPrice);

            return modUpd;
        }

        
    }
}