using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics;
using System.Text;

namespace Falko.Foundry.Types;

public static class Utf8StringExtensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int Count(this Utf8String value) => Encoding.UTF8.GetCharCount(value);
}
