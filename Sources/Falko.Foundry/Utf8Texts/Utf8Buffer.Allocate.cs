using System.Buffers;
using System.Runtime.CompilerServices;

namespace Falko.Foundry.Utf8Texts;

public ref partial struct Utf8Buffer
{
    public readonly bool IsOnStack
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => _rented is null;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void MoveToHeap()
    {
        if (_rented is not null) return; // already on heap

        MoveToHeapCore();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Allocate(int amount)
    {
        var newLength = _position + amount;

        if (newLength <= _buffer.Length) return;

        AllocateCore(newLength);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Allocate(int amount, int count)
    {
        if (count <= 0) return;
        Allocate(checked(amount * count));
    }

    [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.AggressiveOptimization)]
    private void MoveToHeapCore()
    {
        var newArray = ArrayPool<byte>.Shared.Rent(MinArrayBufferSize);
        var newSpan = newArray.AsSpan();

        AsSpan().CopyTo(newSpan);

        _rented = newArray;
        _buffer = newSpan;
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
}
