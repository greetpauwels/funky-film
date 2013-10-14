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
using Android.Graphics;

namespace Funky_Film.Android.UI 
{
	[Activity (MainLauncher=true)]
	[IntentFilter(new string[]{"android.intent.action.SEARCH"})]
	[MetaData(("android.app.searchable"), Resource = "@xml/searchable")]

	public class SearchActivity : Activity, SearchFragment.CallBacks
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.SearchActivity);
		}


		public override bool OnCreateOptionsMenu(IMenu menu) {
			// Inflate the menu; this adds items to the action bar if it is present.
			MenuInflater.Inflate(Resource.Menu.search, menu);
			SearchManager searchManager = (SearchManager) GetSystemService(Context.SearchService);
			SearchView searchView = (SearchView) menu.FindItem(Resource.Id.action_search).ActionView;
			// Assumes current activity is the searchable activity
			searchView.SetSearchableInfo(searchManager.GetSearchableInfo(ComponentName));
			searchView.SetIconifiedByDefault(false); // Do not iconify the widget; expand it by default
			return true;
		}

		public void OnItemSelected(int movieId, string movieName){
			var toDetail = new Intent (this, typeof(DetailActivity));
			toDetail.PutExtra ("movieId", movieId);
			toDetail.PutExtra ("movieName", movieName);
			StartActivity (toDetail);
		}
	}
}

