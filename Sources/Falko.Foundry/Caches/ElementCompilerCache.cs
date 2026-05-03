using Falko.Foundry.Elements;

namespace Falko.Foundry.Caches;

public static class ElementCompilerCache<TElementCompiler, TElement>
    where TElementCompiler : IElementCompiler<TElement>, new()
    where TElement : struct, ILanguageElement, allows ref struct
{
    public static readonly TElementCompiler Compiler = new();
}
