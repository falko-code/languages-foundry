using System.Runtime.CompilerServices;

namespace Falko.Foundry.Utf8Texts;

public static class StringExtensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Utf8String ToUtf8String(this string value) => new(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Utf8String ToUtf8String(this ReadOnlySpan<byte> value) => new(value);
}
