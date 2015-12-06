using DnDAppAndroid.Utility.Race.ConcreteRaceAlternative;

namespace DnDAppAndroid.Utility.Race.ConcreteRace
{
    internal class ElfRace : AbstractRace
    {
        public ElfRace()
        {
            RaceName = "Elf";
            RaceAlternatives.Add(new HighElf());
            RaceAlternatives.Add(new WoodElf());
            RaceAlternatives.Add(new DarkElf());
        }
    }
}