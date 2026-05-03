using Falko.Foundry.Buffers;
using Falko.Foundry.Elements;
using Falko.Foundry.Types;

namespace Falko.Foundry.Languages;

public static class LanguageElementCompilerExtensions
{
    public static TextMemory CompileElement<TElement>(this ILanguageCompiler compiler, scoped in TElement element)
        where TElement : struct, ILanguageElement, allows ref struct
    {
        return SpanByteBuffer.Create
        (
            argument: new CompileContext<TElement>(compiler, in element),
            allocator: static (context, buffer) =>
            {
                var elementCompiler = context.Compiler.GetElementCompiler<TElement>();
                elementCompiler.Compile(context.Element, ref buffer);
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
