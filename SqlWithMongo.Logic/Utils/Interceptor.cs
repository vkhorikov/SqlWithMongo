using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using NHibernate;

using SqlWithMongo.Logic.SqlAndMongo;


namespace SqlWithMongo.Logic.Utils
{
    internal class Interceptor : EmptyInterceptor
    {
        public override void PostFlush(ICollection entities)
        {
            IEnumerable<MongoNode> nodes = entities.OfType<MongoNode>();

            if (!nodes.Any())
                return;

            var repository = new NodeLinkRepository();
            Task[] tasks = nodes.Select(x => repository.SaveLinks(x.Id, x.Links)).ToArray();
            Task.WaitAll(tasks);
        }
    }
}
