using Falko.Foundry.Elements;

namespace Falko.Foundry.Mixins;

public interface IIndentationMixin<TSelf> : IMixin<TSelf>
    where TSelf : ILanguageElement, IIndentationMixin<TSelf>
{
    int Indent { get; }

    static abstract TSelf Copy(scoped in TSelf element, int indent);
}
