using System;
using System.Collections.Generic;
using System.Linq;


namespace SqlWithMongo.Logic.SqlAndMongo
{
    public class MongoNode
    {
        public virtual int Id { get; protected set; }
        public virtual string Name { get; protected set; }

        protected virtual HashSet<NodeLink> LinksInternal { get; set; }
        public virtual IReadOnlyList<NodeLink> Links
        {
            get { return LinksInternal.ToList(); }
        }


        protected MongoNode()
        {
            LinksInternal = new HashSet<NodeLink>();
        }


        public MongoNode(string name)
            : this()
        {
            Name = name;
        }


        public virtual void AddLink(MongoNode mongoNode)
        {
            LinksInternal.Add(new NodeLink(mongoNode.Id, mongoNode.Name));
            mongoNode.LinksInternal.Add(new NodeLink(Id, Name));
        }
    }
}
