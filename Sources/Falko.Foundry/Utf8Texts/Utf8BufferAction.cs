namespace Falko.Foundry.Utf8Texts;

public delegate void Utf8BufferAction<T>(scoped ref Utf8Buffer buffer, in T state)
    where T : allows ref struct;
