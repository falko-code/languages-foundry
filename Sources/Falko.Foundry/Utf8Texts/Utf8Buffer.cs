using System.Buffers;
using System.Runtime.CompilerServices;

namespace Falko.Foundry.Utf8Texts;

public ref struct Utf8Buffer : IDisposable
{
    public const int StackAllocationThreshold = 256; // average good size for stack allocation

    private byte[]? _rented;

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
        capacity = Math.Max(capacity, StackAllocationThreshold + 1); // if array we can't use less capacity

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

    public readonly bool IsOnStack
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => _rented is null;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly ReadOnlySpan<byte> AsSpan() => _buffer[.._position];

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly ReadOnlyMemory<byte> AsMemory() => new(_rented, 0, _position);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Allocate(int amount)
    {
        var newLength = _position + amount;

        if (newLength <= _buffer.Length) return;

        AllocateCore(newLength);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void MoveToHeap()
    {
        if (_rented is not null) return; // already on heap

        StackToHeapCore();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(scoped in Utf8String value) => Append(value.AsSpan());

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(scoped in Utf8String value, int count) => Append(value.AsSpan());

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(scoped in Utf8Char value) => Append(value.AsSpan());

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(scoped in Utf8Char value, int count) => Append(value.AsSpan(), count);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(scoped in ReadOnlySpan<byte> value)
    {
        scoped ref var positionRef = ref _position;
        var position = positionRef;
        value.CopyTo(_buffer[position..]);
        positionRef = position + value.Length;
    }

    [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.AggressiveOptimization)]
    public void Append(scoped in ReadOnlySpan<byte> value, int count)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(count);
        if (count is 1) { Append(value); return; }

        var valueLength = value.Length;
        ArgumentOutOfRangeException.ThrowIfZero(count);
        if (valueLength is 1) { Append(value[0], count); return; }

        var appendLength = checked(value.Length * count);

        scoped ref var positionRef = ref _position;
        var position = positionRef;
        var destination = _buffer.Slice(position, appendLength);

        // write first copy
        value.CopyTo(destination);

        // each iteration doubles the written region
        var written = value.Length;
        while (written < appendLength)
        {
            var copyLength = Math.Min(written, appendLength - written);
            destination[..written][..copyLength].CopyTo(destination[written..]);
            written += copyLength;
        }

        positionRef = position + appendLength;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(byte value)
    {
        scoped ref var positionRef = ref _position;
        var position = positionRef;
        _buffer[position] = value;
        positionRef = position + 1;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(byte value, int count)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(count);
        if (count is 1) { Append(value); return; }
        scoped ref var positionRef = ref _position;
        var position = positionRef;
        _buffer.Slice(position, count).Fill(value);
        positionRef = position + count;
    }

    [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.AggressiveOptimization)]
    private void AllocateCore(int length)
    {
        var newArray = ArrayPool<byte>.Shared.Rent(length);
        var newSpan = newArray.AsSpan();

        AsSpan().CopyTo(newSpan);

        if (_rented is not null) ArrayPool<byte>.Shared.Return(_rented);

        _rented = newArray;
        _buffer = newSpan;
    }

    [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.AggressiveOptimization)]
    private void StackToHeapCore()
    {
        var newArray = ArrayPool<byte>.Shared.Rent(StackAllocationThreshold + 1);
        var newSpan = newArray.AsSpan();

        AsSpan().CopyTo(newSpan);

        _rented = newArray;
        _buffer = newSpan;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly Utf8String ToUtf8String() => new(AsSpan());

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly override string ToString() => ToUtf8String().ToString();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly void Dispose()
    {
        var rented = _rented;

        if (rented is null) return; // we used stack-allocated buffer

        ArrayPool<byte>.Shared.Return(rented);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Utf8Buffer Create(int capacity = StackAllocationThreshold)
    {
        capacity = Math.Max(capacity, StackAllocationThreshold + 1); // if array we can't use less capacity

        return new Utf8Buffer(capacity);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ActionScope<TArgument>
    (
        scoped in TArgument argument,
        Utf8BufferAction<TArgument> action,
        int capacity = StackAllocationThreshold
    ) where TArgument : allows ref struct
    {
        scoped var builder = capacity > StackAllocationThreshold
            ? new Utf8Buffer(capacity)
            : new Utf8Buffer(stackalloc byte[capacity]); // if stackalloc we can use less capacity

        try
        {
            action(ref builder, in argument);
        }
        finally
        {
            builder.Dispose();
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Utf8String StringScope<TArgument>
    (
        scoped in TArgument argument,
        Utf8BufferAction<TArgument> action,
        int capacity = StackAllocationThreshold
    ) where TArgument : allows ref struct
    {
        scoped var builder = capacity > StackAllocationThreshold
            ? new Utf8Buffer(capacity)
            : new Utf8Buffer(stackalloc byte[capacity]); // if stackalloc we can use less capacity

        try
        {
            action(ref builder, in argument);
            return builder.ToUtf8String();
        }
        finally
        {
            builder.Dispose();
        }
    }

    [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.AggressiveOptimization)]
    public static TResult ResultScope<TArgument, TResult>
    (
        scoped in TArgument argument,
        Utf8BufferAction<TArgument, TResult> action,
        int capacity = StackAllocationThreshold
    ) where TArgument : allows ref struct
    {
        scoped var builder = capacity > StackAllocationThreshold
            ? new Utf8Buffer(capacity)
            : new Utf8Buffer(stackalloc byte[capacity]); // if stackalloc we can use less capacity

        try
        {
            return action(ref builder, in argument);
        }
        finally
        {
            builder.Dispose();
        }
    }
}
