using System.Reflection;
using Autofac;

using Module = Autofac.Module;
using AreaServer.Core.Models;
using AreaServer.Infrastructure.Data.Repository;

public class DefaultInfrastructureModule : Module
{
  private readonly List<Assembly> _assemblies = new List<Assembly>();

  public DefaultInfrastructureModule(Assembly? callingAssembly = null)
  {
    var coreAssembly = Assembly.GetAssembly(typeof(Services)); 
    var infrastructureAssembly = Assembly.GetAssembly(typeof(Services));
    
    if (coreAssembly != null)
    {
      _assemblies.Add(coreAssembly);
    }

    if (infrastructureAssembly != null)
    {
      _assemblies.Add(infrastructureAssembly);
    }

    if (callingAssembly != null)
    {
      _assemblies.Add(callingAssembly);
    }
  }

  protected override void Load(ContainerBuilder builder)
  {
        builder.RegisterGeneric(typeof(EfRepository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();
  }

}
