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

            button = FindViewById<Button>(Resource.Id.increase_experience);
            button.Click += IncreaceExperience;

            button = FindViewById<Button>(Resource.Id.split_experience);
            button.Click += SplitExperience;

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
            var div = 0;
            int.TryParse(FindViewById<TextView>(Resource.Id.splitAmount_incr).Text, out div);

            if (div == 0)
                return;

            var total = experience/div;
            _currentExperience.Exp += total;
            UpdateVisual();
            UpdateIncr(total);
        }

        private int GetCurrentExperience()
        {
            var editField = FindViewById<EditText>(Resource.Id.experienceAmount_incr);
            var experience = 0;
            int.TryParse(editField.Text, out experience);
            return experience;
        }

        private void UpdateIncr(int value)
        {
            FindViewById<TextView>(Resource.Id.experience_modif).Text = "+ " + value;
        }

        private void UpdateVisual()
        {
            FindViewById<EditText>(Resource.Id.experienceAmount).Text = _currentExperience.Exp.ToString();
        }
    }
}