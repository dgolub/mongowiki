using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoWiki.Services
{
    public class MongoClientService
    {
        public MongoClient MongoClient { get; private set; }

        public MongoClientService()
        {
            MongoClient = new MongoClient("mongodb://localhost:27017");
        }
    }
}
