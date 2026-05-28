namespace Falko.Foundry.Exceptions;

public delegate void RefThrowIfAction<TArgument>
(
    scoped in TArgument argument,
    string? argumentName
) where TArgument : allows ref struct;

public delegate void RefThrowIfAction<TArgument, TComparer>
(
    scoped in TArgument argument,
    scoped in TComparer comparer,
    string? argumentName
) where TArgument : allows ref struct where TComparer : allows ref struct;
