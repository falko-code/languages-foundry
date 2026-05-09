using Falko.Foundry.Compilers;
using Falko.Foundry.Elements;

namespace Falko.Foundry.Caches;

internal static class ElementCompilerCache<TElementCompiler, TElement>
    where TElementCompiler : IElementCompiler<TElement>, new()
    where TElement : ILanguageElement
{
    public static readonly TElementCompiler Compiler = new();
}
