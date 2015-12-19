using System;
using System.Linq;
using Android.Views;
using Android.Widget;
using DnDAppAndroid.Utility.Class;
using DnDAppAndroid.Utility.Class.Classes;

namespace DnDAppAndroid.Activity.CreateCharacter
{
    class ClassHandler : AbstractHandler
    {
        private readonly Spinner _selector;
        private const string SpinnerSelectionResName = "class_spinner_selected";
        private AbstractClass _selectedClass;

        public ClassHandler(Android.App.Activity activity, RelativeLayout layout, Spinner selector) : base(activity, layout)
        {
            _selector = selector;
            UpdateClass();

            if (_selectedClass != null)
            {
                // Update Next Handler
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

            // Create adapter from classes array
            var classes = ClassManager.Instance.Classes;
            var adapter = new ArrayAdapter(ActivityScope,
                   Android.Resource.Layout.SimpleSpinnerItem, classes.Select(x => x.Name).ToArray());

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
            Editor.PutInt(SpinnerSelectionResName, e.Position);
            Editor.Apply();

            UpdateClass();
        }

        private void UpdateClass()
        {
            var index = Prefs.GetInt(SpinnerSelectionResName, -1);
            if (index < 0)
                return;

            // Get the selected race
            var classArray = ClassManager.Instance.Classes.Select(x => x.Name).ToArray();
            var className = classArray[index];
            _selectedClass = ClassManager.Instance.GetClass(className);
        }
    }
}