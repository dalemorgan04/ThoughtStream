using System.Web.Mvc;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Tasks.NHibernate;
using Tasks.Repository.Thoughts;
using Tasks.Service.Tasks;
using Tasks.Service.Thoughts;
using Tasks.Service.Users;
using Tasks.Repository.Habits;
using Tasks.Service.Habits;
using Tasks.Service.Projects;
using Tasks.Repository.Core;
using Tasks.Repository.Projects;
using Tasks.Service.PlanWeek;

namespace Tasks
{
    public class DependencyInstaller: IWindsorInstaller
    {
        private readonly string connectionString;

        public DependencyInstaller(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            //Domain

            //Services
            container.Register(Component.For<IThoughtService>().ImplementedBy<ThoughtService>().LifeStyle.Transient);
            container.Register(Component.For<ITaskService>().ImplementedBy<TaskService>().LifeStyle.Transient);
            container.Register(Component.For<IHabitService>().ImplementedBy<HabitService>().LifeStyle.Transient);
            container.Register(Component.For<IProjectService>().ImplementedBy<ProjectService>().LifeStyle.Transient);
            container.Register(Component.For<IPlanWeekService>().ImplementedBy<PlanWeekService>().LifeStyle.Transient);
            container.Register(Component.For<IUserService>().ImplementedBy<UserService>().LifeStyle.Transient);

            //SQL 
            var dependencies = new {connectionString = this.connectionString };

            container.Register(Component
                .For<IThoughtRepository>()
                .ImplementedBy<ThoughtRepository>()
                .LifeStyle.Transient
                .DependsOn(dependencies));

            container.Register(Component
                .For<IHabitRepository>()
                .ImplementedBy<HabitRepository>()
                .LifeStyle.Transient
                .DependsOn(dependencies));

            container.Register(Component
                .For<IProjectRepository>()
                .ImplementedBy<ProjectRepository>()
                .LifeStyle.Transient
                .DependsOn(dependencies));

            //Interceptor
            //TODO 

            //Session Factory   
            container.Register(Component.For<IUnitOfWork, INHUnitOfWork, NHUnitOfWork>().ImplementedBy<NHUnitOfWork>().LifeStyle.PerWebRequest);         

            //Repositories
            container.Register(Component.For(typeof(ISpecificationRepository<,>)).ImplementedBy(typeof(NHRepository<,>)).LifeStyle.Transient);

            //MVC Controllers
            container.Register(Classes.FromThisAssembly().BasedOn<IController>().LifestyleTransient());
        }
    }
}