using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace Falko.Foundry.Exceptions;

public static partial class StructArgumentException
{
    public static void ThrowIfEmptyOrDefault<T>
    (
        ImmutableArray<T> value,
        [CallerArgumentExpression(nameof(value))] string? paramName = null
    )
    {
        if (value.IsDefaultOrEmpty)
        {
            throw new ArgumentException("Value cannot be empty or default.", paramName);
        }
    }
}
