using System.Runtime.CompilerServices;
using Falko.Foundry.Caches;
using Falko.Foundry.Common;
using Falko.Foundry.Compilers;
using Falko.Foundry.Elements;

namespace Falko.Foundry.Utf8Texts;

public static class Utf8BufferExtensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static void AllocateAppendCompilerElementOrCompile<TElement>
    (
        this scoped ref Utf8Buffer buffer,
        scoped in CompilerOrLanguageElement<TElement> element,
        ILanguageCompiler compiler,
        int allocateAdditional = 0
    ) where TElement : ILanguageElement
    {
        if (element.TryGetCompilerElement(out var compilerElement))
        {
            var compilerElementString = compilerElement.AsString();
            CompilerException.ThrowIfEmptyOrDefault(compilerElementString, nameof(compilerElement));

            buffer.Allocate(compilerElementString.Length + allocateAdditional);
            buffer.Append(compilerElementString);
        }
        else
        {
            compiler.GetElementCompiler<TElement>().Compile(ref buffer, element.LanguageElement);
            buffer.Allocate(allocateAdditional);
        }
    }

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
            CompilerException.ThrowIfEmptyOrDefault(compilerElementString, nameof(compilerElement));

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
