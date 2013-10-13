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

namespace Funky_Film.Android.UI
{
	[Activity]			
	public class PersonActivity : Activity, PersonAsCastFragment.CallBacks
	{
		int actorId;
		

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.PersonActivity);
			Intent intent = Intent;
			actorId = intent.GetIntExtra ("actorId", 0);

			ActionBar actionBar = ActionBar;
			actionBar.NavigationMode = ActionBarNavigationMode.Tabs;

			Fragment personAsCastTab = new PersonAsCastFragment ();
			Fragment personDetailTab = new PersonDetailFragment ();
	
			AddTab (this.Resources.GetString (Resource.String.about), personDetailTab);
			AddTab (this.Resources.GetString (Resource.String.other_cast), personAsCastTab);

		}

		void AddTab (string tabText, Fragment fragment)
		{
			var tab = this.ActionBar.NewTab ();
			tab.SetText (tabText);


			tab.TabSelected += delegate(object sender, ActionBar.TabEventArgs e) {
				e.FragmentTransaction.Add (Resource.Id.person_container, fragment);
			};

			tab.TabReselected += delegate(object sender, ActionBar.TabEventArgs e) {
				e.FragmentTransaction.Detach (fragment);
				e.FragmentTransaction.Attach (fragment);
		};


			tab.TabUnselected += delegate(object sender, ActionBar.TabEventArgs e) {
				e.FragmentTransaction.Remove (fragment);
		};

			this.ActionBar.AddTab (tab);
		}


		public void OnItemSelected(int movieId){
			Intent toDetail = new Intent (this, typeof(DetailActivity));
			toDetail.PutExtra ("movieId", movieId);
			StartActivity (toDetail);
		}
	}
}

