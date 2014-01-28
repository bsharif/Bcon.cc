using System;
using System.Configuration;
using System.Data;
using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Data;
using Gecko.Bcon.Domain;
using Gecko.Bcon.Domain.Mappings;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using Configuration = NHibernate.Cfg.Configuration;

namespace Gecko.Bcon.DataAccess
{
    /// <summary>
    /// Connection manager gives access to the database session
    /// </summary>
    public class ConnectionManager
    {
        /// <summary>
        /// Singleton
        /// </summary>
        private ConnectionManager()
        {
            if (_sessionFactory == null)
            {
                if (_sessionFactory == null)
                    InitializeSessionFactory();
            }
        }

        private static ConnectionManager _instance;
        private ISessionFactory _sessionFactory;

        /// <summary>
        /// The actual instance of the object
        /// </summary>
        internal static ConnectionManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ConnectionManager();
                }
                return _instance;
            }
        }


        /// <summary>
        /// Prepares fluent nhibernate and Nhiberante
        /// </summary>
        private void InitializeSessionFactory()
        {
            _sessionFactory = Fluently.Configure()
                .Database(MySQLConfiguration.Standard
                              .ConnectionString(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString)
                              .ShowSql()
                )
                .Mappings(m => m.AutoMappings.Add(
                    AutoMap.AssemblyOf<AuthToken>(x => x.Namespace.EndsWith("Domain"))
                        .UseOverridesFromAssemblyOf<UserMappingOverride>()
                        .Conventions.Add(new Conventions()).IgnoreBase<Entity>)
                        )
                .ExposeConfiguration(BuildSchema)
                //.ExposeConfiguration(cfg=> new SchemaUpdate(cfg).Execute(false,true)) //Debugging
                .BuildSessionFactory();
        }

        /// <summary>
        /// Hook in case we need to rebuild the database schema, if set will update, or Drop and recreated scheme
        /// </summary>
        private static void BuildSchema(Configuration cfg)
        {
            string result = ConfigurationManager.AppSettings["Schema"];
            if (result == "DropCreate")
            {
                new SchemaExport(cfg).Drop(false, true);
                new SchemaExport(cfg).Create(false, true);
            }
            else if (result == "Update")
            {
                var update = new SchemaUpdate(cfg);
                update.Execute(true, true);
            }
        }

        /// <summary>
        /// Creates an instance of the nHiberante Session
        /// </summary>
        /// <returns></returns>
        public static ISession OpenSession()
        {
            return Instance.GetSession();
        }

        /// <summary>
        /// Gets a stateless session, useful if you need a little speed, and are not doing any 
        /// updates
        /// </summary>
        public static IStatelessSession OpenStatelessSession()
        {
            return Instance.GetStatelessSession();
        }

        private IStatelessSession GetStatelessSession()
        {
            return _sessionFactory.OpenStatelessSession();
        }

        /// <summary>
        /// Gets the actual instance session
        /// </summary>
        private ISession GetSession()
        {
            var session = _sessionFactory.OpenSession();
            session.BeginTransaction(IsolationLevel.ReadCommitted);
            return session;
        }
    }

   
    
}