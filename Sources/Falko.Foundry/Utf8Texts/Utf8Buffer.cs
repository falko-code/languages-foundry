using System.Buffers;
using System.Runtime.CompilerServices;

namespace Falko.Foundry.Utf8Texts;

public ref partial struct Utf8Buffer
{
    public const int MaxHeapBufferSize = 256; // average good size for stack allocation
    public const int MinArrayBufferSize = MaxHeapBufferSize + 1; // if array we can't use less capacity

    private byte[]? _rented;
    private Span<byte> _buffer;
    private int _position;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Utf8Buffer(Span<byte> stackAllocatedBuffer) => _buffer = stackAllocatedBuffer;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Utf8Buffer(int capacity)
    {
        ThrowIfDebug(ArgumentOutOfRangeException.ThrowIfNegative, capacity);
        capacity = Math.Max(capacity, MinArrayBufferSize); // if array we can't use less capacity
        _rented = ArrayPool<byte>.Shared.Rent(capacity);
        _buffer = _rented;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Utf8Buffer() : this(0) { }

    public readonly int Capacity
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => _buffer.Length;
    }

    public readonly int Count
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => _position;
    }

    public readonly bool IsEmpty
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => Capacity is 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly ReadOnlySpan<byte> AsSpan() => _buffer[.._position];

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly ReadOnlyMemory<byte> AsMemory() => new(_rented, 0, _position);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly Utf8String ToUtf8String() => new(AsSpan());

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly override string ToString() => ToUtf8String().ToString();
}
