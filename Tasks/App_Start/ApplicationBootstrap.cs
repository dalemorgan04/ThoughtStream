using System.Configuration;
using System.Web.Mvc;
using Castle.Windsor;
using Castle.Windsor.Installer;
using Tasks.NHibernate;

namespace Tasks
{
    public class ApplicationBootstrap 
    {
        private static WindsorContainer container;
        
        public static void Start()
        {
            //We want a service resolver. Set the container as a windsor container
            //Create a session            

            string connectionString = ConfigurationManager.ConnectionStrings["Local"].ConnectionString;

            InitialiseWindsor(connectionString);
            MappingConfig.RegisterMappings();
            NHSessionFactory.Initialise();
        }        

        public static void End()
        {
            container.Dispose();
        }

        private static void InitialiseWindsor(string connectionString)
        {
            container = new WindsorContainer();            
            container.Install(
                new DependencyInstaller(connectionString));

            ControllerBuilder.Current.SetControllerFactory(new WindsorControllerFactory(container.Kernel));
        }


    }
}