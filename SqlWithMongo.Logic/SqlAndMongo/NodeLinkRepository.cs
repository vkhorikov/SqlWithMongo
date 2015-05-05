using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using MongoDB.Driver;


namespace SqlWithMongo.Logic.SqlAndMongo
{
    internal class NodeLinkRepository
    {
        private static IMongoCollection<NodeLinks> _collection;


        public IList<NodeLink> GetLinks(int nodeId)
        {
            NodeLinks links = _collection.Find(x => x.Id == nodeId).SingleOrDefaultAsync().Result;
            
            if (links == null)
                return new NodeLink[0];

            return links.Links;
        }


        public Task SaveLinks(int nodeId, IEnumerable<NodeLink> links)
        {
            var nodeLinks = new NodeLinks(nodeId, links);
            var updateOptions = new UpdateOptions
            {
                IsUpsert = true
            };

            return _collection.ReplaceOneAsync(x => x.Id == nodeId, nodeLinks, updateOptions);
        }


        internal static void Init(string connectionString)
        {
            var client = new MongoClient(connectionString);
            IMongoDatabase database = client.GetDatabase("sqlWithMongo");
            var collectionSettings = new MongoCollectionSettings
            {
                WriteConcern = new WriteConcern(1)
            };
            _collection = database.GetCollection<NodeLinks>("links", collectionSettings);
        }


        private class NodeLinks
        {
            public int Id { get; private set; }
            public List<NodeLink> Links { get; private set; }


            public NodeLinks(int nodeId, IEnumerable<NodeLink> links)
            {
                Id = nodeId;
                Links = new List<NodeLink>();
                Links.AddRange(links);
            }
        }
    }
}
