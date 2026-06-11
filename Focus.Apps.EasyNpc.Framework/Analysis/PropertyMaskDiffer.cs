using Focus.Apps.EasyNpc.Data;
using Mutagen.Bethesda.Plugins.Records;

namespace Focus.Apps.EasyNpc.Analysis;

/// <summary>
/// Implementation of an <see cref="IPropertyDiffer{TRecord, TProperty}"/> using Mutagen's
/// translation masks (AKA equals mask).
/// </summary>
public abstract class PropertyMaskDiffer<TRecord, TMask, TProperty>
    : IPropertyDiffer<TRecord, TProperty>
    where TRecord : IMajorRecordGetter
    where TMask : MajorRecord.TranslationMask
    where TProperty : Enum
{
    protected abstract IEnumerable<TProperty> SupportedProperties { get; }

    public IReadOnlyDictionary<TProperty, PropertyDiffFlags> Diff(
        TRecord record,
        IReadOnlyList<TRecord> masterRecords
    )
    {
        var diffs = new Dictionary<TProperty, PropertyDiffFlags>();
        foreach (var prop in SupportedProperties)
        {
            var mask = GetMask(prop);
            var diff = Diff(record, masterRecords, mask);
            diffs.Add(prop, diff);
        }
        return diffs;
    }

    private static PropertyDiffFlags Diff(
        TRecord record,
        IReadOnlyList<TRecord> masterRecords,
        TMask mask
    )
    {
        if (masterRecords.Count == 0)
        {
            return PropertyDiffFlags.IdenticalToAllMasters;
        }
        bool hasNonIdenticalMaster = false;
        var result = PropertyDiffFlags.None;
        for (int i = 0; i < masterRecords.Count; i++)
        {
            var master = masterRecords[i];
            if (!record.Equals(master, mask))
            {
                hasNonIdenticalMaster = true;
                continue;
            }
            if (i == 0)
            {
                result |= PropertyDiffFlags.IdenticalToFirstMaster;
            }
            else if (i == masterRecords.Count - 1)
            {
                result |= PropertyDiffFlags.IdenticalToLastMaster;
            }
            else
            {
                result |= PropertyDiffFlags.IdenticalToMiddleMaster;
            }
        }
        if (!hasNonIdenticalMaster)
        {
            result |= PropertyDiffFlags.IdenticalToAllMasters;
        }
        return result;
    }

    protected abstract TMask GetMask(TProperty property);
}
