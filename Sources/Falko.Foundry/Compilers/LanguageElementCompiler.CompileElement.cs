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
        CompileElementAction<TArgument> action
    )
        where TElement : struct, ILanguageElement, allows ref struct
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
    public static Utf8String CompileElement<TElement>(this ILanguageCompiler compiler, scoped in TElement element)
        where TElement : struct, ILanguageElement, allows ref struct
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
        );
    }

    [method: MethodImpl(MethodImplOptions.AggressiveInlining)]
    private readonly ref struct CompileContext<TElement>
    (
        ILanguageCompiler compiler,
        scoped in TElement element
    ) where TElement : ILanguageElement, allows ref struct
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
        CompileElementAction<TArgument> action
    )
        where TElement : ILanguageElement, allows ref struct
        where TArgument : allows ref struct
    {
        public readonly ILanguageCompiler Compiler = compiler;

        public readonly TElement Element = element;

        public readonly TArgument Argument = argument;

        public readonly CompileElementAction<TArgument> Action = action;
    }
}
