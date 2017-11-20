using System.Configuration;
using System.Reflection;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using Tasks.Repository;
using Tasks.Repository.Thoughts;

namespace Tasks.NHibernate
{
    public class NHSessionFactory
    {

        private static FluentConfiguration fluentConfig = null;
        private static ISessionFactory sessionFactory = null;

        public static ISessionFactory SessionFactory
        {
            get            
            {
                if (sessionFactory == null)
                {
                    sessionFactory = buildSessionFactory();
                }
                return sessionFactory;
            }
        }

        public static void Initialise()
        {
            //This should only be run once for the app and is found in app start
            //This is known as singleton - only one instance running at any one time
            if (sessionFactory == null)
            {
                sessionFactory = buildSessionFactory();
            }
        }

        public static void Dispose()
        {
            sessionFactory.Dispose();
            sessionFactory = null;
        }

        private static void buildConfig()
        {
            //string conStr = "Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename = \"D:\\Users\\Dale.Morgan\\Documents\\Visual Studio 2017\\Projects\\Tasks\\Tasks\\App_Data\\Tasks.mdf\"; Integrated Security = True";
            string connectionString = ConfigurationManager.ConnectionStrings["Local"].ConnectionString;

            fluentConfig = Fluently.Configure().Database(MsSqlConfiguration.MsSql2005.ConnectionString(connectionString))
                            .Mappings(m => m.FluentMappings                                                
                                                .AddFromAssembly(Assembly.GetAssembly(typeof(ThoughtMap)))
                                                .AddFromAssembly(Assembly.GetAssembly(typeof(TaskMap)))                                                
                                                .AddFromAssembly(Assembly.GetAssembly(typeof(UserMap)))
                                                .AddFromAssembly(Assembly.GetAssembly(typeof(PriorityMap)))
                            );
        }
        private static ISessionFactory buildSessionFactory()
        {
            buildConfig();
            return fluentConfig.BuildSessionFactory();
        }        

    }
}