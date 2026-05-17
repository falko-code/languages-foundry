using System.Runtime.CompilerServices;

namespace Falko.Foundry.Utf8Texts;

public ref partial struct Utf8Buffer
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(scoped in Utf8String value) => Append(value.AsSpan());

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(scoped in Utf8String value, int repeat) => Append(value.AsSpan(), repeat);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(scoped in Utf8Char value) => Append(value.AsSpan());

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(scoped in Utf8Char value, int repeat) => Append(value.AsSpan(), repeat);

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

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(scoped in ReadOnlySpan<byte> value, int repeat)
    {
        var valueLength = value.Length;

        if (valueLength is 1) { Append(value[0], repeat); return; } // utf8-char often 1 byte, so first
        if (repeat is 1) { Append(value); return; }
        if (valueLength is 0) return;
        if (repeat <= 0) return;

        AppendCore(value, valueLength, repeat);
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
    public void Append(byte value, int repeat)
    {
        if (repeat is 1) { Append(value); return; }
        if (repeat <= 0) return;

        scoped ref var positionRef = ref _position;
        var position = positionRef;
        _buffer.Slice(position, repeat).Fill(value);
        positionRef = position + repeat;
    }

    [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.AggressiveOptimization)]
    private void AppendCore(scoped in ReadOnlySpan<byte> value, int valueLength, int repeat)
    {
        var appendLength = checked(valueLength * repeat);

        scoped ref var positionRef = ref _position;
        var position = positionRef;

        var destination = _buffer.Slice(position, appendLength);

        value.CopyTo(destination); // write first copy

        var written = valueLength; // each iteration doubles the written region
        while (written < appendLength)
        {
            var copyLength = Math.Min(written, appendLength - written);
            destination[..written][..copyLength].CopyTo(destination[written..]);
            written += copyLength;
        }

        positionRef = position + appendLength;
    }
}
