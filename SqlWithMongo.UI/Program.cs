using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

using MongoDB.Driver;

using SqlWithMongo.Logic.Sql;
using SqlWithMongo.Logic.SqlAndMongo;
using SqlWithMongo.Logic.Utils;


namespace SqlWithMongo.UI
{
    internal class Program
    {
        private const string SqlServerConnectionString = @"Server=VLADIMIR-PC\SQL2012;Database=SqlWithMongo;Trusted_Connection=true;";
        private const string MongoConnectionString = "mongodb://localhost:27017";
        private const int NumberOfNodes = 1000;


        private static void Main(string[] args)
        {
            Console.WriteLine("Clearing database...");
            ClearDatabases();
            Initer.Init(SqlServerConnectionString, MongoConnectionString);
            Console.WriteLine("Completed");

            Console.WriteLine("Creating nodes...");
            CreateNodes();
            Console.WriteLine("Completed");

            Console.WriteLine("Linking nodes...");
            long milliseconds1 = LinkSqlNodes();
            Console.WriteLine("SQL : " + milliseconds1);
            long milliseconds2 = LinkMongoNodes();
            Console.WriteLine("Mongo : " + milliseconds2);
            Console.WriteLine("Completed");

            Console.WriteLine("Fetching nodes...");
            long milliseconds3 = FetchSqlNodes();
            Console.WriteLine("SQL : " + milliseconds3);
            long milliseconds4 = FetchMongoNodes();
            Console.WriteLine("Mongo : " + milliseconds4);
            Console.WriteLine("Completed");

            Console.ReadKey();
        }


        private static long FetchMongoNodes()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            for (int i = 0; i < NumberOfNodes; i++)
            {
                using (var unitOfWork = new UnitOfWork())
                {
                    var repository = new MongoNodeRepository(unitOfWork);

                    MongoNode node = repository.GetById(i + 1);
                    IReadOnlyList<NodeLink> links = node.Links;
                }
            }

            stopwatch.Stop();
            return stopwatch.ElapsedMilliseconds;
        }


        private static long FetchSqlNodes()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            for (int i = 0; i < NumberOfNodes; i++)
            {
                using (var unitOfWork = new UnitOfWork())
                {
                    var repository = new NodeRepository(unitOfWork);

                    Node node = repository.GetById(i + 1);
                    IReadOnlyList<Node> links = node.Links;
                }
            }

            stopwatch.Stop();
            return stopwatch.ElapsedMilliseconds;
        }


        private static long LinkSqlNodes()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            using (var unitOfWork = new UnitOfWork())
            {
                var repository = new NodeRepository(unitOfWork);

                IList<Node> nodes = repository.GetAll();
                foreach (Node node1 in nodes)
                {
                    foreach (Node node2 in nodes)
                    {
                        node1.AddLink(node2);
                    }
                }

                unitOfWork.Commit();
            }

            stopwatch.Stop();
            return stopwatch.ElapsedMilliseconds;
        }


        private static long LinkMongoNodes()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            using (var unitOfWork = new UnitOfWork())
            {
                var repository = new MongoNodeRepository(unitOfWork);

                IList<MongoNode> nodes = repository.GetAll();
                foreach (MongoNode node1 in nodes)
                {
                    foreach (MongoNode node2 in nodes)
                    {
                        node1.AddLink(node2);
                    }
                }

                unitOfWork.Commit();
            }

            stopwatch.Stop();
            return stopwatch.ElapsedMilliseconds;
        }


        private static void CreateNodes()
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var repository = new NodeRepository(unitOfWork);

                for (int i = 0; i < NumberOfNodes; i++)
                {
                    var node = new Node("Node " + (i + 1));
                    repository.Save(node);
                }

                unitOfWork.Commit();
            }

            using (var unitOfWork = new UnitOfWork())
            {
                var repository = new MongoNodeRepository(unitOfWork);

                for (int i = 0; i < NumberOfNodes; i++)
                {
                    var node = new MongoNode("Node " + (i + 1));
                    repository.Save(node);
                }

                unitOfWork.Commit();
            }
        }


        private static void ClearDatabases()
        {
            new MongoClient(MongoConnectionString)
                .GetDatabase("sqlWithMongo")
                .DropCollectionAsync("links")
                .Wait();

            string query = "DELETE FROM [dbo].[MongoNode];" +
                "DELETE FROM [dbo].[Node_Node];" +
                "DELETE FROM [dbo].[Node];" +
                "UPDATE [dbo].[Ids] SET [NextHigh] = 0";

            using (var connection = new SqlConnection(SqlServerConnectionString))
            {
                var command = new SqlCommand(query, connection)
                {
                    CommandType = CommandType.Text
                };

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
