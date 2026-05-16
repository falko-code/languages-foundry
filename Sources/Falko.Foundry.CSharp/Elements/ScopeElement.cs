using System.Collections.Immutable;
using Falko.Foundry.Common;
using Falko.Foundry.Elements;

namespace Falko.Foundry.CSharp.Elements;

public readonly struct ScopeElement() : ILanguageElement, ISafeStruct, IIndentationElementMixin<ScopeElement>
{
    public static readonly ScopeElement Empty = new();

    public bool IsInit { get; } = true;

    public int Indent { get; init; }

    public ImmutableArray<IndentationCompilerAction> Elements { get; init; }
        = ImmutableArray<IndentationCompilerAction>.Empty;

    public static ScopeElement MutateIndent
    (
        in ScopeElement element,
        int indent
    )
    {
        return element with { Indent = indent };
    }
}
