using System.Runtime.CompilerServices;

namespace Falko.Foundry.Utf8Text;

public static class StringExtensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Utf8String ToUtf8String(this string value) => new(value);
}
