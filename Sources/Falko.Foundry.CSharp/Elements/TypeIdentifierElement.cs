using Falko.Foundry.Elements;
using Falko.Foundry.Utf8Texts;

namespace Falko.Foundry.CSharp.Elements;

public readonly record struct TypeIdentifierElement : ILanguageElement
{
    public int Fingerprint { get; init; }

    public required Utf8String Name { get; init; }

    public required TypeElement Type { get; init; }
}
