using MongoDB.Bson;
using NUnit.Framework;
using System;

namespace Tests
{
    public class BsonDocumentTests
    {
        [Test]
        public void TestMethod()
        {
            var doc = new BsonDocument();

            Console.WriteLine(doc.ToJson());
            Assert.IsFalse(false);
        }
    }
}
