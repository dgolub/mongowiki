using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoWiki.Models
{
    public class WikiPageRevision
    {
        public ObjectId Id { get; set; }

        [BsonElement("pageId")]
        public ObjectId PageId { get; set; }

        [BsonElement("revisionNumber")]
        public int RevisionNumber { get; set; }

        [BsonElement("created")]
        public DateTime Created { get; set; }

        [BsonElement("content")]
        public string Content { get; set; }

        [BsonElement("comment")]
        public string Comment { get; set; }
    }
}
