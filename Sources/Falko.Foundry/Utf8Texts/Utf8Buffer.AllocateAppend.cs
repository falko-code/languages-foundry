using System.Runtime.CompilerServices;
using Falko.Foundry.Exceptions;

namespace Falko.Foundry.Utf8Texts;

public ref partial struct Utf8Buffer
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void AllocateAppend(Utf8String value) => AllocateAppend(value.AsSpan());

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void AllocateAppend(Utf8String value, int repeat) => AllocateAppend(value.AsSpan(), repeat);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void AllocateAppend(Utf8Char value)
    {
        DebugArgumentException.ThrowIfDebug
        (
            throwIf: StructArgumentException.ThrowIfNotInit, value
        );

        AllocateAppend(value.AsSpan());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void AllocateAppend(Utf8Char value, int repeat)
    {
        DebugArgumentException.ThrowIfDebug
        (
            throwIf: StructArgumentException.ThrowIfNotInit, value
        );

        AllocateAppend(value.AsSpan(), repeat);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void AllocateAppend(scoped ReadOnlySpan<byte> value)
    {
        Allocate(value.Length);
        Append(value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void AllocateAppend(scoped ReadOnlySpan<byte> value, int repeat)
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
