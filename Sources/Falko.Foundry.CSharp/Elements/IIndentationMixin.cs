using Falko.Foundry.Common;
using Falko.Foundry.Elements;

namespace Falko.Foundry.CSharp.Elements;

public interface IIndentationElementMixin<TSelf> : IMixin where TSelf : ILanguageElement, IIndentationElementMixin<TSelf>
{
    int Indent { get; }

    static abstract TSelf MutateIndent(in TSelf element, int indent);
}
