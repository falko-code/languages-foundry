using System.Runtime.CompilerServices;

namespace Falko.Foundry.Utf8Texts;

public readonly partial struct Utf8String
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Enumerator GetEnumerator() => new(AsSpan());

    public ref struct Enumerator
    {
        private ReadOnlySpan<byte> _remaining;
        private Utf8Char _current;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal Enumerator(ReadOnlySpan<byte> span) => _remaining = span;

        public readonly Utf8Char Current
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => _current;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public bool MoveNext()
        {
            var remaining = _remaining;

            var utf8Char = Utf8Char.FirstOrDefault(remaining);
            if (utf8Char.IsDefault) return false;

            _current = utf8Char;
            _remaining = remaining[utf8Char.Length..];
            return true;
        }
    }
}
