namespace TryingStuff.DependencyInjector;

public static class DependencyServiceBuilder
{
    public static IDependencyCollection Build()
    {
        var collection = new DependencyCollection();
        return collection;
    }
}