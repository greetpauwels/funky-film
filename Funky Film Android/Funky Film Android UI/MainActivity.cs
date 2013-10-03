using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Funky_Film.Android.UI;

namespace Funky_Film.Android.Ui
{
	[Activity (Label = "MainActivity", MainLauncher=true)]		

	public class MainActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.Main);
			Button button = FindViewById<Button> (Resource.Id.myButton);

			button.Click += (sender, e) => {
				StartActivity (typeof(SearchActivity));
			};
		}
	}
}

