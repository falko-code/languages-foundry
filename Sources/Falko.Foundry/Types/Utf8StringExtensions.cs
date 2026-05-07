using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics;

namespace Falko.Foundry.Types;

public static class Utf8StringExtensions
{
    private const byte ContinuationMask = 0xC0;
    private const byte ContinuationPattern = 0x80;

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
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

        vectorLoop:
        {
            var chunk = Vector256.LoadUnsafe(ref MemoryMarshal.GetReference(span), (nuint)i);
            var isContinuation = Vector256.Equals(chunk & mask, pattern);
            acc -= isContinuation;
            i += Vector256<byte>.Count;
        }
        if (i <= span.Length - Vector256<byte>.Count) goto vectorLoop;

        var count = span.Length - Vector256.Sum(acc);

        ref var start = ref MemoryMarshal.GetReference(span);
        if (i >= span.Length) return count;

        scalarLoop:
        if ((Unsafe.Add(ref start, i) & ContinuationMask) != ContinuationPattern) count++;
        if (++i < span.Length) goto scalarLoop;

        return count;
    }

    [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.AggressiveOptimization)]
    private static int CountVector128(ReadOnlySpan<byte> span)
    {
        var mask = Vector128.Create(ContinuationMask);
        var pattern = Vector128.Create(ContinuationPattern);
        var acc = Vector128<byte>.Zero;
        var i = 0;

        vectorLoop:
        {
            var chunk = Vector128.LoadUnsafe(ref MemoryMarshal.GetReference(span), (nuint)i);
            var isContinuation = Vector128.Equals(chunk & mask, pattern);
            acc -= isContinuation;
            i += Vector128<byte>.Count;
        }
        if (i <= span.Length - Vector128<byte>.Count) goto vectorLoop;

        var count = span.Length - Vector128.Sum(acc);

        ref var start = ref MemoryMarshal.GetReference(span);
        if (i >= span.Length) return count;

        scalarLoop:
        if ((Unsafe.Add(ref start, i) & ContinuationMask) != ContinuationPattern) count++;
        if (++i < span.Length) goto scalarLoop;

        return count;
    }

    [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.AggressiveOptimization)]
    private static int CountScalar(ReadOnlySpan<byte> span)
    {
        if (span.IsEmpty) return 0;

        var count = 0;
        ref var start = ref MemoryMarshal.GetReference(span);
        var i = 0;

        loop:
        if ((Unsafe.Add(ref start, i) & ContinuationMask) != ContinuationPattern) count++;
        if (++i < span.Length) goto loop;

        return count;
    }
}
