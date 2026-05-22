namespace Falko.Foundry.Mixins;

public interface IStructInitMixin<TSelf> : IMixin<TSelf>
    where TSelf : struct, IStructInitMixin<TSelf>, allows ref struct
{
    bool IsInit { get; }
}
