using Falko.Foundry.Elements;

namespace Falko.Foundry.Compilers;

public delegate void CompilerElementAction<TElement, TArgument>
(
    scoped in CompilerElement<TElement> compilerElement,
    in TArgument argument
) where TElement : ILanguageElement;
