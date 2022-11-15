namespace TryingStuff.Memory;

public interface IMemoryCache
{
    void PushObject(string reference, object? o);
    T? GetObject<T>(string reference);
    void RemoveFromCache(string reference);
    void PurgeMemory();
}