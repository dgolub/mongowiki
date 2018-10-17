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

        public async Task AddRevision(WikiPageRevision revision)
        {
            await _collection.InsertOneAsync(revision);
        }

        public async Task<List<WikiPageRevision>> FindAllByPageId(ObjectId pageId)
        {
            return await _collection.AsQueryable()
                .Where(model => model.PageId == pageId)
                .OrderByDescending(model => model.RevisionNumber)
                .ToAsyncEnumerable()
                .ToList();
        }

        public async Task<WikiPageRevision> FindMostRecentByPageId(ObjectId pageId)
        {
            return await _collection.AsQueryable()
                .Where(model => model.PageId == pageId)
                .OrderByDescending(model => model.RevisionNumber)
                .ToAsyncEnumerable()
                .FirstOrDefault();
        }

        public async Task<WikiPageRevision> FindByRevisionNumber(ObjectId pageId, int revisionNumber)
        {
            return await _collection.AsQueryable()
                .Where(model => model.PageId == pageId && model.RevisionNumber == revisionNumber)
                .ToAsyncEnumerable()
                .SingleOrDefault();
        }
    }
}
