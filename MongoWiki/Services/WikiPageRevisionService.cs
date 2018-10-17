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
            _collection.Indexes.CreateOne(
                new CreateIndexModel<WikiPageRevision>(
                    Builders<WikiPageRevision>.IndexKeys
                        .Ascending(model => model.PageId)
                        .Ascending(model => model.RevisionNumber),
                    new CreateIndexOptions()
                    {
                        Unique = true
                    }));
        }

        public void AddRevision(WikiPageRevision revision)
        {
            _collection.InsertOne(revision);
        }

        public List<WikiPageRevision> FindAllByPageId(ObjectId pageId)
        {
            return _collection.AsQueryable()
                .Where(model => model.PageId == pageId)
                .OrderByDescending(model => model.RevisionNumber)
                .ToList();
        }

        public WikiPageRevision FindMostRecentByPageId(ObjectId pageId)
        {
            return _collection.AsQueryable()
                .Where(model => model.PageId == pageId)
                .OrderByDescending(model => model.RevisionNumber)
                .FirstOrDefault();
        }

        public WikiPageRevision FindByRevisionNumber(ObjectId pageId, int revisionNumber)
        {
            return _collection.AsQueryable()
                .Where(model => model.PageId == pageId && model.RevisionNumber == revisionNumber)
                .SingleOrDefault();
        }
    }
}
