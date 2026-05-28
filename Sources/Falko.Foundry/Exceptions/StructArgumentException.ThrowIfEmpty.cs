using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Falko.Foundry.Utf8Texts;

namespace Falko.Foundry.Exceptions;

public static partial class StructArgumentException
{
    private const string ValueCannotBeEmptyMessage = "Value cannot be empty.";

    public static void ThrowIfEmpty<T>
    (
        ImmutableArray<T> value,
        [CallerArgumentExpression(nameof(value))] string? paramName = null
    )
    {
        if (value.IsEmpty)
        {
            throw new ArgumentException(ValueCannotBeEmptyMessage, paramName);
        }
    }

    public static void ThrowIfEmpty
    (
        Utf8String value,
        [CallerArgumentExpression(nameof(value))] string? paramName = null
    )
    {
        if (value.IsEmpty)
        {
            throw new ArgumentException(ValueCannotBeEmptyMessage, paramName);
        }
    }

    public static void ThrowIfEmpty<T>
    (
        scoped ReadOnlySpan<T> value,
        [CallerArgumentExpression(nameof(value))] string? paramName = null
    )
    {
        if (value.IsEmpty)
        {
            throw new ArgumentException(ValueCannotBeEmptyMessage, paramName);
        }
    }
}
