using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

#pragma warning disable CS8777 // Parameter must have a non-null value when exiting.

namespace Falko.Foundry.Exceptions;

public static class DebugArgumentException
{
    [Conditional("DEBUG")]
    public static void ThrowIfDebug<TArgument>
    (
        ThrowIfAction<TArgument> throwIf,
        scoped TArgument argument,
        [CallerArgumentExpression(nameof(argument))] string? argumentName = null
    ) where TArgument : allows ref struct
    {
        throwIf(argument, argumentName);
    }

    [Conditional("DEBUG")]
    public static void ThrowIfDebug<TArgument>
    (
        RefThrowIfAction<TArgument> throwIf,
        scoped in TArgument argument,
        [CallerArgumentExpression(nameof(argument))] string? argumentName = null
    ) where TArgument : allows ref struct
    {
        throwIf(in argument, argumentName);
    }

    [Conditional("DEBUG")]
    public static void ThrowIfDebug<TArgument, TComparer>
    (
        ThrowIfAction<TArgument, TComparer> throwIf,
        scoped TArgument argument,
        scoped TComparer comparer,
        [CallerArgumentExpression(nameof(argument))] string? argumentName = null
    ) where TArgument : allows ref struct where TComparer : allows ref struct
    {
        throwIf(argument, comparer, argumentName);
    }

    [Conditional("DEBUG")]
    public static void ThrowIfDebug<TArgument, TComparer>
    (
        RefThrowIfAction<TArgument, TComparer> throwIf,
        scoped in TArgument argument,
        scoped in TComparer comparer,
        [CallerArgumentExpression(nameof(argument))] string? argumentName = null
    ) where TArgument : allows ref struct where TComparer : allows ref struct
    {
        throwIf(in argument, in comparer, argumentName);
    }

    [Conditional("DEBUG")]
    public static void ThrowIfDebugNotNull<TArgument>
    (
        ThrowIfAction<TArgument?> throwIf,
        [NotNull] TArgument? argument,
        [CallerArgumentExpression(nameof(argument))] string? argumentName = null
    ) where TArgument : class
    {
        throwIf(argument, argumentName);
    }

    [Conditional("DEBUG")]
    public static void ThrowIfDebugNotNull<TArgument, TComparer>
    (
        ThrowIfAction<TArgument?, TComparer?> throwIf,
        [NotNull] TArgument? argument,
        [NotNull] TComparer? comparer,
        [CallerArgumentExpression(nameof(argument))] string? argumentName = null
    ) where TArgument : class where TComparer : class
    {
        throwIf(argument, comparer, argumentName);
    }
}
