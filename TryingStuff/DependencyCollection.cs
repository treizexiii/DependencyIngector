using System.Reflection;

namespace TryingStuff;

public class DependencyCollection
{
    private readonly List<DependencyDescriptor> _dependencies = new();

    public void RegisterTransient<TInterface, TImplementation>()
    {
        var service = RegistrationControls<TInterface, TImplementation>();

        var dependencyDescriptor = new DependencyDescriptor(
            service.Item1,
            service.Item2, 
            null,
            LifeTime.transient);
        _dependencies.Add(dependencyDescriptor);
    }

    public void RegisterSingleton<TInterface, TImplementation>()
    {
        var service = RegistrationControls<TInterface, TImplementation>();
        
        var dependencyDescriptor = new DependencyDescriptor(
            service.Item1, 
            service.Item2, 
            null, 
            LifeTime.singleton);
        _dependencies.Add(dependencyDescriptor);
    }
    
    public DependencyContainer GenerateContainer()
    {
        var container = new DependencyContainer(_dependencies);
        return container;
    }
    
    private (Type, Type) RegistrationControls<TInterface, TImplementation>()
    {
        var interfaceType = typeof(TInterface);
        var implementationType = typeof(TImplementation);
        if (!implementationType.GetInterfaces().Contains(interfaceType))
            throw new Exception("Type " + typeof(TImplementation).FullName + " does not implement " +
                                interfaceType.FullName);
        if (implementationType.IsAbstract)
            throw new Exception("Type " + typeof(TImplementation).FullName + " is abstract");
        if (_dependencies.Any(x => x.Interface == interfaceType))
        {
            throw new Exception("Service type " + interfaceType.FullName + "already registered");
        }

        return (interfaceType, implementationType);
    }
}