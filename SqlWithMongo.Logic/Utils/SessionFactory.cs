using System;
using System.Reflection;

using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;

using NHibernate;
using NHibernate.Event;


namespace SqlWithMongo.Logic.Utils
{
    public static class SessionFactory
    {
        private static ISessionFactory _factory;


        internal static ISession OpenSession()
        {
            return _factory.OpenSession(new Interceptor());
        }


        internal static void Init(string connectionString)
        {
            _factory = BuildSessionFactory(connectionString);
        }


        private static ISessionFactory BuildSessionFactory(string connectionString)
        {
            FluentConfiguration configuration = Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2012.ConnectionString(connectionString))
                .Mappings(m => m.FluentMappings.AddFromAssembly(Assembly.GetExecutingAssembly()))
                .ExposeConfiguration(x =>
                {
                    x.EventListeners.PostLoadEventListeners = new IPostLoadEventListener[]
                    {
                        new EventListener()
                    };
                });

            return configuration.BuildSessionFactory();
        }
    }
}
