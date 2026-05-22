using System.Runtime.CompilerServices;
using Falko.Foundry.Mixins;

namespace Falko.Foundry.Exceptions;

public static class StructArgumentException
{
    public static void ThrowIfNotInit<T>
    (
        scoped in T element,
        [CallerArgumentExpression(nameof(element))] string? paramName = null
    ) where T : struct, IStructInitMixin<T>, allows ref struct
    {
        if (element.IsInit is false)
        {
            throw new ArgumentException("Structure must be initialized with constructor.", paramName);
        }
    }
}
