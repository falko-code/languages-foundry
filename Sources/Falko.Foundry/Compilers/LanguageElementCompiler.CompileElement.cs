using System.Runtime.CompilerServices;
using Falko.Foundry.Elements;
using Falko.Foundry.Utf8Texts;

namespace Falko.Foundry.Compilers;

public static class LanguageElementCompilerExtensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void CompileElement<TElement, TArgument>
    (
        this ILanguageCompiler compiler,
        scoped in TElement element,
        scoped in TArgument argument,
        CompilerElementAsSpanAction<TArgument> action
    )
        where TElement : ILanguageElement
        where TArgument : allows ref struct
    {
        Utf8Buffer.ActionScope
        (
            argument: new CompileContext<TElement, TArgument>
            (
                compiler: compiler,
                element: in element,
                argument: in argument,
                action: action
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
    public static CompilerElement<TElement> CompileElement<TElement>
    (
        this ILanguageCompiler compiler,
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

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static CompilerElement<T> ToCompilerElement<T>(this Utf8String elementText)
        where T : ILanguageElement
    {
        return new CompilerElement<T>(elementText);
    }

    [method: MethodImpl(MethodImplOptions.AggressiveInlining)]
    private readonly ref struct CompileContext<TElement>
    (
        ILanguageCompiler compiler,
        scoped in TElement element
    ) where TElement : ILanguageElement
    {
        public readonly ILanguageCompiler Compiler = compiler;

        public readonly TElement Element = element;
    }

    [method: MethodImpl(MethodImplOptions.AggressiveInlining)]
    private readonly ref struct CompileContext<TElement, TArgument>
    (
        ILanguageCompiler compiler,
        scoped in TElement element,
        scoped in TArgument argument,
        CompilerElementAsSpanAction<TArgument> action
    )
        where TElement : ILanguageElement
        where TArgument : allows ref struct
    {
        public readonly ILanguageCompiler Compiler = compiler;

        public readonly TElement Element = element;

        public readonly TArgument Argument = argument;

        public readonly CompilerElementAsSpanAction<TArgument> Action = action;
    }
}
