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
using System.Net.Mime;
using System.Resources;
using Funky_Film.Android.Util;
using Android;

namespace Funky_Film.Android.UI
{
	public class DetailActorFragment : Fragment
	{

		private Context context;
		private DetailActorAdapter adapter;
		private CallBacks listener;
		private Intent intent;
		private ConnectivityChecker connectionCheck;

		private int movieId;
		private Casting cast;
		private Actor[] actors;

		private string url;

		private View view;
		private ListView movie_cnt_list;
		private LinearLayout emptyLayout;
		private TextView emptyVw;
		private Button reloadBttn;

		public interface CallBacks{
			void OnItemSelected (int actorId, string actorName);
		}

		public override void OnAttach(Activity activity){
			base.OnAttach (activity);
			listener = (CallBacks) activity;
		}

		public override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);
			context = Activity.ApplicationContext;
			connectionCheck = new ConnectivityChecker (context);
			intent = Activity.Intent;
			movieId = intent.GetIntExtra ("movieId", 0);
			// TODO: use String.Format ();
			//url = Const.UrlMovie + movieId + "/casts" + Const.ApiKey;
			url = String.Format ("{0}{1}/casts{2}", Const.UrlMovie, movieId, Const.ApiKey);
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


		private async Task<Casting> RunSearch(){

			Log.Info ("DetailActorFragment", "RunSearchIN" );

			Log.Info ("DetailActorFragment search url", url);

			cast = await new SearchResultLoader ().GetCastDetail (url);

			Log.Info ("DetailActorFragment", "NewSearchOUT" );

			return  cast;

		}

		private async void NewSearch(){

			Log.Info ("DetailActorFragment", "NewSearchIN" );

			Casting castToConvert = await RunSearch ();
			actors = castToConvert.Cast;

			Log.Info ("DetailActorFragment", "NewSearchMID" );
			Log.Info ("DetailActorFragment", " - newSearchMID "+Convert.ToString (actors.Length) ); 

			adapter = new DetailActorAdapter (Activity.ApplicationContext, actors.OfType<Actor> ().ToList ());
			movie_cnt_list.Adapter = adapter;

			Log.Info ("DetailActorFragment", "NewSearchOUT" );
		}

		void OnListItemClick(object sender, AdapterView.ItemClickEventArgs e){
			int itemId = actors [e.Position].Id;
			string itemName = actors [e.Position].Name;
			listener.OnItemSelected (itemId, itemName);
		}
	}
}

