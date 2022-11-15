namespace TryingStuff.DependencyInjector;

internal class DependencyCollection : IDependencyCollection
{
    private readonly List<DependencyDescriptor> _dependencies;

    public DependencyCollection()
    {
        _dependencies = new List<DependencyDescriptor>();
    }

    public IDependencyContainer GenerateContainer()
    {
        var container = new DependencyContainer(_dependencies);
        return container;
    }
    
    public void RegisterTransient<TImplementation>(params object[] parameters)
    {
        RegisterService(typeof(TImplementation), typeof(TImplementation), LifeTime.transient, parameters);
    }
    
    public void RegisterTransient<TInterface, TImplementation>(params object[] parameters)
    {
        var service = RegistrationControls<TInterface, TImplementation>();
        RegisterService(service.Item1, service.Item2, LifeTime.transient, parameters);
    }
    
    public void RegisterSingleton<TImplementation>(params object[] parameters)
    {
        RegisterService(typeof(TImplementation), typeof(TImplementation), LifeTime.singleton, parameters);
    }

    public void RegisterSingleton<TInterface, TImplementation>(params object[] parameters)
    {
        var service = RegistrationControls<TInterface, TImplementation>();
        RegisterService(service.Item1, service.Item2, LifeTime.singleton, parameters);
    }

    private void RegisterService(Type serviceType, Type serviceImplementation, LifeTime lifeTime, object[] parameters)
    {
        var dependencyDescriptor = new DependencyDescriptor(
            serviceType,
            serviceImplementation,
            null,
            lifeTime,
            parameters);
        _dependencies.Add(dependencyDescriptor);
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