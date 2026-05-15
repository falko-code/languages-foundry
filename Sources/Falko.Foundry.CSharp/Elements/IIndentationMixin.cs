using Falko.Foundry.Common;
using Falko.Foundry.Elements;

namespace Falko.Foundry.CSharp.Elements;

public interface IIndentationElementMixin<TSelf> : IMixin<TSelf>
    where TSelf : ILanguageElement, IIndentationElementMixin<TSelf>
{
    int Indent { get; }

    static abstract TSelf Mutate(in TSelf element, int indent);
}
