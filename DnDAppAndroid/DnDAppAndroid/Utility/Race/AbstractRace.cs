using System;
using System.Collections.Generic;
using System.Linq;

namespace DnDAppAndroid.Utility.Race
{
    [Serializable]
    abstract class AbstractRace
    {
        protected List<RaceAlternative> RaceAlternatives = new List<RaceAlternative>();
        public List<RaceAlternative> GetRaceAlternative()
        {
            return RaceAlternatives;
        }

        public RaceAlternative GetAlternative(string name)
        {
            return RaceAlternatives.FirstOrDefault(alternatice => alternatice.RaceAlternativeName == name);
        }

        public string RaceName { get; set; }

        public override string ToString()
        {
            return RaceName;
        }
    }
}