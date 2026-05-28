using System.Buffers;
using System.Runtime.CompilerServices;
using System.Text;

namespace Falko.Foundry.Utf8Texts;

[SkipLocalsInit]
public readonly struct Utf8String : IEquatable<Utf8String>, IComparable<Utf8String>,
    ISpanFormattable, IUtf8SpanFormattable
{
    public static readonly Utf8String Empty = Wrap(ReadOnlyMemory<byte>.Empty);

    private readonly ReadOnlyMemory<byte> _utf8Bytes;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Utf8String(ReadOnlyMemory<byte> utf8Bytes) => _utf8Bytes = utf8Bytes;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Utf8String(scoped ReadOnlySpan<byte> utf8Bytes) : this(new ReadOnlyMemory<byte>(utf8Bytes.ToArray())) { }

    [MethodImpl(MethodImplOptions.AggressiveOptimization)]
    public Utf8String(string text)
    {
        var maxByteCount = Encoding.UTF8.GetMaxByteCount(text.Length);

        if (maxByteCount > Utf8Buffer.MaxHeapBufferSize)
        {
            _utf8Bytes = new ReadOnlyMemory<byte>(Encoding.UTF8.GetBytes(text));
            return;
        }

        Span<byte> stackBuffer = stackalloc byte[maxByteCount];
        var written = Encoding.UTF8.GetBytes(text, stackBuffer);
        _utf8Bytes = new ReadOnlyMemory<byte>(stackBuffer[..written].ToArray());
    }

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
    public ReadOnlyMemory<byte> AsMemory() => _utf8Bytes;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override int GetHashCode()
    {
        var hash = new HashCode();
        hash.AddBytes(AsSpan());
        return hash.ToHashCode();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool Equals(object? obj) => obj is Utf8String other && Equals(other);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(Utf8String other) => AsSpan().SequenceEqual(other.AsSpan());

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int CompareTo(Utf8String other) => AsSpan().SequenceCompareTo(other.AsSpan());

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString() => Encoding.UTF8.GetString(AsSpan());

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ToString(string? format, IFormatProvider? provider) => ToString();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryFormat
    (
        scoped Span<char> destination,
        out int charsWritten,
        scoped ReadOnlySpan<char> format,
        IFormatProvider? provider
    ) => Encoding.UTF8.TryGetChars(AsSpan(), destination, out charsWritten);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryFormat
    (
        scoped Span<byte> utf8Destination,
        out int bytesWritten,
        scoped ReadOnlySpan<char> format,
        IFormatProvider? provider
    )
    {
        scoped var span = AsSpan();

        if (span.TryCopyTo(utf8Destination))
        {
            bytesWritten = span.Length;
            return true;
        }

        bytesWritten = 0;
        return false;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator Utf8String(scoped ReadOnlySpan<byte> utf8Bytes) => new(utf8Bytes);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator Utf8String(string text) => new(text);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator ReadOnlySpan<byte>(Utf8String utf8String) => utf8String.AsSpan();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator string(Utf8String utf8String) => utf8String.ToString();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(Utf8String left, Utf8String right) => left.Equals(right);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(Utf8String left, Utf8String right) => left.Equals(right) is false;

    [MethodImpl(MethodImplOptions.AggressiveOptimization)]
    public static Utf8String operator +(Utf8String left, Utf8String right)
    {
        scoped var leftSpan = left.AsSpan();
        scoped var rightSpan = right.AsSpan();

        var leftSpanLength = leftSpan.Length;

        var combinedBytes = new byte[leftSpanLength + rightSpan.Length];
        scoped var combinedSpan = combinedBytes.AsSpan();

        leftSpan.CopyTo(combinedSpan);
        rightSpan.CopyTo(combinedSpan[leftSpanLength..]);

        return Wrap(combinedBytes);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Utf8String operator +(Utf8String left, string right) => Concat
    (
        sourceSpan: left.AsSpan(),
        targetString: right,
        targetIsRight: true
    );

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Utf8String operator +(string left, Utf8String right) => Concat
    (
        sourceSpan: right.AsSpan(),
        targetString: left,
        targetIsRight: false
    );

    [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.AggressiveOptimization)]
    private static Utf8String Concat
    (
        scoped ReadOnlySpan<byte> sourceSpan,
        string targetString,
        bool targetIsRight
    )
    {
        var maxByteCount = Encoding.UTF8.GetMaxByteCount(targetString.Length);

        if (maxByteCount <= Utf8Buffer.MaxHeapBufferSize)
        {
            Span<byte> stackBuffer = stackalloc byte[maxByteCount];

            var written = Encoding.UTF8.GetBytes(targetString, stackBuffer);
            var targetSpan = stackBuffer[..written];

            return targetIsRight
                ? Combine(sourceSpan, targetSpan)
                : Combine(targetSpan, sourceSpan);
        }

        var rentedBytes = ArrayPool<byte>.Shared.Rent(maxByteCount);

        try
        {
            var written = Encoding.UTF8.GetBytes(targetString, rentedBytes);
            var targetSpan = rentedBytes.AsSpan(0, written);

            return targetIsRight
                ? Combine(sourceSpan, targetSpan)
                : Combine(targetSpan, sourceSpan);
        }
        finally
        {
            ArrayPool<byte>.Shared.Return(rentedBytes);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static Utf8String Combine
    (
        scoped ReadOnlySpan<byte> left,
        scoped ReadOnlySpan<byte> right
    )
    {
        var combinedBytes = new byte[left.Length + right.Length];

        left.CopyTo(combinedBytes);
        right.CopyTo(combinedBytes.AsSpan(left.Length));

        return Wrap(combinedBytes);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Utf8String Wrap(ReadOnlyMemory<byte> utf8Bytes) => new(utf8Bytes);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Enumerator GetEnumerator() => new(AsSpan());

    public ref struct Enumerator
    {
        private ReadOnlySpan<byte> _remaining;
        private Rune _current;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal Enumerator(ReadOnlySpan<byte> span) => _remaining = span;

        public Rune Current
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => _current;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool MoveNext()
        {
            var remaining = _remaining;

            if (remaining.IsEmpty) return false;

            Rune.DecodeFromUtf8(remaining, out _current, out var bytesConsumed);
            _remaining = remaining[bytesConsumed..];
            return true;
        }
    }
}
