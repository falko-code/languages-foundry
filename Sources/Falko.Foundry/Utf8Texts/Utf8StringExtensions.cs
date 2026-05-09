using System.Runtime.CompilerServices;
using System.Text;

namespace Falko.Foundry.Utf8Texts;

public static class Utf8StringExtensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Utf8String ToUtf8String(this string value) => new(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Utf8String ToUtf8String(this ReadOnlySpan<byte> value) => new(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int Count(this Utf8String value) => Encoding.UTF8.GetCharCount(value);
}
