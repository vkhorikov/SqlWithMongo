using System;


namespace SqlWithMongo.Logic.SqlAndMongo
{
    public class NodeLink
    {
        public int Id { get; private set; }
        public string Name { get; private set; }


        public NodeLink(int id, string name)
        {
            Id = id;
            Name = name;
        }


        public override bool Equals(object obj)
        {
            var nodeLink = obj as NodeLink;

            if (ReferenceEquals(nodeLink, null))
                return false;

            return nodeLink.Id == Id;
        }


        public override int GetHashCode()
        {
            return Id;
        }
    }
}
