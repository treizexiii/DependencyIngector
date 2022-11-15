namespace TryingStuff.DependencyInjector;

public interface IDependencyCollection
{
    IDependencyContainer GenerateContainer();
    void RegisterTransient<TImplementation>(params object[] parameters);
    void RegisterTransient<TInterface, TImplementation>(params object[] parameters);
    void RegisterSingleton<TImplementation>(params object[] parameters);
    void RegisterSingleton<TInterface, TImplementation>(params object[] parameters);
}