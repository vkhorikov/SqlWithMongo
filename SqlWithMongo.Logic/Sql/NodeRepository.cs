using System;
using System.Collections.Generic;
using System.Linq;

using SqlWithMongo.Logic.Utils;


namespace SqlWithMongo.Logic.Sql
{
    public class NodeRepository
    {
        private readonly UnitOfWork _unitOfWork;


        public NodeRepository(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public Node GetById(int id)
        {
            return _unitOfWork.Get<Node>(id);
        }


        public IList<Node> GetAll()
        {
            return _unitOfWork.Query<Node>()
                .ToList();
        }


        public void Save(Node mongoNode)
        {
            _unitOfWork.SaveOrUpdate(mongoNode);
        }
    }
}
