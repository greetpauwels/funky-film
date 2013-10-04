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

namespace Funky_Film.Android.UI 
{
	public class SearchFragment : Fragment
	{
		
		SearchListAdapter adapter;
		MovieList movieList;
		List<Movie> movies = new List<Movie>();
		ListView list;


		public SearchFragment(){
		}



		public override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);
			string query = Const.TestSearch;
			//TODO vervangen door input van gebruiker


			NewSearch(query);

		}

		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			View view = inflater.Inflate (Resource.Layout.SearchFragment, container, false);

			list = (ListView)view.FindViewById (Resource.Id.list);
			adapter = new SearchListAdapter (Activity.ApplicationContext, movies);
			list.Adapter = adapter;

			TextView empty = (TextView)view.FindViewById (Resource.Id.empty);
			list.EmptyView = empty;

			return view;
		}

	private async Task<List<Movie>> RunSearch(string query){

			string url = Const.UrlSearch + query;
			Log.Info ("search url", url);
			movieList = await new SearchResultLoader ().GetSearchResults (url);
		//	Task returnedSearchResults = new SearchResultLoader ().GetSearchResults (url);
		//	movieList = await returnedSearchResults;
			return  movieList.results.OfType<Movie> ().ToList ();

		}

	private async void NewSearch(string query){

			movies = await RunSearch (query);
		//	Task searchResultsAsList = RunSearch ();
		//	movies = await searchResultsAsList;
			adapter.Clear ();
			adapter.AddAll (movies);
			if (movies.Count == 0) {
				Toast.MakeText (Activity.ApplicationContext, Resource.String.no_result, ToastLength.Long).Show ();
			}
		}

	/*class RunSearch:AsyncTask<Java.Lang.String, object, object> {

			string url = "";
			Context context;
			SearchListAdapter adapter;
			MovieList movieList;
			List<Movie> movies;

			public RunSearch(Context context, SearchListAdapter adapter, MovieList movieList, List<Movie> movies){
				this.adapter = adapter;
				this.context = context;
				this.movieList = movieList;
				this.movies = movies;
			}

			protected override object RunInBackground(params Java.Lang.String[] param){
				url = Const.UrlSearch + param[0].ToString ();
				movieList = (MovieList)new SearchResultLoader ().GetSearchResults (url);
				movies = movieList.results.OfType<Movie> ().ToList ();
				//	(List<Movie>)Arrays.AsList (movieList.results);
				return null;
			}

			protected override void OnPostExecute (Java.Lang.Object result){
				adapter.Clear ();
				adapter.AddAll (movies);
				if (movies.Count == 0) {
					Toast.MakeText (context, Resource.String.no_result, ToastLength.Long).Show ();
				}

			}
		}*/

	}


}

