using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using Falko.Foundry.Exceptions;
using Falko.Foundry.Mixins;

namespace Falko.Foundry.Utf8Texts;

public readonly struct Utf8Char : IStructInitMixin<Utf8Char>
{
    public const int MinLength = 1;
    public const int MaxLength = 4;

    private readonly uint _raw;
    private readonly int _length;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Utf8Char(uint raw, int length) => (_raw, _length) = (raw, length);

    public int Length
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => _length;
    }

    public bool IsInit
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => _length > 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ReadOnlySpan<byte> AsSpan()
    {
        return MemoryMarshal.CreateReadOnlySpan
        (
            reference: ref Unsafe.As<uint, byte>
            (
                source: ref Unsafe.AsRef(in _raw)
            ),
            length: _length
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Utf8String ToUtf8String() => AsSpan();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString()
    {
        var length = _length;
        if (length is 0) return string.Empty;
        scoped Span<byte> bytes = stackalloc byte[length];
        AsSpan().CopyTo(bytes);
        return Encoding.UTF8.GetString(bytes);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator Utf8Char(scoped ReadOnlySpan<byte> utf8Bytes) => Single(utf8Bytes);

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Utf8Char Single(scoped ReadOnlySpan<byte> utf8Bytes)
    {
        StructArgumentException.ThrowIfEmpty(utf8Bytes);
        scoped ref var utf8BytesFirstByteRef = ref MemoryMarshal.GetReference(utf8Bytes);

        var expectedLength = GetByteCount(utf8BytesFirstByteRef);
        ArgumentOutOfRangeException.ThrowIfNotEqual(utf8Bytes.Length, expectedLength, nameof(utf8Bytes));

        return new Utf8Char
        (
            raw: Unsafe.ReadUnaligned<uint>(ref utf8BytesFirstByteRef),
            length: expectedLength
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Utf8Char FirstOrDefault(scoped ReadOnlySpan<byte> utf8Bytes)
    {
        if (utf8Bytes.IsEmpty) return default;

        scoped ref var utf8BytesFirstByteRef = ref MemoryMarshal.GetReference(utf8Bytes);

        var expectedLength = GetByteCount(utf8BytesFirstByteRef);
        if (utf8Bytes.Length < expectedLength) return default;

        return new Utf8Char
        (
            raw: Unsafe.ReadUnaligned<uint>(ref utf8BytesFirstByteRef),
            length: expectedLength
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    private static int GetByteCount(byte firstByte) => firstByte switch
    {
        < 0x80 => 1, // 0xxxxxxx ASCII
        < 0xC0 => 0,
        < 0xE0 => 2, // 110xxxxx
        < 0xF0 => 3, // 1110xxxx
        < 0xF8 => 4, // 11110xxx
        _ => 0
    };
}
