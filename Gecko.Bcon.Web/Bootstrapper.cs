using System.Web.Mvc;
using Gecko.Bcon.DataAccess;
using Microsoft.Practices.Unity;
using NHibernate;
using Unity.Mvc4;

namespace Gecko.Bcon.Web
{
  public static class Bootstrapper
  {
    public static IUnityContainer Initialise()
    {
      var container = BuildUnityContainer();

      DependencyResolver.SetResolver(new UnityDependencyResolver(container));

      return container;
    }

    private static IUnityContainer BuildUnityContainer()
    {
      var container = new UnityContainer();

      // register all your components with the container here
      // it is NOT necessary to register your controllers
      container.RegisterType<ISession>(new InjectionFactory(c => ConnectionManager.OpenSession()));
      // e.g. container.RegisterType<ITestService, TestService>();    
      RegisterTypes(container);

      return container;
    }

    public static void RegisterTypes(IUnityContainer container)
    {
    
    }
  }
}