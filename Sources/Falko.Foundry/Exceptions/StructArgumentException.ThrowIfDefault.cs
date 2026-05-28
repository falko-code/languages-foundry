using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace Falko.Foundry.Exceptions;

public static partial class StructArgumentException
{
    public static void ThrowIfDefault<T>
    (
        ImmutableArray<T> value,
        [CallerArgumentExpression(nameof(value))] string? paramName = null
    )
    {
        if (value.IsDefault)
        {
            throw new ArgumentException("Value cannot be default.", paramName);
        }
    }
}
