using Android.App;
using Android.OS;
using Android.Widget;

namespace DnDAppAndroid.Activity
{
    [Activity(Label = "GoldPouchActivity", Theme = "@android:style/Theme.NoTitleBar")]
    public class GoldPouchActivity : Android.App.Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.goldPouch);

            var button = FindViewById<Button>(Resource.Id.back);
            button.SetBackgroundResource(Resource.Drawable.BackButtonImg);
            button.Click += delegate
            {
                StartActivity(typeof(LoadCharActivity));
            };
        }
    }
}