using System.Reflection;
using FastEnumUtility;

namespace Focus.Apps.EasyNpc.Annotations;

/// <summary>
/// Helper for working with enums using <see cref="SupportedGameAttribute"/> at runtime.
/// </summary>
/// <typeparam name="TEnum">An enum type with game-conditional members.</typeparam>
public static class SupportedGames<TEnum>
    where TEnum : struct, Enum
{
    private static readonly Lazy<ILookup<GameName, TEnum>> lookup =
        new(
            () =>
                (
                    from value in FastEnum.GetValues<TEnum>()
                    from attr in FastEnum
                        .GetMember(value)
                        ?.FieldInfo.GetCustomAttributes<SupportedGameAttribute>() ?? []
                    select (value, game: attr.Game)
                ).ToLookup(x => x.game, x => x.value),
            LazyThreadSafetyMode.PublicationOnly
        );

    /// <summary>
    /// Gets all enum members that are supported for a particular game.
    /// </summary>
    public static IEnumerable<TEnum> GetValues(GameName game)
    {
        return lookup.Value[game];
    }
}
