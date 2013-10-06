using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Funky_Film.Android.Adapter;
using Funky_Film.Model;
using Funky_Film.Tasks;
using Java.Util;
using Org.Apache.Http.Util;
using System.Threading.Tasks;
using Java.Lang;

namespace Funky_Film.Android.UI 
{
	public class SearchFragment : Fragment
	{
		
		Intent intent;
		SearchListAdapter adapter;
		MovieList movieList;
		List<Movie> movies = new List<Movie>();
		ListView list;
		string query;
		CallBacks listener;

		public interface CallBacks{
			void OnItemSelected (int movieId);
		}

		public SearchFragment(){
		}

		public override void OnAttach(Activity activity){
			base.OnAttach (activity);

			/*if(!(activity.GetType () == typeof( CallBacks))){
				throw new IllegalArgumentException("Activity should implement Callback");
			}*/

			listener = (CallBacks) activity;
		}

		public override void OnDetach(){
			base.OnDetach ();
		}


		public override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			intent = Activity.Intent;
			if (Intent.ActionSearch.Equals (intent.Action)) {
				query = intent.GetStringExtra (SearchManager.Query);
			} else {
				query = Const.TestSearch;
				//TODO vervangen door input van gebruiker
			}

		}

		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			View view = inflater.Inflate (Resource.Layout.SearchFragment, container, false);
			//TODO Connection check to prevent crashes
			if (query != null) {
				NewSearch();

			}

			list = (ListView)view.FindViewById (Resource.Id.list);
			Log.Info ("SearchFragment - onCreateView", Convert.ToString (movies.Count) );

			TextView empty = (TextView)view.FindViewById (Resource.Id.empty);
			list.EmptyView = empty;

			list.ItemClick += OnListItemClick;

			return view;
		}

		void OnListItemClick(object sender, AdapterView.ItemClickEventArgs e){
			//Movie movie = this.adapter.GetItem (e.Position);
			int itemId = movies.ElementAt (e.Position).id;
			listener.OnItemSelected (itemId);
		}

	private async Task<List<Movie>> RunSearch(){

			Log.Info ("SearchFragment", "RunSearchIN" );

			string url = Const.UrlSearch + query;

			Log.Info ("search url", url);

			movieList = await new SearchResultLoader ().GetSearchResults (url);

			Log.Info ("SearchFragment", "NewSearchOUT" );

			return  movieList.results.OfType<Movie> ().ToList ();

		}

	private async void NewSearch(){
		
			Log.Info ("SearchFragment", "NewSearchIN" );

			movies = await RunSearch ();
		//	Task searchResultsAsList = RunSearch ();
		//	movies = await searchResultsAsList;
			Log.Info ("SearchFragment", "NewSearchMID" );
			Log.Info ("SearchFragment", " - newSearchMID "+Convert.ToString (movies.Count) ); 

			adapter = new SearchListAdapter (Activity.ApplicationContext, movies);
			list.Adapter = adapter;


			if (movies.Count == 0) {
				Toast.MakeText (Activity.ApplicationContext, Resource.String.no_result, ToastLength.Long).Show ();
			}

			Log.Info ("SearchFragment", "NewSearchOUT" );
		}
		

	}


}

