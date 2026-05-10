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
        _rented = ArrayPool<byte>.Shared.Rent(capacity);
        _buffer = _rented;
    }

    public int Length
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => _buffer.Length;
    }

    public int Count
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => _position;
    }

    public bool IsEmpty
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => Length is 0;
    }

    public bool IsAllocatedOnStack
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => _rented is null;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly ReadOnlySpan<byte> AsSpan() => _buffer[.._position];

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Allocate(int amount)
    {
        var newLength = _position + amount;

        if (newLength <= _buffer.Length) return;

        AllocateCore(newLength);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(scoped in Utf8String data)
    {
        Append(data.AsSpan());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(scoped in Utf8Char data)
    {
        scoped ref var positionRef = ref _position;
        var position = positionRef;
        data.AsSpan().CopyTo(_buffer[position..]);
        positionRef = position + data.Length;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(scoped in ReadOnlySpan<byte> data)
    {
        scoped ref var positionRef = ref _position;
        var position = positionRef;
        data.CopyTo(_buffer[position..]);
        positionRef = position + data.Length;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(byte data)
    {
        scoped ref var positionRef = ref _position;
        var position = positionRef;
        _buffer[position] = data;
        positionRef = position + 1;
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

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Utf8String ToUtf8String() => new(AsSpan());

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString() => ToUtf8String().ToString();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Dispose()
    {
        if (_rented is null) return; // we used stack-allocated buffer

        ArrayPool<byte>.Shared.Return(_rented);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Utf8Buffer Create(int capacity = StackAllocationThreshold)
    {
        capacity = Math.Max(capacity, StackAllocationThreshold); // if array we can't use less capacity

        return new Utf8Buffer(capacity);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ActionScope<TArgument>
    (
        Utf8BufferAction<TArgument> action,
        scoped in TArgument argument,
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
        Utf8BufferAction<TArgument> action,
        scoped in TArgument argument,
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
        Utf8BufferAction<TArgument, TResult> action,
        scoped in TArgument argument,
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
