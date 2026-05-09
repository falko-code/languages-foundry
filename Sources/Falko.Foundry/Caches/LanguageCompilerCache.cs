using System.Runtime.CompilerServices;
using Falko.Foundry.Compilers;
using Falko.Foundry.Elements;

namespace Falko.Foundry.Caches;

// ReSharper disable once UnusedTypeParameter
internal static class ElementCompilerRelativeCache<TLanguageCompiler, TElement>
    where TLanguageCompiler : class, ILanguageCompiler
    where TElement : ILanguageElement
{
    private static IElementCompiler<TElement>? _compiler;

    public static IElementCompiler<TElement>? Compiler
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => _compiler;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Declare<TElementCompiler>()
        where TElementCompiler : class, IElementCompiler<TElement>, new()
    {
        _compiler = ElementCompilerCache<TElementCompiler, TElement>.Compiler;
    }
}
