namespace TryingStuff.Memory;

public interface IMemoryCache
{
    void PushObject(string reference, object? o);
    object? GetObject<T>(string reference);
    void RemoveFromCache(string reference);
    void PurgeMemory();
}