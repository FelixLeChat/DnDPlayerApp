using Android.Content;
using Android.Preferences;
using Android.Views;
using Android.Widget;

namespace DnDAppAndroid.Activity.CreateCharacter
{
    abstract class AbstractHandler
    {
        protected Android.App.Activity ActivityScope;
        protected ISharedPreferences Prefs;
        protected ISharedPreferencesEditor Editor;
        protected RelativeLayout Layout;
        protected bool IsInitialised;

        protected AbstractHandler(Android.App.Activity activity, RelativeLayout layout)
        {
            ActivityScope = activity;
            Layout = layout;

            Prefs = PreferenceManager.GetDefaultSharedPreferences(ActivityScope);
            Editor = Prefs.Edit();

            // Hide Layout
            Layout.Visibility = ViewStates.Gone;
        }

        public abstract void ShowHandler();
        protected abstract void HandleSpinner();
        protected abstract void HandleSelection(object sender, AdapterView.ItemSelectedEventArgs e);
    }
}