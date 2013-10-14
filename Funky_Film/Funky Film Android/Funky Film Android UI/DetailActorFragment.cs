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
using System.Net.Mime;
using System.Resources;
using Funky_Film.Android.Util;

namespace Funky_Film
{
	public class DetailActorFragment : Fragment
	{

		Context context;
		DetailActorAdapter adapter;
		CallBacks listener;
		Intent intent;
		ConnectivityChecker connectionCheck;

		private int movieId;
		Cast cast;
		Actor[] actors;

		string url;

		private View view;
		ListView movie_cnt_list;
		LinearLayout emptyLayout;
		TextView emptyVw;
		Button reloadBttn;




		public interface CallBacks{
			void OnItemSelected (int actorId);
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
			connectionCheck = new ConnectivityChecker (context);
			intent = Activity.Intent;
			movieId = intent.GetIntExtra ("movieId", 0);
			url = Const.UrlMovie + movieId + "/casts" + Const.ApiKey;
			// Create your fragment here
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState){

			view = inflater.Inflate (Resource.Layout.CrewFragment, container, false);
			movie_cnt_list = (ListView)view.FindViewById (Resource.Id.add_cnt_list);
			emptyLayout = (LinearLayout)view.FindViewById (Resource.Id.emptyView);
			emptyVw = (TextView)view.FindViewById (Resource.Id.empty);
			reloadBttn = (Button)view.FindViewById (Resource.Id.reload);

			movie_cnt_list.ItemClick += OnListItemClick;

			ProceedByConnectionStatus ();

			return view;
		}

		private void ProceedByConnectionStatus(){
			if (connectionCheck.IsConnected()) {
				NewSearch();
			} else {
				Toast.MakeText (Activity.ApplicationContext, "No internet connection",ToastLength.Long).Show ();
				movie_cnt_list.EmptyView = emptyLayout;
				reloadBttn.Click += delegate {
					ProceedByConnectionStatus ();
				};
			}
		}


		private async Task<Cast> RunSearch(){

			Log.Info ("DetailActorFragment", "RunSearchIN" );

			Log.Info ("DetailActorFragment search url", url);

			cast = await new SearchResultLoader ().GetCastDetail (url);

			Log.Info ("DetailActorFragment", "NewSearchOUT" );

			return  cast;

		}

		private async void NewSearch(){

			Log.Info ("DetailActorFragment", "NewSearchIN" );

			Cast castToConvert = await RunSearch ();
			actors = castToConvert.cast;

			//	Task searchResultsAsList = RunSearch ();
			//	movies = await searchResultsAsList;
			Log.Info ("DetailActorFragment", "NewSearchMID" );
			Log.Info ("DetailActorFragment", " - newSearchMID "+Convert.ToString (actors.Length) ); 

			adapter = new DetailActorAdapter (Activity.ApplicationContext, actors.OfType<Actor> ().ToList ());
			movie_cnt_list.Adapter = adapter;


			/*if (actors.Count == 0) {
				Toast.MakeText (Activity.ApplicationContext, Resource.String.no_result, ToastLength.Long).Show ();
			}*/

			Log.Info ("DetailActorFragment", "NewSearchOUT" );
		}

		void OnListItemClick(object sender, AdapterView.ItemClickEventArgs e){
			int itemId = actors [e.Position].id;
			listener.OnItemSelected (itemId);
		}

	}
}

