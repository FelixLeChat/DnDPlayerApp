using Android.App;
using Android.Content;
using Android.OS;
using Android.Preferences;
using Android.Widget;
using DnDAppAndroid.Activity.CreateCharacter;

namespace DnDAppAndroid.Activity
{
    [Activity(Label = "CreateCharActivity", Theme = "@android:style/Theme.NoTitleBar")]
    public class CreateCharActivity : Android.App.Activity
    {
        private ISharedPreferences _prefs;
        private ISharedPreferencesEditor _editor;

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
            editText.Text = _prefs.GetString("characterName", "");
            editText.TextChanged += delegate
            {
                _editor.PutString("characterName", editText.Text);
                _editor.Apply();
                raceHandler.ShowHandler();
            };
            if(editText.Text != "")
                raceHandler.ShowHandler();

            // Add back on button
            var button = FindViewById<Button>(Resource.Id.back);
            button.SetBackgroundResource(Resource.Drawable.BackButtonImg);
            button.Click += delegate
            {
                StartActivity(typeof(MainActivity));
            };
}
    }
}