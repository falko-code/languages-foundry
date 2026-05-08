using System.Buffers;
using System.Runtime.CompilerServices;

namespace Falko.Foundry.Utf8Texts;

public ref struct Utf8Buffer : IDisposable
{
    public const int StackAllocationThreshold = 256;

    private byte[]? _cache;

    private Span<byte> _buffer;

    private int _position;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Utf8Buffer(Span<byte> stackAllocatedBuffer)
    {
        _buffer = stackAllocatedBuffer;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Utf8Buffer(int capacity)
    {
        _cache = ArrayPool<byte>.Shared.Rent(capacity);
        _buffer = _cache;
    }

    public int Length
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => _buffer.Length;
    }

    public void Allocate(int capacity)
    {
        throw new NotImplementedException();
    }

    public void Append(Utf8String data) => Append(data.AsSpan());

    public void Append(Utf8Char data) => Append(data.AsByte());

    private void Append(ReadOnlySpan<byte> data)
    {
        throw new NotImplementedException();
    }

    private void Append(byte data)
    {
        throw new NotImplementedException();
    }

    public Utf8String ToUtf8String() => throw new NotImplementedException();

    public override string ToString() => ToUtf8String().ToString();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Dispose()
    {
        if (_cache is null) return; // we used stack-allocated buffer

        ArrayPool<byte>.Shared.Return(_cache);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Utf8Buffer Create(int capacity = StackAllocationThreshold)
    {
        capacity = Math.Max(capacity, StackAllocationThreshold); // if array we can't use less capacity

        return new Utf8Buffer(capacity);
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static Utf8String Create<T>
    (
        in T argument,
        Utf8BufferAction<T> action,
        int capacity = StackAllocationThreshold
    ) where T : struct, allows ref struct
    {
        scoped var builder = capacity > StackAllocationThreshold
            ? new Utf8Buffer(capacity)
            : new Utf8Buffer(stackalloc byte[capacity]); // if stackalloc we can use less capacity

        try
        {
            action(ref builder, argument);
            return builder.ToString();
        }
        finally
        {
            builder.Dispose();
        }
    }
}
