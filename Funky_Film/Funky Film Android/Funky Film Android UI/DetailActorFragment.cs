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

namespace Funky_Film
{
	public class DetailActorFragment : Fragment
	{

		DetailActorAdapter adapter;

		Intent intent;

		private int movieId;
		private View view;

		ListView movie_cnt_list;

		Cast cast;

		string url;

		public override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);
			movieId = intent.GetIntExtra ("movieId", 0);
			url = Const.UrlMovie + movieId + "/cast" + Const.ApiKey;
			// Create your fragment here
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState){

			view = inflater.Inflate (Resource.Layout.CrewFragment, container, false);

			NewSearch ();

			return view;
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
			Actor[] actors = castToConvert.cast;

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

	}
}

