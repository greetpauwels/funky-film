using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Content.Res;
using Android.Views;
using Android.Widget;
using Funky_Film.Android.Adapter;
using Java.Util.Zip;
using Funky_Film.Model;
using System.Threading.Tasks;
using Funky_Film.Tasks;
using Funky_Film.Android.Util;
using System.Resources;

namespace Funky_Film.Android.UI
{
	public class PersonAsCastFragment : Fragment
	{

		Intent intent;
		Context context;
		Resources res;
		ConnectivityChecker connectionCheck;

		CreditsCastAdapter adapter;
		CallBacks listener;

		Credits credits;
		List<CastCredit> creditsAsCast;

		View view;
		ListView listView;
		TextView title;
		LinearLayout emptyLayout;
		Button reloadBttn;

		int actorId;
		string url;
	
		public interface CallBacks{
			void OnItemSelected (int movieId, string movieName);
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
			actorId = intent.GetIntExtra ("personId", 0);

			url = Const.UrlBase+"person/"+actorId+"/credits"+Const.ApiKey;

			Log.Info ("PersonAsCast", "Person Credits RL= " + url);

		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState){
			view = inflater.Inflate (Resource.Layout.SearchFragment, container, false);

			var viewToHide = view.FindViewById(Resource.Id.separator) as View;
			viewToHide.Visibility = ViewStates.Gone;

			emptyLayout = view.FindViewById (Resource.Id.emptyView) as LinearLayout;
			reloadBttn = view.FindViewById (Resource.Id.reload) as Button;
			title = view.FindViewById (Resource.Id.list_title) as TextView;
			title.Visibility = ViewStates.Gone;

			listView = view.FindViewById (Resource.Id.list) as ListView;
			listView.ItemClick += OnListItemClick;

			ProceedByConnectionStatus ();

			return view;
		}

		private void ProceedByConnectionStatus(){
			if (connectionCheck.IsConnected()) {
				NewCreditsAsCast();
			} else {
				Toast.MakeText (context, "No internet connection",ToastLength.Long).Show ();
				listView.EmptyView = emptyLayout;
				reloadBttn.Click += delegate {
					ProceedByConnectionStatus ();
				};
			}
		}
		
		private async Task<Credits> loadCreditsAsCast(){
			Log.Info ("PersonAsCastFragment", "loadCreditsAsCastIN" );

			Log.Info ("PersonAsCastFragment url", url);

			credits = await new SearchResultLoader ().GetPersonCredits (url);

			Log.Info ("PersonAsCastFragment", "loadCreditsAsCastOUT" );
			return credits;
		}

		private async void NewCreditsAsCast(){
			Log.Info ("PersonAsCastFragment", "NewCreditsAsCastIN" );
			Credits creditsToConvert = await loadCreditsAsCast ();

			creditsAsCast = creditsToConvert.Cast;

			Log.Info ("PersonAsCastFragment", Convert.ToString (creditsAsCast.Count));


			adapter = new CreditsCastAdapter (context, creditsAsCast);
			listView.Adapter = adapter;

			Log.Info ("PersonAsCastFragment", "NewCreditsAsCastOUT" );
		}
		
		void OnListItemClick(object sender, AdapterView.ItemClickEventArgs e){
			int itemId = creditsAsCast.ElementAt (e.Position).Id;
			string itemName = creditsAsCast.ElementAt (e.Position).Original_title;
			listener.OnItemSelected (itemId, itemName);
		}
	}
}

