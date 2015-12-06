using System.Collections.Generic;
using Android.Content;
using Android.Preferences;

namespace DnDAppAndroid.Utility.Experience
{
    class Experience
    {
        private static Experience _ins;
        public static Experience Instance
        {
            get { return _ins ?? (_ins = new Experience()); }
        }
        private readonly ISharedPreferences _prefs;
        private readonly ISharedPreferencesEditor _editor;

        private Experience()
        {
            _prefs = PreferenceManager.GetDefaultSharedPreferences(Android.App.Application.Context);
            _editor = _prefs.Edit();

            Exp = _prefs.GetInt("experience", 0);
        }

        private int _exp;
        public int Exp
        {
            get
            {
                return _exp;
            }
            set
            {
                _exp = value;
                _editor.PutInt("experience", _exp);
                _editor.Apply();
            }
            
        }

        private readonly List<int> _levelExperience = new List<int>()
        {
            0,300,900,2700,6500,
            14000,23000,34000,48000,64000,
            85000,100000,120000,140000,165000,
            195000,225000,265000,305000,355000
        };

        public int GetLevel()
        {
            for (var i = 0; i < _levelExperience.Count; i++)
            {
                if (Exp < _levelExperience[i])
                    return i;
            }

            // Max level
            return _levelExperience.Count;
        }
    }
}