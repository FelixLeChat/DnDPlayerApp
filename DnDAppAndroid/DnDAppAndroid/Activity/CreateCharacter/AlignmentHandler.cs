using Android.Views;
using Android.Widget;
using DnDAppAndroid.Utility.Alignment;

namespace DnDAppAndroid.Activity.CreateCharacter
{
    internal class AlignmentHandler : AbstractHandler
    {
        private readonly Spinner _selector;
        private readonly ClassHandler _nextHandler;
        private const string SpinnerSelectionResName = "alignment_spinner_selected";
        private string _selectedAlignment;

        public AlignmentHandler(Android.App.Activity activity, RelativeLayout layout, Spinner selector) : base(activity, layout)
        {
            _selector = selector;

            _nextHandler = new ClassHandler(activity,
                activity.FindViewById<RelativeLayout>(Resource.Id.classLayout),
                activity.FindViewById<Spinner>(Resource.Id.classSelector));

            if(_selectedAlignment != "")
                UpdateAlignment();
        }

        public override void ShowHandler()
        {
            Layout.Visibility = ViewStates.Visible;

            if (!IsInitialised)
            {
                HandleSpinner();
                IsInitialised = true;
            }
        }

        protected override void HandleSpinner()
        {
            // Event on item selected
            _selector.ItemSelected += HandleSelection;

            var alignments = AlignmentManager.AllAlignment;
            var adapter = new ArrayAdapter(ActivityScope,
                   Android.Resource.Layout.SimpleSpinnerItem, alignments);

            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            _selector.Adapter = adapter;

            // set current value for race if one was selected before
            var selected = Prefs.GetInt(SpinnerSelectionResName, -1);
            if (selected != -1)
            {
                _selector.SetSelection(selected);
            }
        }

        protected override void HandleSelection(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Editor.PutInt(SpinnerSelectionResName, e.Position);
            Editor.Apply();
            UpdateAlignment();
        }

        private void UpdateAlignment()
        {
            var index = Prefs.GetInt(SpinnerSelectionResName, -1);
            if (index < 0)
                return;

            var alignmentArray = AlignmentManager.AllAlignment;

            if (index < alignmentArray.Count)
            {
                _selectedAlignment = alignmentArray[index];
                _nextHandler.ShowHandler();
            }
        }
    }
}