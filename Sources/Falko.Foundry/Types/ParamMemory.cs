using System.Runtime.CompilerServices;

namespace Falko.Foundry.Types;

[method: MethodImpl(MethodImplOptions.AggressiveInlining)]
public readonly struct ParamMemory<T>(ReadOnlyMemory<T> arguments) : IEquatable<ParamMemory<T>>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ParamMemory(params ReadOnlySpan<T> arguments)
        : this(new ReadOnlyMemory<T>(arguments.ToArray())) { }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ParamMemory(params T[] arguments)
        : this(new ReadOnlyMemory<T>(arguments)) { }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ParamMemory(T argument)
        : this(new ReadOnlyMemory<T>([argument])) { }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ParamMemory(ParamMemory<T> arguments)
        : this(arguments._arguments) { }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ParamMemory() : this([]) { }

    private readonly ReadOnlyMemory<T> _arguments = arguments;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ReadOnlySpan<T> AsSpan() => _arguments.Span;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ReadOnlyMemory<T> AsMemory() => _arguments;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString() => _arguments.ToString();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(ParamMemory<T> other) => _arguments.Equals(other._arguments);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool Equals(object? obj) => obj is ParamMemory<T> other && Equals(other);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override int GetHashCode() => _arguments.GetHashCode();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(ParamMemory<T> left, ParamMemory<T> right) => left.Equals(right);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(ParamMemory<T> left, ParamMemory<T> right) => left.Equals(right) is false;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator ParamMemory<T>(T argument) => new(argument);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator ParamMemory<T>(ReadOnlySpan<T> arguments) => new(arguments);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator ParamMemory<T>(ReadOnlyMemory<T> arguments) => new(arguments);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator ParamMemory<T>(T[] arguments) => new(arguments);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator ReadOnlyMemory<T>(ParamMemory<T> paramMemory) => paramMemory.AsMemory();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator ReadOnlySpan<T>(ParamMemory<T> paramMemory) => paramMemory.AsSpan();
}
