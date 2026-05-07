using System.Runtime.CompilerServices;
using System.Text;

namespace Falko.Foundry.Types;

public static class Utf8StringExtensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static int Count(this Utf8String value) => Encoding.UTF8.GetCharCount(value.AsSpan());
}
