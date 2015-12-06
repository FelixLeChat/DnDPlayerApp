using System.Linq;
using Android.Views;
using Android.Widget;
using DnDAppAndroid.Utility.Race;

namespace DnDAppAndroid.Activity.CreateCharacter
{
    class SubRaceHandler : AbstractHandler
    {
        private readonly Spinner _selector;
        private const string SpinnerSelectionResName = "subRace_spinner_selected";
        private AbstractRace _selectedRace;
        public static RaceAlternative SelectedRaceAlternative;
        private AlignmentHandler _nextHandler;

        public SubRaceHandler(Android.App.Activity activity, RelativeLayout layout, Spinner selector) 
            : base(activity, layout)
        {
            _selector = selector;

            _nextHandler = new AlignmentHandler(activity,
                activity.FindViewById<RelativeLayout>(Resource.Id.subClassLayout),
                activity.FindViewById<Spinner>(Resource.Id.aignmentSelector));
        }

        public override void ShowHandler()
        {
            ShowSpinner();

            if (!IsInitialised)
            {
                HandleSpinner();
            }
        }

        protected override void HandleSpinner()
        {
            if (_selectedRace == null)
            {
                HideSpinner();
                return;
            }

            var subRaces = _selectedRace.GetRaceAlternative();
            if (subRaces == null || subRaces.Count == 0)
            {
                HideSpinner();
                return;
            }
            ShowSpinner();

            if(!IsInitialised)
                _selector.ItemSelected += HandleSelection;

            var adapter = new ArrayAdapter(ActivityScope,
                Android.Resource.Layout.SimpleSpinnerItem, subRaces.Select(x => x.RaceAlternativeName).ToArray());

            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            _selector.Adapter = adapter;

            // set current value for subrace if one was selected before
            var selected = Prefs.GetInt(SpinnerSelectionResName, -1);
            if (selected != -1 && selected < subRaces.Count)
            {
                // Set to the last one selected
                _selector.SetSelection(selected);
                _nextHandler.ShowHandler();
            }
            
            IsInitialised = true;
        }

        protected override void HandleSelection(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            // Chance selection on value change
            Editor.PutInt(SpinnerSelectionResName, e.Position);
            Editor.Apply();
            UpdateSubRace();
        }

        public void UpdateSpinner(AbstractRace currentRace)
        {
            _selectedRace = currentRace;
            HandleSpinner();
        }

        private void HideSpinner()
        {
            Layout.Visibility = ViewStates.Gone;
        }

        private void ShowSpinner()
        {
            Layout.Visibility = ViewStates.Visible;
        }

        private void UpdateSubRace()
        {
            var index = Prefs.GetInt(SpinnerSelectionResName, -1);
            if (index < 0 || _selectedRace == null)
                return;

            // Get the selected race
            var subRace = _selectedRace.GetRaceAlternative();
            if (subRace == null)
                return;

            var subRaceArray = subRace.Select(x => x.RaceAlternativeName).ToArray();

            var subRaceName = subRaceArray[index];
            SelectedRaceAlternative = _selectedRace.GetAlternative(subRaceName);
        }
    }
}