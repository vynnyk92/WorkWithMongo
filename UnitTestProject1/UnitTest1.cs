using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Bson;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var person = new BsonDocument()
            {
                { "contact", new BsonDocument()
                    {
                        {"phone", "12-a" },
                        {"phone2", "121-a" },
                    }
                }
            };

            person.Add("firstName", new BsonString("Lol"));
            person.Add("Age", new BsonInt32(12));
            person.Add("Age2", new BsonDateTime(DateTime.Now));
            person.Add("Hooby", new BsonArray((new string[] { "Football", "Music" })));



            Console.WriteLine(person);
        }
    }
}
