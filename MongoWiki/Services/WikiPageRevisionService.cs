using MongoDB.Bson;
using MongoDB.Driver;
using MongoWiki.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoWiki.Services
{
    public class WikiPageRevisionService
    {
        private IMongoCollection<WikiPageRevision> _collection;

        public WikiPageRevisionService(MongoClientService mongoClientService)
        {
            _collection =
                mongoClientService.MongoClient
                    .GetDatabase("wiki")
                    .GetCollection<WikiPageRevision>("revisions");
        }

        public void AddRevision(WikiPageRevision revision)
        {
            _collection.InsertOne(revision);
        }

        public WikiPageRevision FindMostRecentByPageId(ObjectId pageId)
        {
            return _collection.AsQueryable()
                .Where(model => model.PageId == pageId)
                .OrderByDescending(model => model.Created)
                .First();
        }
    }
}
