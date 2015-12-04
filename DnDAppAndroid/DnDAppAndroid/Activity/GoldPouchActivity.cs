using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Text;
using Android.Widget;
using DnDAppAndroid.Utility.GoldPouch;

namespace DnDAppAndroid.Activity
{
    [Activity(Label = "GoldPouchActivity", Theme = "@android:style/Theme.NoTitleBar")]
    public class GoldPouchActivity : Android.App.Activity
    {
        private GoldPouch pouch = GoldPouch.Instance;

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

            var editText = FindViewById<EditText>(Resource.Id.goldAmount);
            editText.Text = pouch.CurrentPouch.Gold.ToString();

            editText = FindViewById<EditText>(Resource.Id.silverAmount);
            editText.Text = pouch.CurrentPouch.Silver.ToString();

            editText = FindViewById<EditText>(Resource.Id.copperAmount);
            editText.Text = pouch.CurrentPouch.Copper.ToString();
        }
    }
}