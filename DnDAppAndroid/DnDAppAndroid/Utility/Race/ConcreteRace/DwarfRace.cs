using DnDAppAndroid.Utility.Race.ConcreteRaceAlternative;

namespace DnDAppAndroid.Utility.Race.ConcreteRace
{
    class DwarfRace : AbstractRace
    {
        public DwarfRace()
        {
            RaceName = "Dwarf";
            RaceAlternatives.Add(new HillDwarf());
            RaceAlternatives.Add(new MountainDwarf());
        }
    }
}