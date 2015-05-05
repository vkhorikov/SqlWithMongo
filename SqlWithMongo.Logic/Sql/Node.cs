using System;
using System.Collections.Generic;
using System.Linq;


namespace SqlWithMongo.Logic.Sql
{
    public class Node
    {
        public virtual int Id { get; protected set; }
        public virtual string Name { get; protected set; }

        protected virtual ISet<Node> LinksInternal { get; set; }
        public virtual IReadOnlyList<Node> Links
        {
            get { return LinksInternal.ToList(); }
        }


        protected Node()
        {
            LinksInternal = new HashSet<Node>();
        }


        public Node(string name)
            : this()
        {
            Name = name;
        }


        public virtual void AddLink(Node node)
        {
            LinksInternal.Add(node);
            node.LinksInternal.Add(this);
        }
    }
}
