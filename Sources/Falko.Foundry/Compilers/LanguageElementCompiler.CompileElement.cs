using Falko.Foundry.Buffers;
using Falko.Foundry.Elements;
using Falko.Foundry.Types;

namespace Falko.Foundry.Compilers;

public static class LanguageElementCompilerExtensions
{
    public static Utf8String CompileElement<TElement>(this ILanguageCompiler compiler, scoped in TElement element)
        where TElement : struct, ILanguageElement, allows ref struct
    {
        return SpanByteBuffer.Create
        (
            argument: new CompileContext<TElement>(compiler, in element),
            allocator: static (context, buffer) =>
            {
                context
                    .Compiler
                    .GetElementCompiler<TElement>()
                    .Compile(context.Element, ref buffer);
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
