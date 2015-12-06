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
    }
}