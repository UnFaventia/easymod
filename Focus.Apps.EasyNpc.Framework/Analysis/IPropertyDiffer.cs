using Focus.Apps.EasyNpc.Data;
using Mutagen.Bethesda.Plugins.Records;

namespace Focus.Apps.EasyNpc.Analysis;

/// <summary>
/// Generic (game-agnostic) property differ.
/// </summary>
/// <remarks>
/// The purpose of the abstraction is to allow individual game-specific classes each supporting the
/// same general "kind" of record with somewhat uniform property types. Implementations will
/// generally narrow both type parameters to define a property differ for a specific kind of record,
/// given a specific type of property, e.g. Skyrim <see cref="Mutagen.Bethesda.Skyrim.INpcGetter"/>
/// and <see cref="NpcProperty"/>.
/// </remarks>
/// <typeparam name="TRecord">The game-specific record type supported by the differ.</typeparam>
/// <typeparam name="TProperty">Game-agnostic property type enumeration.</typeparam>
public interface IPropertyDiffer<TRecord, TProperty>
    where TRecord : IMajorRecordGetter
    where TProperty : Enum
{
    /// <summary>
    /// Compares all supported properties of a record with those of its master records, and produces
    /// a diff for each property.
    /// </summary>
    /// <param name="record">The record to compare with its masters.</param>
    /// <param name="masterRecords">List of records belonging to the masters of the plugin that owns
    /// the <paramref name="record"/>, in listing order, i.e. from top (original plugin that defined
    /// the record) to bottom (last master that has the record).</param>
    /// <returns>A dictionary whose keys are the values of <typeparamref name="TProperty"/> that are
    /// supported by the given <typeparamref name="TRecord"/>, and whose values are the diff flags
    /// for that property indicating similarity to various masters.</returns>
    IReadOnlyDictionary<TProperty, PropertyDiffFlags> Diff(
        TRecord record,
        IReadOnlyList<TRecord> masterRecords
    );
}
