using System.Runtime.CompilerServices;
using Falko.Foundry.Utf8Texts;

namespace Falko.Foundry.Caches;

[method: MethodImpl(MethodImplOptions.AggressiveInlining)]
public readonly struct CompilerCache(Utf8String cacheString)
{
    public bool IsEmpty
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => cacheString.IsEmpty;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Utf8String AsString() => cacheString;
}
