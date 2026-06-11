namespace Focus;

public class UnsupportedGameException : Exception
{
    public string GameId { get; private init; } = string.Empty;
    public string GameName { get; private init; } = string.Empty;

    public UnsupportedGameException(string gameId)
        : base(GetMessage(gameId))
    {
        GameId = gameId;
    }

    public UnsupportedGameException(string gameId, string gameName)
        : base(GetMessage(gameId))
    {
        GameId = gameId;
        GameName = gameName;
    }

    public UnsupportedGameException(string gameId, Exception innerException)
        : base(GetMessage(gameId), innerException)
    {
        GameId = gameId;
    }

    public UnsupportedGameException(string gameId, string gameName, Exception innerException)
        : base(GetMessage(gameId), innerException)
    {
        GameId = gameId;
        GameName = gameName;
    }

    private static string GetMessage(string gameId)
    {
        return $"Game '{gameId}' is not supported";
    }
}
