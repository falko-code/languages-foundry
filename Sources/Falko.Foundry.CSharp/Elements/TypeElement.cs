using Falko.Foundry.Elements;
using Falko.Foundry.Types;

namespace Falko.Foundry.CSharp.Elements;

public readonly struct TypeElement : ILanguageElement
{
    public required TextMemory Name { get; init; }

    public TextMemory Namespace { get; init; }

    public ParamMemory<TypeElement> GenericTypes { get; init; }
}
