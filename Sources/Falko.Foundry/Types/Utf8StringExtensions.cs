using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics;

namespace Falko.Foundry.Types;

public static class Utf8StringExtensions
{
    private const byte ContinuationMask = 0xC0;
    private const byte ContinuationPattern = 0x80;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int Count(this Utf8String value)
    {
        var span = value.AsSpan();

        if (Vector256.IsHardwareAccelerated && span.Length >= Vector256<byte>.Count) return CountVector256(span);
        if (Vector128.IsHardwareAccelerated && span.Length >= Vector128<byte>.Count) return CountVector128(span);
        return CountScalar(span);
    }

    [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.AggressiveOptimization)]
    private static int CountVector256(ReadOnlySpan<byte> span)
    {
        var mask = Vector256.Create(ContinuationMask);
        var pattern = Vector256.Create(ContinuationPattern);
        var acc = Vector256<byte>.Zero;
        var i = 0;

        for (; i <= span.Length - Vector256<byte>.Count; i += Vector256<byte>.Count)
        {
            var chunk = Vector256.LoadUnsafe(ref MemoryMarshal.GetReference(span), (nuint)i);
            acc -= Vector256.Equals(chunk & mask, pattern);
        }

        var count = span.Length - (int)Vector256.Sum(acc);

        ref var start = ref MemoryMarshal.GetReference(span);
        for (; i < span.Length; i++) if ((Unsafe.Add(ref start, i) & ContinuationMask) != ContinuationPattern) count++;

        return count;
    }

    [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.AggressiveOptimization)]
    private static int CountVector128(ReadOnlySpan<byte> span)
    {
        var mask = Vector128.Create(ContinuationMask);
        var pattern = Vector128.Create(ContinuationPattern);
        var acc = Vector128<byte>.Zero;
        var i = 0;

        for (; i <= span.Length - Vector128<byte>.Count; i += Vector128<byte>.Count)
        {
            var chunk = Vector128.LoadUnsafe(ref MemoryMarshal.GetReference(span), (nuint)i);
            acc -= Vector128.Equals(chunk & mask, pattern);
        }

        var count = span.Length - (int)Vector128.Sum(acc);

        ref var start = ref MemoryMarshal.GetReference(span);
        for (; i < span.Length; i++) if ((Unsafe.Add(ref start, i) & ContinuationMask) != ContinuationPattern) count++;

        return count;
    }

    [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.AggressiveOptimization)]
    private static int CountScalar(ReadOnlySpan<byte> span)
    {
        var count = 0;
        ref var start = ref MemoryMarshal.GetReference(span);
        for (var i = 0; i < span.Length; i++) if ((Unsafe.Add(ref start, i) & ContinuationMask) != ContinuationPattern) count++;
        return count;
    }
}
