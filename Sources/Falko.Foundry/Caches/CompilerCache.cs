using Falko.Foundry.Elements;
using Falko.Foundry.Utf8Texts;

namespace Falko.Foundry.Caches;

public sealed class CompilerCache
{
    private Utf8String[] _cache = new Utf8String[16];

    private int _lastIndex;

    public TCacheElement Cache<TCacheElement>(scoped in TCacheElement element, Utf8String data)
        where TCacheElement : ICacheElement<TCacheElement>, allows ref struct
    {
        var nextIndex = ++_lastIndex;

        ResizeIfNeeded();

        _cache[nextIndex] = data;

        return TCacheElement.Fingerprint(in element, new CompilerCacheFingerprint(this, nextIndex));
    }

    public Utf8String GetCachedData(int cacheIndex)
    {
        return _cache[cacheIndex];
    }

    private void ResizeIfNeeded()
    {
        if (_lastIndex + 1 >= _cache.Length)
        {
            var newCache = new Utf8String[_cache.Length * 2];

            Array.Copy(_cache, newCache, _cache.Length);

            _cache = newCache;
        }
    }
}
