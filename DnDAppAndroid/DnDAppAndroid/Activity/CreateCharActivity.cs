using Android.App;
using Android.Content;
using Android.OS;
using Android.Preferences;
using Android.Views;
using Android.Widget;
using DnDAppAndroid.Activity.CreateCharacter;

namespace DnDAppAndroid.Activity
{
    [Activity(Label = "CreateCharActivity", Theme = "@android:style/Theme.NoTitleBar")]
    public class CreateCharActivity : Android.App.Activity
    {
        private ISharedPreferences _prefs;
        private ISharedPreferencesEditor _editor;
        private const string CharacterNameKey = "pref_character_name";

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.createChar);

            // Get preferences
            _prefs = PreferenceManager.GetDefaultSharedPreferences(this);
            _editor = _prefs.Edit();


            var raceHandler = new RaceHandler(this, 
                FindViewById<RelativeLayout>(Resource.Id.raceLayout),
                FindViewById<Spinner>(Resource.Id.raceSelector));


            //Character Name (Load the one in memory)
            var editText = FindViewById<EditText>(Resource.Id.characterName);
            var name = _prefs.GetString(CharacterNameKey, "");

            editText.TextChanged += delegate
            {
                if (editText.Text == "")
                    return;

                _editor.PutString(CharacterNameKey, editText.Text);
                _editor.Apply();
                raceHandler.ShowHandler();
            };

            // set the registered name
            if (name != "")
            {
                editText.Text = name;
                raceHandler.ShowHandler();
            }
                

            // Add back on button
            var button = FindViewById<Button>(Resource.Id.back);
            button.SetBackgroundResource(Resource.Drawable.BackButtonImg);
            button.Click += delegate
            {
                StartActivity(typeof(MainActivity));
            };


            //Hide other layout
            if (name != "")
                return;

            FindViewById<RelativeLayout>(Resource.Id.raceLayout).Visibility = ViewStates.Gone;
            FindViewById<RelativeLayout>(Resource.Id.subRaceLayout).Visibility = ViewStates.Gone;
            FindViewById<RelativeLayout>(Resource.Id.alignmentLayout).Visibility = ViewStates.Gone;
            FindViewById<RelativeLayout>(Resource.Id.classLayout).Visibility = ViewStates.Gone;
        }
    }
}