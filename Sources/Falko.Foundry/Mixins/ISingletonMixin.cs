namespace Falko.Foundry.Mixins;

public interface ISingletonMixin<TSelf> : IMixin<TSelf>
    where TSelf : ISingletonMixin<TSelf>
{
    static abstract TSelf Instance { get; }
}
