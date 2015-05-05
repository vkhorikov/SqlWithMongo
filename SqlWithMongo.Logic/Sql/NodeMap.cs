using System;

using FluentNHibernate;
using FluentNHibernate.Mapping;


namespace SqlWithMongo.Logic.Sql
{
    public class NodeMap : ClassMap<Node>
    {
        public NodeMap()
        {
            Id(x => x.Id, "NodeId").GeneratedBy.HiLo("[dbo].[Ids]", "NextHigh", "10", "EntityName = 'Node'");
            Map(x => x.Name).Not.Nullable();

            HasManyToMany<Node>(Reveal.Member<Node>("LinksInternal"))
                .AsSet()
                .Table("Node_Node")
                .ParentKeyColumn("NodeId1")
                .ChildKeyColumn("NodeId2");
        }
    }
}
