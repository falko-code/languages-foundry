using System.Runtime.CompilerServices;
using Falko.Foundry.Exceptions;

namespace Falko.Foundry.Utf8Texts;

public ref partial struct Utf8Buffer
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ActionScope<TArgument>
    (
        scoped in TArgument argument,
        Utf8BufferAction<TArgument> action,
        int capacity = MaxHeapBufferSize
    ) where TArgument : allows ref struct
    {
        DebugArgumentException.ThrowIfDebug
        (
            throwIf: ArgumentOutOfRangeException.ThrowIfNegative, capacity
        );

        scoped var builder = capacity > MaxHeapBufferSize
            ? new Utf8Buffer(capacity)
            : new Utf8Buffer(stackalloc byte[Math.Max(capacity, 0)]); // if stackalloc we can use less capacity

        try
        {
            action(ref builder, in argument);
        }
        finally
        {
            builder.Dispose();
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Utf8String StringScope<TArgument>
    (
        scoped in TArgument argument,
        Utf8BufferAction<TArgument> action,
        int capacity = MaxHeapBufferSize
    ) where TArgument : allows ref struct
    {
        DebugArgumentException.ThrowIfDebug
        (
            throwIf: ArgumentOutOfRangeException.ThrowIfNegative, capacity
        );

        scoped var builder = capacity > MaxHeapBufferSize
            ? new Utf8Buffer(capacity)
            : new Utf8Buffer(stackalloc byte[Math.Max(capacity, 0)]); // if stackalloc we can use less capacity

        try
        {
            action(ref builder, in argument);
            return builder.ToUtf8String();
        }
        finally
        {
            builder.Dispose();
        }
    }

    [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.AggressiveOptimization)]
    public static TResult ResultScope<TArgument, TResult>
    (
        scoped in TArgument argument,
        Utf8BufferAction<TArgument, TResult> action,
        int capacity = MaxHeapBufferSize
    ) where TArgument : allows ref struct
    {
        DebugArgumentException.ThrowIfDebug
        (
            throwIf: ArgumentOutOfRangeException.ThrowIfNegative, capacity
        );

        scoped var builder = capacity > MaxHeapBufferSize
            ? new Utf8Buffer(capacity)
            : new Utf8Buffer(stackalloc byte[Math.Max(capacity, 0)]); // if stackalloc we can use less capacity

        try
        {
            return action(ref builder, in argument);
        }
        finally
        {
            builder.Dispose();
        }
    }
}
