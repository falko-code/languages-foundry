using System.Runtime.CompilerServices;
using Falko.Foundry.Elements;
using Falko.Foundry.Utf8Texts;

namespace Falko.Foundry.Compilers;

public static class LanguageElementCompilerExtensions
{
    extension(ILanguageCompiler compiler)
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void CompileElement<TElement, TArgument>
        (
            scoped in TElement element,
            CompilerElementAsSpanAction<TArgument> action,
            scoped in TArgument argument
        ) where TElement : ILanguageElement
        {
            Utf8Buffer.ActionScope
            (
                argument: new CompileContext<TElement, TArgument>
                (
                    compiler: compiler,
                    element: in element,
                    action: action,
                    argument: in argument
                ),
                action: static (scoped ref buffer, in context) =>
                {
                    context.Compiler.GetElementCompiler<TElement>().Compile
                    (
                        ref buffer,
                        in context.Element
                    );

                    context.Action(buffer.AsSpan(), in context.Argument);
                }
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CompilerElement<TElement> CompileElement<TElement>
        (
            scoped in TElement element
        ) where TElement : ILanguageElement
        {
            return Utf8Buffer.StringScope
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
            ).ToCompilerElement<TElement>();
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static CompilerElement<T> ToCompilerElement<T>
    (
        this Utf8String elementText
    ) where T : ILanguageElement
    {
        return new CompilerElement<T>(elementText);
    }

    [method: MethodImpl(MethodImplOptions.AggressiveInlining)]
    private readonly ref struct CompileContext<TElement>
    (
        ILanguageCompiler compiler,
        in TElement element
    ) where TElement : ILanguageElement
    {
        public readonly ILanguageCompiler Compiler = compiler;

        public readonly ref readonly TElement Element = ref element;
    }

    [method: MethodImpl(MethodImplOptions.AggressiveInlining)]
    private readonly ref struct CompileContext<TElement, TArgument>
    (
        ILanguageCompiler compiler,
        in TElement element,
        CompilerElementAsSpanAction<TArgument> action,
        in TArgument argument
    ) where TElement : ILanguageElement
    {
        public readonly ILanguageCompiler Compiler = compiler;

        public readonly ref readonly TElement Element = ref element;

        public readonly CompilerElementAsSpanAction<TArgument> Action = action;

        public readonly ref readonly TArgument Argument = ref argument;
    }
}
