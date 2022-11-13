namespace TryingStuff.Memory;

internal class CachedObject
{
    public CachedObject(string reference, object? o)
    {
        Reference = reference;
        Obj = o;
    }

    public string Reference { get; set; }
    public object? Obj { get; set; }
}

internal class MemoryCache : IMemoryCache
{
    private CachedObject[]? _cache;
    private int _memorySize;

    public MemoryCache()
    {
        _cache = null;
        _memorySize = 0;
    }

    public void PushObject(string reference, object? o)
    {
        if (_cache == null)
        {
            _cache = new[] { new CachedObject(reference, o) };
            _memorySize++;
        }
        else
        {
            var newCache = new CachedObject[_memorySize + 1];
            newCache[0] = new CachedObject(reference, o);
            _cache.CopyTo(newCache, 1);
            RewriteCache(newCache);
        }
    }

    public object? GetObject<T>(string reference)
    {
        for (int i = 0; i < _memorySize; i++)
        {
            if (_cache[i].Reference == reference)
            {
                return (T)_cache[i].Obj!;
            }
        }

        return null;
    }

    public void RemoveFromCache(string reference)
    {
        if (_memorySize == 0)
        {
            throw new OutOfMemoryException("Cache is empty");
        }
        var newCache = new CachedObject[_memorySize - 1];
        for (int i = 0; i < _memorySize; i++)
        {
            if (_cache[i].Reference != reference)
            {
                newCache[i] = _cache[i];
            }
        }

        RewriteCache(newCache);
    }

    public void PurgeMemory()
    {
        _cache = null;
        _memorySize = 0;
    }

    private void RewriteCache(CachedObject[] newCache)
    {
        _cache = newCache;
        _memorySize = _cache.Length;
    }
}
