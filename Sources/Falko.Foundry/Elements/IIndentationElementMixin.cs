using Falko.Foundry.Common;

namespace Falko.Foundry.Elements;

public interface IIndentationElementMixin<TSelf> : IMixin<TSelf>
    where TSelf : ILanguageElement, IIndentationElementMixin<TSelf>
{
    int Indent { get; }

    static abstract TSelf Copy(in TSelf element, int indent);
}
