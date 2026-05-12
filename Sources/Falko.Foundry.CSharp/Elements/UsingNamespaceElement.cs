using Falko.Foundry.Common;
using Falko.Foundry.Elements;
using Falko.Foundry.Utf8Texts;

namespace Falko.Foundry.CSharp.Elements;

public readonly struct UsingNamespaceElement() : ILanguageElement, ISafeStruct
{
    public bool IsInit { get; } = true;

    public required Utf8String Namespace { get; init; }
}
