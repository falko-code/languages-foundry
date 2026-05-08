using Falko.Foundry.Utf8Texts;

namespace Falko.Foundry.Caches;

public readonly struct CompilerCacheFingerprint(CompilerCache compilerCache, int cacheIndex)
{
    internal readonly CompilerCache? CompilerCache = compilerCache;

    internal readonly int CacheIndex = cacheIndex;

    public bool HasCache => CompilerCache is not null;

    public Utf8String GetString() => CompilerCache?.GetCachedData(CacheIndex) ?? Utf8String.Empty;
}
