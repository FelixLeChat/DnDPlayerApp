using System.Collections.Generic;
using System.Linq;
using DnDAppAndroid.Utility.Race.ConcreteRace;

namespace DnDAppAndroid.Utility.Race
{
    class RaceManager
    {
        private static List<AbstractRace> _allRaces; 
        public static List<AbstractRace> AllRace()
        {
            return _allRaces ?? (_allRaces = new List<AbstractRace>()
            {
                new DwarfRace(),
                new ElfRace(),
                new HalfElfRace(),
                new HalflingRace(),
                new HumanRace(),
                new DragonbornRace(),
                new GnomeRace(),
                new HalfOrcRace(),
                new TieflingRace()
            });
        }

        public static AbstractRace GetRace(string raceName)
        {
            return _allRaces.FirstOrDefault(race => race.RaceName == raceName);
        }
    }
}