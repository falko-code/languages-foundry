using System.Buffers;
using System.Runtime.CompilerServices;

namespace Falko.Foundry.Utf8Texts;

public ref partial struct Utf8Buffer
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(scoped in Utf8String value) => Append(value.AsSpan());

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(scoped in Utf8String value, int count) => Append(value.AsSpan(), count);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(scoped in Utf8Char value) => Append(value.AsSpan());

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(scoped in Utf8Char value, int count) => Append(value.AsSpan(), count);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(scoped in ReadOnlySpan<byte> value)
    {
        var valueLength = value.Length;
        if (valueLength is 0) return;

        scoped ref var positionRef = ref _position;
        var position = positionRef;
        value.CopyTo(_buffer[position..]);
        positionRef = position + value.Length;
    }

    [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.AggressiveOptimization)]
    public void Append(scoped in ReadOnlySpan<byte> value, int count)
    {
        var valueLength = value.Length;
        if (valueLength is 1) { Append(value[0], count); return; }
        if (count is 1) { Append(value); return; }
        if (valueLength is 0) return;
        if (count <= 0) return;

        var appendLength = checked(value.Length * count);

        scoped ref var positionRef = ref _position;
        var position = positionRef;

        var destination = _buffer.Slice(position, appendLength);

        value.CopyTo(destination); // write first copy

        var written = value.Length; // each iteration doubles the written region
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
        if (count is 1) { Append(value); return; }
        if (count <= 0) return;

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
}
