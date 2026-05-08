using System.Runtime.CompilerServices;
using System.Text;

namespace Falko.Foundry.Utf8Texts;

[method: MethodImpl(MethodImplOptions.AggressiveInlining)]
public readonly struct Utf8Char(byte value)
{
    public const int Length = 1;

    private readonly byte _value = value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public byte AsByte() => _value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public char ToChar() => Encoding.UTF8.GetChars([_value])[0];

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Utf8String ToUtf8String() => Utf8String.Wrap(new[] { _value });

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString() => ToChar().ToString();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator Utf8Char(ReadOnlySpan<byte> utf8Bytes)
    {
        ArgumentOutOfRangeException.ThrowIfNotEqual(utf8Bytes.Length, 1, nameof(utf8Bytes));

        return new Utf8Char(utf8Bytes[0]);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator byte(Utf8Char utf8Byte) => utf8Byte.AsByte();
}
