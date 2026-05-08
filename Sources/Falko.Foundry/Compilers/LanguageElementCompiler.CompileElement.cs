using Falko.Foundry.Elements;
using Falko.Foundry.Utf8Texts;

namespace Falko.Foundry.Compilers;

public static class LanguageElementCompilerExtensions
{
    public static Utf8String CompileElement<TElement>(this ILanguageCompiler compiler, scoped in TElement element)
        where TElement : struct, ILanguageElement, allows ref struct
    {
        return Utf8Buffer.Create
        (
            argument: new CompileContext<TElement>(compiler, in element),
            action: static (scoped ref buffer, in context) =>
            {
                context.Compiler.GetElementCompiler<TElement>().Compile
                (
                    ref buffer,
                    in context.Element
                );
            }
        );
    }

    private readonly ref struct CompileContext<TElement>
    (
        ILanguageCompiler compiler,
        scoped ref readonly TElement element
    ) where TElement : struct, ILanguageElement, allows ref struct
    {
        public readonly ILanguageCompiler Compiler = compiler;

        public readonly TElement Element = element;
    }
}
