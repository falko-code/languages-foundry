using System.Runtime.CompilerServices;
using Falko.Foundry.Caches;
using Falko.Foundry.Compilers;
using Falko.Foundry.Utf8Texts;

namespace Falko.Foundry.Elements;

public static class CacheElementExtensions
{
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void AppendFromCacheOrCompile<TCacheElement>
    (
        this ILanguageCompiler compiler,
        scoped ref Utf8Buffer buffer,
        scoped in TCacheElement element
    ) where TCacheElement : ICacheLanguageElement<TCacheElement>, allows ref struct
    {
        var cacheString = element.Cache.AsString();

        if (cacheString.IsEmpty)
        {
            compiler.GetElementCompiler<TCacheElement>().Compile
            (
                ref buffer,
                in element
            );
        }
        else
        {
            buffer.Allocate(cacheString.Length);
            buffer.Append(cacheString);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TCacheElement WithCache<TCacheElement>
    (
        this TCacheElement element,
        Utf8String cache
    ) where TCacheElement : ICacheLanguageElement<TCacheElement>, allows ref struct
    {
        return TCacheElement.CacheCopy(in element, new CompilerCache(cache));
    }
}
