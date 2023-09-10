using MorePersonaTraits.Settings;
using MorePersonaTraits.Utils;
using Verse;

namespace MorePersonaTraits;

[StaticConstructorOnStartup]
public class MinMaxTraitsRangeChanger
{
    static MinMaxTraitsRangeChanger()
    {
        FieldRefUtils.IntRangeFieldRef.Invoke() = new IntRange(MorePersonaTraitsSettings.minTraits, MorePersonaTraitsSettings.maxTraits);
    }
}