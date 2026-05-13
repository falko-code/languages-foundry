using System.Buffers;
using System.Runtime.CompilerServices;

namespace Falko.Foundry.Utf8Texts;

public ref partial struct Utf8Buffer : IDisposable
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly void Dispose()
    {
        var rented = _rented;

        if (rented is null) return; // we used stack-allocated buffer

        ArrayPool<byte>.Shared.Return(rented);
    }
}
