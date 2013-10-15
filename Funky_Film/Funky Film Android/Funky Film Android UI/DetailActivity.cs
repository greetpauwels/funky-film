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
using Funky_Film.Model;
using Funky_Film.Tasks;
using Funky_Film.Android.Tasks;
using System.Threading.Tasks;
using Android;
using Funky_Film.Android.UI;


namespace Funky_Film
{
	[Activity]	
			
	public class DetailActivity : Activity, DetailActorFragment.CallBacks
	{

		int movieId;
		string movieName;

		Movie movie = new Movie();

		//private DetailPagerAdapter detailPagerAdapter;
		//private ViewPager viewPager;



		protected override void OnCreate (Bundle savedInstanceState)
		{
		base.OnCreate (savedInstanceState);
			Intent intent = Intent;
			movieId = intent.GetIntExtra ("movieId", 0);
			movieName = intent.GetStringExtra ("movieName");
			this.Title = movieName;
			ActionBar.SetDisplayHomeAsUpEnabled (true);
			ActionBar.SetHomeButtonEnabled (true);
			SetContentView (Resource.Layout.DetailActivity);




		//	detailPagerAdapter = new DetailPagerAdapter (FragmentManager, movieId);
		//	viewPager = (ViewPager)FindViewById (Resource.Id.movie_container);
		//	viewPager.Adapter = detailPagerAdapter;
		//	viewPager.SetOnPageChangeListener (this);


			ActionBar actionBar = ActionBar;
			actionBar.NavigationMode = ActionBarNavigationMode.Tabs;
			//actionBar.DisplayOptions = ActionBarDisplayOptions.ShowTitle;
			//actionBar.SetDisplayHomeAsUpEnabled(true);
			//actionBar.SetDisplayShowTitleEnabled (true);

			Fragment movieTab = new DetailFragment ();
			Fragment actorTab = new DetailActorFragment ();

			AddTab (this.Resources.GetString (Resource.String.about), movieTab);
			AddTab (this.Resources.GetString (Resource.String.cast_list), actorTab);

		}

		void AddTab (string tabText, Fragment fragment)
		{
			var tab = this.ActionBar.NewTab ();
			tab.SetText (tabText);


			tab.TabSelected += delegate(object sender, ActionBar.TabEventArgs e) {
				e.FragmentTransaction.Add (Resource.Id.movie_container, fragment);
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

		public void OnItemSelected(int actorId, string actorName){
			Intent toPerson = new Intent (this, typeof(PersonActivity));
			toPerson.PutExtra ("personId", actorId);
			toPerson.PutExtra ("personName", actorName);
			StartActivity (toPerson);
		}

		public override bool OnOptionsItemSelected (IMenuItem item){
			StartActivity (typeof(SearchActivity));
			return true;
		}


	}

}
