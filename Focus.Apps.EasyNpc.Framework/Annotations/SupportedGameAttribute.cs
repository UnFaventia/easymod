namespace Focus.Apps.EasyNpc.Annotations;

/// <summary>
/// Attribute available to members of certain types (e.g. <see cref="Data.NpcProperty"/>)
/// indicating which games they apply to.
/// </summary>
[AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
public class SupportedGameAttribute(GameName game) : Attribute
{
    /// <summary>
    /// The game supported by this member.
    /// </summary>
    /// <remarks>
    /// Multiple <see cref="SupportedGameAttribute"/>s can be added to tag multiple games.
    /// </remarks>
    public GameName Game { get; set; } = game;
}
