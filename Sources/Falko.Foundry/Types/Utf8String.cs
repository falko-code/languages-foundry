using System.Runtime.CompilerServices;
using System.Text;

namespace Falko.Foundry.Types;

[method: MethodImpl(MethodImplOptions.AggressiveInlining)]
public readonly struct Utf8String(ReadOnlyMemory<byte> utf8Bytes)
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Utf8String(ReadOnlySpan<byte> utf8Bytes) : this(new ReadOnlyMemory<byte>(utf8Bytes.ToArray())) { }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Utf8String(string text) : this(Encoding.UTF8.GetBytes(text)) { }

    public int Length
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => utf8Bytes.Length;
    }

    public bool IsEmpty
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => utf8Bytes.IsEmpty;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ReadOnlySpan<byte> AsSpan() => utf8Bytes.Span;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ReadOnlyMemory<byte> AsMemory() => utf8Bytes;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString() => Encoding.UTF8.GetString(utf8Bytes.Span);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator Utf8String(ReadOnlyMemory<byte> utf8Bytes) => new(utf8Bytes);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator Utf8String(ReadOnlySpan<byte> utf8Bytes) => new(utf8Bytes);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator Utf8String(string text) => new(text);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator ReadOnlyMemory<byte>(Utf8String utf8String) => utf8String.AsMemory();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator ReadOnlySpan<byte>(Utf8String utf8String) => utf8String.AsSpan();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator string(Utf8String utf8String) => utf8String.ToString();
}
