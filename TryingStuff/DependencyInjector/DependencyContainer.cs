using System.Reflection;

namespace TryingStuff.DependencyInjector;

internal class DependencyContainer : IDependencyContainer
{
    private readonly IEnumerable<DependencyDescriptor> _descriptors;

    public DependencyContainer(IEnumerable<DependencyDescriptor> descriptors)
    {
        _descriptors = descriptors;
    }

    public TInterface GetService<TInterface>()
    {
        var type = typeof(TInterface);
        var dependencyDescriptor = _descriptors.FirstOrDefault(x => x.Interface == type);
        if (dependencyDescriptor == null)
            throw new Exception("Could not find dependency of type " + type.FullName);

        if (dependencyDescriptor.LifeTime == LifeTime.singleton)
        {
            if (dependencyDescriptor.Instance == null)
            {
                dependencyDescriptor.Instance =
                    BuildInstance<TInterface>(dependencyDescriptor.Implementation, dependencyDescriptor.Parameters);
            }

            return (TInterface)dependencyDescriptor.Instance!;
        }

        return BuildInstance<TInterface>(dependencyDescriptor.Implementation, dependencyDescriptor.Parameters);
    }

    private TInterface BuildInstance<TInterface>(Type implementation, object[] parameters)
    {
        object?[] parameterInstances = parameters;
        if (parameterInstances.Length == 0)
        {
            var constructor = FindConstructor(implementation);
            if (constructor == null)
                throw new Exception("Could not find a constructor for type " + implementation);
            parameterInstances = ParameterInstances(constructor);
        }

        var instance = (TInterface)Activator.CreateInstance(implementation, parameterInstances)!;
        if (instance == null)
            throw new Exception("Could not create instance of type " + implementation);
        return instance;
    }

    private ConstructorInfo? FindConstructor(Type implementation)
    {
        var constructors = implementation.GetConstructors(BindingFlags.Public | BindingFlags.Instance);
        var constructor = constructors.FirstOrDefault(c =>
        {
            var parameters = c.GetParameters();
            foreach (var param in parameters)
            {
                if (!param.ParameterType.IsInterface &&
                    _descriptors.Any(d => d.Interface == param.ParameterType) == false)
                    return false;
            }

            return true;
        });
        return constructor;
    }

    private object?[] ParameterInstances(ConstructorInfo constructor)
    {
        var parameters = constructor.GetParameters();
        var parameterInstances = new object?[parameters.Length];
        if (parameters.Length > 0)
        {
            for (var i = 0; i < parameters.Length; i++)
            {
                var parameter = parameters[i];
                var parameterType = parameter.ParameterType;
                if (parameterType.IsInterface)
                {
                    var dependency = _descriptors.FirstOrDefault(d => d.Interface == parameterType);
                    if (dependency == null)
                        throw new Exception("Could not find dependency for type " + parameterType.FullName);
                    parameterInstances[i] = dependency.Instance;
                }
            }
        }

        return parameterInstances;
    }
}