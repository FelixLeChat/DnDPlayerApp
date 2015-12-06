using System;
using Android.Views;
using Android.Widget;
using DnDAppAndroid.Utility.Alignment;

namespace DnDAppAndroid.Activity.CreateCharacter
{
    class AlignmentHandler : AbstractHandler
    {
        private Spinner _selector;
        private const string SpinnerSelectionResName = "alignment_spinner_selected";

        public AlignmentHandler(Android.App.Activity activity, RelativeLayout layout, Spinner selector) : base(activity, layout)
        {
            _selector = selector;
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
        }
    }
}