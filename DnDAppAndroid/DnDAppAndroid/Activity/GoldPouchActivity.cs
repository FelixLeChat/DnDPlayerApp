using System;
using Android.App;
using Android.OS;
using Android.Widget;
using DnDAppAndroid.Utility.GoldPouch;

namespace DnDAppAndroid.Activity
{
    [Activity(Label = "GoldPouchActivity", Theme = "@android:style/Theme.NoTitleBar")]
    public class GoldPouchActivity : Android.App.Activity
    {
        private readonly GoldPouch _pouch = GoldPouch.Instance;

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
            editText.Text = _pouch.CurrentPouch.Gold.ToString();

            editText = FindViewById<EditText>(Resource.Id.silverAmount);
            editText.Text = _pouch.CurrentPouch.Silver.ToString();

            editText = FindViewById<EditText>(Resource.Id.copperAmount);
            editText.Text = _pouch.CurrentPouch.Copper.ToString();

            button = FindViewById<Button>(Resource.Id.increase_money);
            button.Click += IncreaceMoney;

            button = FindViewById<Button>(Resource.Id.split_money);
            button.Click += SplitMoney;

            button = FindViewById<Button>(Resource.Id.reset_money);
            button.Click += ComplexDialogClick;
        }

        private void IncreaceMoney(object obj, EventArgs e)
        {
            var selected = GetSelectedAmount();
            _pouch.AddAmountToPouch(selected);
            UpdateVisual();
            UpdateIncr(selected);
        }

        private void SplitMoney(object obj, EventArgs e)
        {
            var splitText = FindViewById<EditText>(Resource.Id.splitAmount_incr).Text;
            int splitCount;
            int.TryParse(splitText, out splitCount);

            // can't split with 0
            if (splitCount == 0)
                return;

            // get value in copper coins
            var value = GetSelectedAmount().GetTotalValue();
            value = value/splitCount;
            var reduced = GoldPouch.Amount.Reduce(value);
            _pouch.AddAmountToPouch(reduced);
            UpdateVisual();
            UpdateIncr(reduced);
        }

        void ComplexDialogClick(object sender, EventArgs e)
        {
            AlertDialog.Builder builder = new AlertDialog.Builder(this);
            AlertDialog alertDialog = builder.Create();
            alertDialog.SetTitle("Pouch Reset");
            alertDialog.SetMessage("Do you really want to reset your pouch ?");

            alertDialog.SetButton("Yes", (s, ev) =>
            {
                _pouch.ResetPouch();
                UpdateVisual();
            });

            alertDialog.SetButton2("No", (s, ev) =>
            {
            });

            alertDialog.Show();
        }

        private GoldPouch.Amount GetSelectedAmount()
        {
            var editField = FindViewById<EditText>(Resource.Id.goldAmount_incr);
            int gold;
            int.TryParse(editField.Text, out gold);

            editField = FindViewById<EditText>(Resource.Id.silverAmount_incr);
            int silver;
            int.TryParse(editField.Text, out silver);

            editField = FindViewById<EditText>(Resource.Id.copperAmount_incr);
            int copper;
            int.TryParse(editField.Text, out copper);

            return new GoldPouch.Amount()
            {
                Gold = gold,
                Silver = silver,
                Copper = copper
            };
        }

        private void UpdateVisual()
        {
            FindViewById<EditText>(Resource.Id.goldAmount_incr).Text = "";
            FindViewById<EditText>(Resource.Id.silverAmount_incr).Text = "";
            FindViewById<EditText>(Resource.Id.copperAmount_incr).Text = "";
            FindViewById<EditText>(Resource.Id.goldAmount).Text = _pouch.CurrentPouch.Gold.ToString();
            FindViewById<EditText>(Resource.Id.silverAmount).Text = _pouch.CurrentPouch.Silver.ToString();
            FindViewById<EditText>(Resource.Id.copperAmount).Text = _pouch.CurrentPouch.Copper.ToString();
        }

        private void UpdateIncr(GoldPouch.Amount amount)
        {
            FindViewById<TextView>(Resource.Id.gold_modif).Text = "+ " + amount.Gold;
            FindViewById<TextView>(Resource.Id.silver_modif).Text = "+ " + amount.Silver;
            FindViewById<TextView>(Resource.Id.copper_modif).Text = "+ " + amount.Copper;
        }
    }
}