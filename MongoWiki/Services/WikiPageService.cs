using MongoDB.Driver;
using MongoWiki.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoWiki.Services
{
    public class WikiPageService
    {
        private IMongoCollection<WikiPage> _collection;

        public WikiPageService(MongoClientService mongoClientService)
        {
            _collection =
                mongoClientService.MongoClient
                    .GetDatabase("wiki")
                    .GetCollection<WikiPage>("pages");
        }

        public WikiPage FindBySlug(string slug)
        {
            return _collection.AsQueryable()
                .Where(model => model.Slug == slug)
                .SingleOrDefault();
        }
    }
}
