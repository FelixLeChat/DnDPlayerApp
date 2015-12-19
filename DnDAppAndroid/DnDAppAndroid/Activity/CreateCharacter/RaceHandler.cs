using System.Linq;
using Android.Views;
using Android.Widget;
using DnDAppAndroid.Utility.Race;

namespace DnDAppAndroid.Activity.CreateCharacter
{
    internal class RaceHandler : AbstractHandler
    {
        private readonly Spinner _selector;
        private const string SpinnerSelectionResName = "race_spinner_selected";
        public static AbstractRace SelectedRace;
        private readonly SubRaceHandler _nextHandler;

        public RaceHandler(Android.App.Activity activity, RelativeLayout layout, Spinner selector)
            : base(activity, layout)
        {
            _selector = selector;

            _nextHandler = new SubRaceHandler(activity,
                activity.FindViewById<RelativeLayout>(Resource.Id.subRaceLayout),
                activity.FindViewById<Spinner>(Resource.Id.subRaceSelector));

            UpdateRace();

            if (SelectedRace != null)
            {
                _nextHandler.UpdateSpinner(SelectedRace);
            }   
        }

        public override void ShowHandler()
        {
            Layout.Visibility = ViewStates.Visible;

            if (IsInitialised) return;
            HandleSpinner();
            IsInitialised = true;
        }

        protected override void HandleSpinner()
        {
            // Event on item selected
            _selector.ItemSelected += HandleSelection;

            // Create adapter from race array
            var races = RaceManager.AllRace();
            var adapter = new ArrayAdapter(ActivityScope,
                   Android.Resource.Layout.SimpleSpinnerItem, races.Select(x => x.RaceName).ToArray());

            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            _selector.Adapter = adapter;

            // set current value for race if one was selected before
            var selected = Prefs.GetInt(SpinnerSelectionResName, -1);
            if (selected != -1)
            {
                // Set to the last one selected
                _selector.SetSelection(selected);
            }
        }

        protected override void HandleSelection(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            // Chance selection on value change
            Editor.PutInt(SpinnerSelectionResName, e.Position);
            Editor.Apply();

            UpdateRace();

            if (SelectedRace != null)
            {
                _nextHandler.UpdateSpinner(SelectedRace);
            }
        }

        private void UpdateRace()
        {
            var index = Prefs.GetInt(SpinnerSelectionResName, -1);
            if (index < 0)
                return;

            // Get the selected race
            var raceArray = RaceManager.AllRace().Select(x => x.RaceName).ToArray();
            var raceName = raceArray[index];
            SelectedRace = RaceManager.GetRace(raceName);
        }
    }
}