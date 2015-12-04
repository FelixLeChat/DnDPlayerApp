using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;

namespace DnDAppAndroid.Activity
{
	[Activity (Label = "DnDAppAndroid", MainLauncher = true, Icon = "@mipmap/icon", Theme= "@android:style/Theme.NoTitleBar")]
	public class MainActivity : Android.App.Activity
    {

		protected override void OnCreate (Bundle savedInstanceState)
		{
            // Create default main page
			Xamarin.Insights.Initialize (XamarinInsights.ApiKey, this);
			base.OnCreate (savedInstanceState);
			SetContentView (Resource.Layout.Main);
            

            // Add a new activity on button click
            Button button = FindViewById<Button> (Resource.Id.loadChar);
			button.Click += delegate 
            {
				StartActivity(typeof(LoadCharActivity));
			};

            // Add Create Character on button click
		    button = FindViewById<Button>(Resource.Id.createChar);
		    button.Click += delegate
		    {
                StartActivity(typeof(CreateCharActivity));
		    };
		}
	}
}
