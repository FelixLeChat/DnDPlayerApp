using Android.App;
using Android.OS;
using Android.Widget;

namespace DnDAppAndroid.Activity
{
    [Activity(Label = "LoadCharActivity", Theme = "@android:style/Theme.NoTitleBar")]
    public class LoadCharActivity : Android.App.Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            // Load Char Page
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.loadChar);

            var button = FindViewById<Button>(Resource.Id.back);
            button.SetBackgroundResource(Resource.Drawable.BackButtonImg);
            button.Click += delegate
            {
                StartActivity(typeof(MainActivity));
            };
        }
    }
}