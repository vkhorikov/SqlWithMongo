using System;
using System.Collections.Generic;
using System.Linq;

using SqlWithMongo.Logic.Utils;


namespace SqlWithMongo.Logic.SqlAndMongo
{
    public class MongoNodeRepository
    {
        private readonly UnitOfWork _unitOfWork;


        public MongoNodeRepository(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public MongoNode GetById(int id)
        {
            return _unitOfWork.Get<MongoNode>(id);
        }


        public void Save(MongoNode mongoNode)
        {
            _unitOfWork.SaveOrUpdate(mongoNode);
        }


        public IList<MongoNode> GetAll()
        {
            return _unitOfWork.Query<MongoNode>()
                .ToList();
        }
    }
}
