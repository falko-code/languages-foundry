namespace Falko.Foundry.Compilers;

public delegate void CompilerElementAsSpanAction<TArgument>
(
    scoped ReadOnlySpan<byte> utf8Bytes,
    in TArgument argument
);
