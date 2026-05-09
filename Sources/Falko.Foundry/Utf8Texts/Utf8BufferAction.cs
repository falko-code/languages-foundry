namespace Falko.Foundry.Utf8Texts;

public delegate void Utf8BufferAction<TArgument>
(
    scoped ref Utf8Buffer buffer,
    in TArgument argument
) where TArgument : allows ref struct;

public delegate TResult Utf8BufferAction<TArgument, out TResult>
(
    scoped ref Utf8Buffer buffer,
    in TArgument argument
) where TArgument : allows ref struct;
