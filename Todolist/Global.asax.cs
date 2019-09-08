using Autofac;
using Autofac.Integration.Mvc;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Todolist.ContextDb;
using Todolist.Repositories;
using Todolist.Services;

/*
 * 
 * @author Vladislav Kozlov <k2v.akosa@gmail.com>
*/
namespace Todolist
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<TodolistDbContext>().As<ITodolistDbContext>();
            builder.RegisterType<TaskRepository>().As<ITaskRepository>();
            builder.RegisterType<TaskService>().As<ITaskService>();
            //builder.RegisterModelBinders(typeof(MvcApplication).Assembly);         
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
