using System;

using FluentNHibernate.Mapping;


namespace SqlWithMongo.Logic.SqlAndMongo
{
    public class MongoNodeMap : ClassMap<MongoNode>
    {
        public MongoNodeMap()
        {
            Id(x => x.Id, "MongoNodeId").GeneratedBy.HiLo("[dbo].[Ids]", "NextHigh", "10", "EntityName = 'MongoNode'");
            Map(x => x.Name).Not.Nullable();
        }
    }
}
