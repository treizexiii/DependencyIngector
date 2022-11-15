namespace TryingStuff.DependencyInjector;

public class DependencyDescriptor
{
    public Type Interface { get; }
    public Type Implementation { get; }
    public object? Instance { get; set; }
    public object[] Parameters { get; }

    public LifeTime LifeTime { get; }

    public DependencyDescriptor(Type @interface, 
        Type implementation, 
        object? instance, 
        LifeTime lifeTime,
        params object[] parameters)
    {
        Instance = instance;
        Interface = @interface;
        LifeTime = lifeTime;
        Parameters = parameters;
        Implementation = implementation;
    }
}

public enum LifeTime
{
    singleton,
    transient
}