using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.Content.Res;
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
using System.Net.Mime;
using System.Resources;
using Funky_Film.Android.Util;

namespace Funky_Film.Android.UI 
{
	public class SearchFragment : Fragment
	{
		
		Context context;
		Resources res;
		Intent intent;
		SearchListAdapter adapter;
		CallBacks listener;
		ConnectivityChecker connectionCheck;

		MovieList movieList;
		List<Movie> movies = new List<Movie>();

		TextView title;
		ListView list;
		LinearLayout emptyLayout;
		TextView emptyVw;
		Button reloadBttn;

		string query;
		string url;
		string listTitle;
		string emptyString;
		

		public interface CallBacks{
			void OnItemSelected (int movieId, string movieName);
		}

		public SearchFragment(){
		}

		public override void OnAttach(Activity activity){
			base.OnAttach (activity);
			listener = (CallBacks) activity;
		}

		public override void OnDetach(){
			base.OnDetach ();
		}



		public override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			context = Activity.ApplicationContext;
			res = context.Resources;
			connectionCheck = new ConnectivityChecker (context);
			intent = Activity.Intent;

			if (Intent.ActionSearch.Equals (intent.Action)) {
				Log.Info ("SearchFragment", "is search action");
				query = intent.GetStringExtra (SearchManager.Query);
				url = Const.UrlSearch + query;
				listTitle = Resources.GetString (Resource.String.search_fragment_title_search)+" '"+query+"'";
				emptyString = Resources.GetString (Resource.String.empty_search);
			} else {
				url = Const.UrlUpcoming;
				listTitle = res.GetString (Resource.String.search_fragment_title_upcoming);
				emptyString = res.GetString (Resource.String.empty_no_connection);
			}

		}


		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			View view = inflater.Inflate (Resource.Layout.SearchFragment, container, false);

			title = (TextView)view.FindViewById (Resource.Id.list_title);
			list = (ListView)view.FindViewById (Resource.Id.list);
			emptyLayout = (LinearLayout)view.FindViewById (Resource.Id.emptyView);
			emptyVw = (TextView)view.FindViewById (Resource.Id.empty);
			reloadBttn = (Button)view.FindViewById (Resource.Id.reload);

			ProceedByConnectionStatus ();

			emptyVw.Text = emptyString;
			title.Text = listTitle;

			list.ItemClick += OnListItemClick;

			Log.Info ("SearchFragment - onCreateView", Convert.ToString (movies.Count) );

			return view;
		}

	void OnListItemClick(object sender, AdapterView.ItemClickEventArgs e){
			//Movie movie = this.adapter.GetItem (e.Position);
			int itemId = movies.ElementAt (e.Position).Id;
			string itemName = movies.ElementAt (e.Position).Original_title;
			listener.OnItemSelected (itemId, itemName);
		}

	private void ProceedByConnectionStatus(){
			if (connectionCheck.IsConnected()) {
				NewSearch();
			} else {
				Toast.MakeText (Activity.ApplicationContext, "No internet connection",ToastLength.Long).Show ();
				list.EmptyView = emptyLayout;
				reloadBttn.Click += delegate {
						ProceedByConnectionStatus ();
				};
			}
	}

	private async Task<List<Movie>> RunSearch(){

			Log.Info ("SearchFragment", "RunSearchIN" );

			Log.Info ("search url", url);

			movieList = await new SearchResultLoader ().GetSearchResults (url);

			Log.Info ("SearchFragment", "NewSearchOUT" );

			return  movieList.Results.OfType<Movie> ().ToList ();

		}

	private async void NewSearch(){
		
			Log.Info ("SearchFragment", "NewSearchIN" );

			movies = await RunSearch ();

			Log.Info ("SearchFragment", "NewSearchMID" );
			Log.Info ("SearchFragment", " - newSearchMID "+Convert.ToString (movies.Count) ); 

			adapter = new SearchListAdapter (Activity.ApplicationContext, movies);
			list.Adapter = adapter;

			if (movies.Count == 0) {
				Toast.MakeText (Activity.ApplicationContext, Resource.String.no_result, ToastLength.Long).Show ();
				list.EmptyView = emptyLayout;
				reloadBttn.Visibility = ViewStates.Invisible;
			}

			Log.Info ("SearchFragment", "NewSearchOUT" );
		}
		
}
}

