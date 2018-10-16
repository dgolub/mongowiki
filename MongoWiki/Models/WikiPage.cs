using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoWiki.Models
{
    public class WikiPage
    {
        public ObjectId Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }
        
        [BsonElement("slug")]
        public string Slug { get; set; }
    }
}
