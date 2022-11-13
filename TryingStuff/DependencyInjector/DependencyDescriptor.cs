namespace TryingStuff.DependencyInjector;

public class DependencyDescriptor
{
    public Type Interface { get; }
    public Type Implementation { get; }
    public object? Instance { get; set; }
    public LifeTime LifeTime { get; }

    public DependencyDescriptor(Type @interface, Type implementation, object? instance, LifeTime lifeTime)
    {
        Instance = instance;
        Interface = @interface;
        LifeTime = lifeTime;
        Implementation = implementation;
    }
}

public enum LifeTime
{
    singleton,
    transient
}