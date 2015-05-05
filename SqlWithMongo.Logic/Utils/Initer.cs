using System;

using SqlWithMongo.Logic.SqlAndMongo;


namespace SqlWithMongo.Logic.Utils
{
    public static class Initer
    {
        public static void Init(string sqlServerConnectionString, string mongoConnectionString)
        {
            SessionFactory.Init(sqlServerConnectionString);
            NodeLinkRepository.Init(mongoConnectionString);
        }
    }
}
