using System.Runtime.CompilerServices;
using System.Text;

namespace Falko.Foundry.Types;

[method: MethodImpl(MethodImplOptions.AggressiveInlining)]
public readonly struct TextMemory(ReadOnlyMemory<byte> text) : IEquatable<TextMemory>
{
    private readonly ReadOnlyMemory<byte> _symbols = text;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ReadOnlySpan<byte> AsSpan() => _symbols.Span;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ReadOnlyMemory<byte> AsMemory() => _symbols;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString() => Encoding.UTF8.GetString(_symbols.Span);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(TextMemory other) => _symbols.Equals(other._symbols);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool Equals(object? obj) => obj is TextMemory other && Equals(other);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override int GetHashCode() => _symbols.GetHashCode();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(TextMemory left, TextMemory right) => left.Equals(right);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(TextMemory left, TextMemory right) => left.Equals(right) is false;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator TextMemory(ReadOnlySpan<byte> text) => new(text.ToArray());

    public static implicit operator TextMemory(ReadOnlyMemory<byte> text) => new(text);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator ReadOnlyMemory<byte>(TextMemory textMemory) => textMemory.AsMemory();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator ReadOnlySpan<byte>(TextMemory textMemory) => textMemory.AsSpan();
}
