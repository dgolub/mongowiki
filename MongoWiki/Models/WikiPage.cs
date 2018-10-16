using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoWiki.Models
{
    public class WikiPage
    {
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
    }
}
