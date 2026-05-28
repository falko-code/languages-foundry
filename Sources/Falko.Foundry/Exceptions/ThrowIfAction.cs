namespace Falko.Foundry.Exceptions;

public delegate void ThrowIfAction<in TArgument>
(
    scoped TArgument argument,
    string? argumentName
) where TArgument : allows ref struct;

public delegate void ThrowIfAction<in TArgument, in TComparer>
(
    scoped TArgument argument,
    scoped TComparer comparer,
    string? argumentName
) where TArgument : allows ref struct where TComparer : allows ref struct;
