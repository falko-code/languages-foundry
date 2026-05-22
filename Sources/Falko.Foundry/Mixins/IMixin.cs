namespace Falko.Foundry.Mixins;

public interface IMixin<TSelf> where TSelf : IMixin<TSelf>, allows ref struct;
