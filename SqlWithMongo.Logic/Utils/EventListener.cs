using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

using NHibernate.Event;

using SqlWithMongo.Logic.SqlAndMongo;


namespace SqlWithMongo.Logic.Utils
{
    internal class EventListener : IPostLoadEventListener
    {
        public void OnPostLoad(PostLoadEvent ev)
        {
            var networkNode = ev.Entity as MongoNode;

            if (networkNode == null)
                return;

            var repository = new NodeLinkRepository();
            IList<NodeLink> linksFromMongo = repository.GetLinks(networkNode.Id);

            HashSet<NodeLink> links = (HashSet<NodeLink>)networkNode
                .GetType()
                .GetProperty("LinksInternal", BindingFlags.NonPublic | BindingFlags.Instance)
                .GetValue(networkNode);
            links.UnionWith(linksFromMongo);
        }
    }
}
