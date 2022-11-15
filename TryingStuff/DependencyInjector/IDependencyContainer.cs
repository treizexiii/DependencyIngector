namespace TryingStuff.DependencyInjector;

public interface IDependencyContainer
{
    TInterface GetService<TInterface>();
}