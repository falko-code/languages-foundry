using System.Buffers;
using System.Runtime.CompilerServices;

namespace Falko.Foundry.Utf8Texts;

public ref partial struct Utf8Buffer
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void AllocateAppend(scoped in Utf8String value) => AllocateAppend(value.AsSpan());

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void AllocateAppend(scoped in Utf8String value, int repeat) => AllocateAppend(value.AsSpan(), repeat);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void AllocateAppend(scoped in Utf8Char value) => AllocateAppend(value.AsSpan());

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void AllocateAppend(scoped in Utf8Char value, int repeat) => AllocateAppend(value.AsSpan(), repeat);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void AllocateAppend(scoped in ReadOnlySpan<byte> value)
    {
        Allocate(value.Length);
        Append(value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void AllocateAppend(scoped in ReadOnlySpan<byte> value, int repeat)
    {
        Allocate(value.Length, repeat);
        Append(value, repeat);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void AllocateAppend(byte value)
    {
        const int byteLength = 1;
        Allocate(byteLength);
        Append(value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void AllocateAppend(byte value, int repeat)
    {
        Allocate(repeat);
        Append(value, repeat);
    }
}
