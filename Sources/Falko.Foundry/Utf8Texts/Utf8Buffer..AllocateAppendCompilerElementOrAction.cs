using System.Runtime.CompilerServices;
using Falko.Foundry.Elements;

namespace Falko.Foundry.Utf8Texts;

public static partial class Utf8BufferExtensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static void AllocateAppendCompilerElementOrAction<TElement>
    (
        this scoped ref Utf8Buffer buffer,
        scoped in CompilerOrLanguageElement<TElement> element,
        Utf8BufferAction<TElement> action,
        int allocateAdditional = 0
    ) where TElement : ILanguageElement
    {
        if (element.TryGetCompilerElement(out var compilerElement))
        {
            var compilerElementString = compilerElement.AsString();
            buffer.Allocate(compilerElementString.Length + allocateAdditional);
            buffer.Append(compilerElementString);
        }
        else
        {
            action(ref buffer, element.LanguageElement);
            buffer.Allocate(allocateAdditional);
        }
    }
}
