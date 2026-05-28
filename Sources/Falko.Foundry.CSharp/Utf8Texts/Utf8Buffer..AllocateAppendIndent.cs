using System.Runtime.CompilerServices;
using Falko.Foundry.CSharp.Compilers;
using Falko.Foundry.Exceptions;
using Falko.Foundry.Utf8Texts;

namespace Falko.Foundry.CSharp.Utf8Texts;

internal static class Utf8BufferExtensions
{
    private const int IndentSpaceCount = 4;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void AllocateAppendIndent
    (
        this scoped ref Utf8Buffer buffer,
        int indent
    )
    {
        DebugArgumentException.ThrowIfDebug
        (
            throwIf: ArgumentOutOfRangeException.ThrowIfNegative, indent
        );
        if (indent <= 0) return;

        var space = CSharpLanguageConstants.Space;
        var spaceCount = indent * IndentSpaceCount;

        buffer.Allocate(space.Length, spaceCount);
        buffer.Append(space, spaceCount);
    }
}
