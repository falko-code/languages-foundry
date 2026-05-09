using System.Buffers;
using System.Runtime.CompilerServices;
using Falko.Foundry.Common;
using Void = Falko.Foundry.Common.Void;

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
    public void AllocateAppend(Utf8String data)
    {
        scoped ref var dataRef = ref data;
        Allocate(dataRef.Length);
        Append(dataRef);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void AllocateAppend(Utf8Char data)
    {
        Allocate(Utf8Char.Length);
        Append(data);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(Utf8String data) => Append(data.AsSpan());

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(Utf8Char data) => Append(data.AsByte());

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void Append(scoped ReadOnlySpan<byte> data)
    {
        var position = _position;
        ref var symbolsRef = ref data;
        var symbolsLength = symbolsRef.Length;
        symbolsRef.CopyTo(_buffer.Slice(position, symbolsLength));
        _position = position + symbolsLength;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void Append(byte data)
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
        in TArgument argument,
        Utf8BufferAction<TArgument> action,
        int capacity = StackAllocationThreshold
    ) where TArgument : struct, allows ref struct
    {
        var cotext = new CreateToStringArgument<TArgument>(in argument, action);

        ResultScope(in cotext, static (scoped ref buffer, in context) =>
        {
            context.Action(ref buffer, in context.Argument);
            return default(Void); // return value is not used
        }, capacity);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Utf8String StringScope<TArgument>
    (
        in TArgument argument,
        Utf8BufferAction<TArgument> action,
        int capacity = StackAllocationThreshold
    ) where TArgument : struct, allows ref struct
    {
        var cotext = new CreateToStringArgument<TArgument>(in argument, action);

        return ResultScope(in cotext, static (scoped ref buffer, in context) =>
        {
            context.Action(ref buffer, in context.Argument);
            return buffer.ToUtf8String();
        }, capacity);
    }

    [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.AggressiveOptimization)]
    public static TResult ResultScope<TArgument, TResult>
    (
        in TArgument argument,
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

    private readonly ref struct CreateToStringArgument<TArgument>
    (
        in TArgument argument,
        Utf8BufferAction<TArgument> action
    ) where TArgument : struct, allows ref struct
    {
        public readonly TArgument Argument = argument;

        public readonly Utf8BufferAction<TArgument> Action = action;
    }
}
