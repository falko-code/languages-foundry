using System.Runtime.CompilerServices;
using System.Text;

namespace Falko.Foundry.Utf8Texts;

public static class Utf8StringExtensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int Count(this Utf8String value) => Encoding.UTF8.GetCharCount(value);
}
