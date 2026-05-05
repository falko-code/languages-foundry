using System.Runtime.CompilerServices;

namespace Falko.Foundry.Types;

public static class StringExtensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Utf8String ToUtf8String(this string value) => new(value);
}
