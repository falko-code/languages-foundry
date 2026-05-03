using System.Runtime.CompilerServices;
using Falko.Foundry.Caches;
using Falko.Foundry.Elements;

namespace Falko.Foundry.Languages;

public abstract class LanguageCompiler<TSelf> : ILanguageCompiler
    where TSelf : class, ILanguageCompiler
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected static void SetElementCompiler<TElementCompiler, TElement>()
        where TElementCompiler : class, IElementCompiler<TElement>, new()
        where TElement : struct, ILanguageElement, allows ref struct
    {
        ElementCompilerRelativeCache<TSelf, TElement>.Declare<TElementCompiler>();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LanguageElementCompiler<TElement> GetElementCompiler<TElement>()
        where TElement : struct, ILanguageElement, allows ref struct
    {
        var elementCompiler = ElementCompilerRelativeCache<TSelf, TElement>.Compiler;

        if (elementCompiler is not null)
        {
            return new LanguageElementCompiler<TElement>(this, elementCompiler);
        }

        throw new NotSupportedException
        (
            message: $"The element type {typeof(TElement).FullName} is not supported by the {nameof(TSelf)}"
        );
    }
}
