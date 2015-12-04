using System;
using Android.Content;
using Android.Preferences;

namespace DnDAppAndroid.Utility.GoldPouch
{
    class GoldPouch
    {
        private static GoldPouch _ins;
        public static GoldPouch Instance
        {
            get { return _ins ?? (_ins = new GoldPouch()); }
        }
        private readonly ISharedPreferences _prefs;
        private readonly ISharedPreferencesEditor _editor;

        public class Amount
        {
            public int Platinum;
            public int Gold;
            public int Electrum;
            public int Silver;
            public int Copper;

            private const int _copperValue = 1;
            private const int _silverValue = 10*_copperValue;
            private const int _electrumValue = 5*_silverValue;
            private const int _goldValue = 10*_silverValue;
            private const int _platinumValue = 10*_goldValue;

            public static Amount operator +(Amount amount1, Amount amount2)
            {
                var total = amount1.GetTotalValue() + amount2.GetTotalValue();

                if (total < 0)
                    throw new Exception("out of funds");

                var totalGold = total/_goldValue;
                var left = total%_goldValue;
                var totalSilver = left/_silverValue;
                left = left%_silverValue;
                var totalCopper = left;

                return new Amount()
                {
                    Gold = totalGold,
                    Silver = totalSilver,
                    Copper = totalCopper
                };
            }

            public int GetTotalValue()
            {
                return (Platinum * _platinumValue) +
                       (Gold * _goldValue) +
                       (Electrum * _electrumValue) +
                       (Silver * _silverValue) +
                       (Copper * _copperValue);
            }
        }

        public Amount CurrentPouch { get; private set; }
        private GoldPouch()
        {
            CurrentPouch = new Amount();
            _prefs = PreferenceManager.GetDefaultSharedPreferences(Android.App.Application.Context);
            _editor = _prefs.Edit();

            CurrentPouch.Platinum = _prefs.GetInt("pouch_platinum", 0);
            CurrentPouch.Gold = _prefs.GetInt("pouch_gold", 0);
            CurrentPouch.Electrum = _prefs.GetInt("pouch_electrum", 0);
            CurrentPouch.Silver = _prefs.GetInt("pouch_silver", 0);
            CurrentPouch.Copper = _prefs.GetInt("pouch_copper", 0);
        }

        public void AddAmountToPouch(Amount amount)
        {
            CurrentPouch += amount;
            SavePouch();
        }

        private void SavePouch()
        {
            _editor.PutInt("pouch_platinum", CurrentPouch.Platinum);
            _editor.PutInt("pouch_gold", CurrentPouch.Gold);
            _editor.PutInt("pouch_electrum", CurrentPouch.Electrum);
            _editor.PutInt("pouch_silver", CurrentPouch.Silver);
            _editor.PutInt("pouch_copper", CurrentPouch.Copper);
            _editor.Apply();
        }
    }
}