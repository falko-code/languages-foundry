using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using Falko.Foundry.Common;

namespace Falko.Foundry.Utf8Texts;

public readonly struct Utf8Char : ISafeStruct
{
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
        get => _length is 0;
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
        Span<byte> bytes = stackalloc byte[_length];
        AsSpan().CopyTo(bytes);
        return Encoding.UTF8.GetString(bytes);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator Utf8Char(scoped in ReadOnlySpan<byte> utf8Bytes) => From(in utf8Bytes);

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Utf8Char From(scoped in ReadOnlySpan<byte> utf8Bytes)
    {
        CompileArgumentException.ThrowIfEmpty(utf8Bytes);
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
    private static int GetByteCount(byte firstByte) => firstByte switch
    {
        < 0x80 => 1, // 0xxxxxxx ASCII
        < 0xC0 => throw new ArgumentException("Invalid UTF-8 first byte."),
        < 0xE0 => 2, // 110xxxxx
        < 0xF0 => 3, // 1110xxxx
        < 0xF8 => 4, // 11110xxx
        _ => throw new ArgumentException("Invalid UTF-8 first byte.")
    };
}
