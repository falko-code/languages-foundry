using System.Runtime.CompilerServices;
using System.Text;

namespace Falko.Foundry.Types;

[SkipLocalsInit]
public readonly struct Utf8String
{
    private readonly ReadOnlyMemory<byte> _utf8Bytes;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Utf8String(ReadOnlyMemory<byte> utf8Bytes) => _utf8Bytes = utf8Bytes;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Utf8String(ReadOnlySpan<byte> utf8Bytes) : this(new ReadOnlyMemory<byte>(utf8Bytes.ToArray())) { }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Utf8String(string text) : this(Encoding.UTF8.GetBytes(text)) { }

    public int Length
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => _utf8Bytes.Length;
    }

    public bool IsEmpty
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => _utf8Bytes.IsEmpty;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ReadOnlySpan<byte> AsSpan() => _utf8Bytes.Span;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString() => Encoding.UTF8.GetString(_utf8Bytes.Span);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator Utf8String(ReadOnlySpan<byte> utf8Bytes) => new(utf8Bytes);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator Utf8String(string text) => new(text);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator ReadOnlySpan<byte>(Utf8String utf8String) => utf8String.AsSpan();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator string(Utf8String utf8String) => utf8String.ToString();

    [MethodImpl(MethodImplOptions.AggressiveOptimization)]
    public static Utf8String operator +(Utf8String left, Utf8String right)
    {
        var leftSpan = left.AsSpan();
        var rightSpan = right.AsSpan();

        var leftSpanLength = leftSpan.Length;

        var combinedBytes = new byte[leftSpanLength + rightSpan.Length];
        var combinedSpan = combinedBytes.AsSpan();

        leftSpan.CopyTo(combinedSpan);
        rightSpan.CopyTo(combinedSpan[leftSpanLength..]);

        return Unsafe(combinedBytes);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Utf8String Unsafe(ReadOnlyMemory<byte> utf8Bytes) => new(utf8Bytes);
}
