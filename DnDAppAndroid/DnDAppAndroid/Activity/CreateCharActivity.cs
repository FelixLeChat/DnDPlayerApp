using Android.App;
using Android.Content;
using Android.OS;
using Android.Preferences;
using Android.Views;
using Android.Widget;

namespace DnDAppAndroid.Activity
{
    [Activity(Label = "CreateCharActivity", Theme = "@android:style/Theme.NoTitleBar")]
    public class CreateCharActivity : Android.App.Activity
    {
        private ISharedPreferences prefs;
        private ISharedPreferencesEditor editor;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.createChar);

            // Get preferences
            prefs = PreferenceManager.GetDefaultSharedPreferences(this);
            editor = prefs.Edit();

            // Hide all
            var raceLayout = FindViewById<RelativeLayout>(Resource.Id.raceLayout);
            raceLayout.Visibility = ViewStates.Gone;
            var alignmentLayout = FindViewById<RelativeLayout>(Resource.Id.alignmentLayout);
            alignmentLayout.Visibility = ViewStates.Gone;

            // set old value in character name
            var editText = FindViewById<EditText>(Resource.Id.characterName);
            editText.Text = prefs.GetString("characterName", "");
            if(editText.Text != "")
                ShowRaceChoice();

            // Add back on button
            var button = FindViewById<Button>(Resource.Id.back);
            button.SetBackgroundResource(Resource.Drawable.BackButtonImg);
            button.Click += delegate
            {
                StartActivity(typeof(MainActivity));
            };

            // On character name change, show race choice
            editText.TextChanged += delegate
            {
                editor.PutString("characterName", editText.Text);
                editor.Apply();
                ShowRaceChoice();
            };


            editor.Apply();
        }

        private void ShowRaceChoice()
        {
            var raceLayout = FindViewById<RelativeLayout>(Resource.Id.raceLayout);
            raceLayout.Visibility = ViewStates.Visible;

            // Initialise the spinner on the first time modified the name
            HandleRaceSpinner();
        }
        private void ShowAligmentChoice()
        {
            var alignmentLayout = FindViewById<RelativeLayout>(Resource.Id.alignmentLayout);
            alignmentLayout.Visibility = ViewStates.Visible;
            HandleAlignmentSpinner();
        }

        private void HandleRaceSpinner()
        {
            var raceSelector = FindViewById<Spinner>(Resource.Id.raceSelector);

            // Event on item selected
            raceSelector.ItemSelected += race_Spinner_ItemSelected;
            var adapter = ArrayAdapter.CreateFromResource(
                this, Resource.Array.race_array, Android.Resource.Layout.SimpleSpinnerItem);

            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            raceSelector.Adapter = adapter;

            // set current value for race if one was selected before
            var selected = prefs.GetInt("race_spinner_selected", -1);
            if (selected != -1)
            {
                raceSelector.SetSelection(selected);
            }
        }
        private void HandleAlignmentSpinner()
        {
            var alignmentSelector = FindViewById<Spinner>(Resource.Id.aignmentSelector);

            // Event on item selected
            alignmentSelector.ItemSelected += alignment_Spinner_ItemSelected;
            var adapter = ArrayAdapter.CreateFromResource(
                this, Resource.Array.alignment_array, Android.Resource.Layout.SimpleSpinnerItem);

            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            alignmentSelector.Adapter = adapter;

            // set current value for race if one was selected before
            var selected = prefs.GetInt("alignment_spinner_selected", -1);
            if (selected != -1)
            {
                alignmentSelector.SetSelection(selected);
            }
        }

        private void race_Spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            var spinner = (Spinner)sender;

            // Chance selection on value change
            editor.PutInt("race_spinner_selected", e.Position);
            editor.Apply();
            ShowAligmentChoice();
        }
        private void alignment_Spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            var spinner = (Spinner)sender;

            // Chance selection on value change
            editor.PutInt("alignment_spinner_selected", e.Position);
            editor.Apply();
        }


    }
}