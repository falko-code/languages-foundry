using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Falko.Foundry.Utf8Texts;

namespace Falko.Foundry.Common;

public static class CompileArgumentException
{
    private const string ValueCannotBeDefaultMessage = "Value cannot be default.";

    private const string ValueCannotBeEmptyMessage = "Value cannot be empty.";

    private const string ValueCannotBeEmptyOrDefaultMessage = "Value cannot be empty or default.";

    public static void ThrowIfEmpty<T>(ImmutableArray<T> value, [CallerArgumentExpression(nameof(value))] string? paramName = null)
    {
        if (value.IsEmpty)
        {
            throw new ArgumentException(ValueCannotBeEmptyMessage, paramName);
        }
    }

    public static void ThrowIfEmpty<T>(ReadOnlySpan<T> value, [CallerArgumentExpression(nameof(value))] string? paramName = null)
    {
        if (value.IsEmpty)
        {
            throw new ArgumentException(ValueCannotBeEmptyMessage, paramName);
        }
    }

    public static void ThrowIfEmpty(Utf8String value, [CallerArgumentExpression(nameof(value))] string? paramName = null)
    {
        if (value.IsEmpty)
        {
            throw new ArgumentException(ValueCannotBeEmptyMessage, paramName);
        }
    }

    public static void ThrowIfDefault<T>(ImmutableArray<T> value, [CallerArgumentExpression(nameof(value))] string? paramName = null)
    {
        if (value.IsDefault)
        {
            throw new ArgumentException(ValueCannotBeDefaultMessage, paramName);
        }
    }

    public static void ThrowIfEmptyOrDefault<T>(ImmutableArray<T> value, [CallerArgumentExpression(nameof(value))] string? paramName = null)
    {
        if (value.IsDefaultOrEmpty)
        {
            throw new ArgumentException(ValueCannotBeEmptyOrDefaultMessage, paramName);
        }
    }
}
