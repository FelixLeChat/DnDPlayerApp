using System;
using Android.App;
using Android.OS;
using Android.Widget;
using DnDAppAndroid.Utility.Experience;

namespace DnDAppAndroid.Activity
{
    [Activity(Label = "ExperienceActivity", Theme = "@android:style/Theme.NoTitleBar")]
    public class ExperienceActivity : Android.App.Activity
    {
        private Experience _currentExperience;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.experience);
            // Create your application hered

            _currentExperience = Experience.Instance;

            var button = FindViewById<Button>(Resource.Id.back);
            button.SetBackgroundResource(Resource.Drawable.BackButtonImg);
            button.Click += delegate
            {
                StartActivity(typeof(LoadCharActivity));
            };

            FindViewById<Button>(Resource.Id.increase_experience).Click += IncreaceExperience;
            FindViewById<Button>(Resource.Id.split_experience).Click += SplitExperience;
            FindViewById<Button>(Resource.Id.reset_experience).Click += ComplexDialogClick;

            UpdateVisual();
            UpdateIncr(0);
        }

        private void IncreaceExperience(object obj, EventArgs arg)
        {
            var experience = GetCurrentExperience();
            _currentExperience.Exp += experience;
            UpdateVisual();
            UpdateIncr(experience);
        }

        private void SplitExperience(object obj, EventArgs arg)
        {
            var experience = GetCurrentExperience();
            int div;
            int.TryParse(FindViewById<TextView>(Resource.Id.splitAmount_incr).Text, out div);

            if (div == 0)
                return;

            var total = experience/div;
            _currentExperience.Exp += total;
            UpdateVisual();
            UpdateIncr(total);
        }

        void ComplexDialogClick(object sender, EventArgs e)
        {
            AlertDialog.Builder builder = new AlertDialog.Builder(this);
            AlertDialog alertDialog = builder.Create();
            alertDialog.SetTitle("Experience Reset");
            alertDialog.SetMessage("Do you really want to reset your experience ?");

            alertDialog.SetButton("Yes", (s, ev) =>
            {
                _currentExperience.Reset();
                UpdateVisual();
            });

            alertDialog.SetButton2("No", (s, ev) =>
            {
            });

            alertDialog.Show();
        }

        private int GetCurrentExperience()
        {
            var editField = FindViewById<EditText>(Resource.Id.experienceAmount_incr);
            int experience;
            int.TryParse(editField.Text, out experience);
            return experience;
        }

        private void UpdateIncr(int value)
        {
            var text = (value == 0) ? "" : value.ToString();
            FindViewById<TextView>(Resource.Id.experience_modif).Text = "+ " + text;
        }

        private void UpdateVisual()
        {
            FindViewById<EditText>(Resource.Id.experienceAmount).Text = _currentExperience.Exp.ToString();
            FindViewById<EditText>(Resource.Id.levelAmount).Text = _currentExperience.GetLevel().ToString();
            FindViewById<EditText>(Resource.Id.proficiencyAmount).Text = _currentExperience.GetProficiency().ToString();
        }
    }
}