using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Funky_Film.Model;
using Funky_Film.Tasks;
using Funky_Film.Android.Tasks;
using System.Threading.Tasks;
using Funky_Film.Android.Util;
using System.Resources;

namespace Funky_Film
{
	public class DetailFragment : Fragment
	{
		Intent intent; 
		Context context;
		Resources res;
		ConnectivityChecker connectionCheck;

		Movie movie = new Movie();
		Cast cast;
		int movieId;

		View view;
		LinearLayout emptyLayout;
		ImageView movie_poster;
		TextView movie_title;
		TextView movie_tagline;
		TextView movie_overview;
		TextView movie_cnt_overview;
		TextView movie_releasedate;
		TextView movie_cnt_releasedate;
		TextView movie_runtime;
		TextView movie_cnt_runtime;
		TextView movie_status;
		TextView movie_cnt_status;
		Button reloadBttn;

		public override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			intent = Activity.Intent;
			movieId = intent.GetIntExtra ("movieId", 0);
			context = Activity.ApplicationContext;
			res = context.Resources;
			connectionCheck = new ConnectivityChecker (context);

		}

		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{

			view = inflater.Inflate (Resource.Layout.DetailFragment, container, false);

			movie_poster = (ImageView) view.FindViewById (Resource.Id.detail_poster);
			movie_title = (TextView) view.FindViewById (Resource.Id.detail_title);
			movie_tagline = (TextView) view.FindViewById(Resource.Id.detail_tagline);
			movie_overview = (TextView)view.FindViewById (Resource.Id.detail_overview);
			movie_cnt_overview = (TextView) view.FindViewById (Resource.Id.detail_cnt_overview);
			movie_releasedate= (TextView) view.FindViewById (Resource.Id.detail_releasedate);
			movie_cnt_releasedate = (TextView) view.FindViewById (Resource.Id.detail_cnt_releasedate);
			movie_runtime=(TextView) view.FindViewById (Resource.Id.detail_runtime);
			movie_cnt_runtime = (TextView) view.FindViewById (Resource.Id.detail_cnt_runtime);
			movie_status=(TextView) view.FindViewById (Resource.Id.detail_status);
			movie_cnt_status = (TextView) view.FindViewById (Resource.Id.detail_cnt_status);
			reloadBttn = (Button)view.FindViewById (Resource.Id.reload);
			emptyLayout = (LinearLayout)view.FindViewById (Resource.Id.emptyView);

			ProceedByConnectionStatus ();

			return view;

		}

		private void ProceedByConnectionStatus(){
			if (connectionCheck.IsConnected()) {
				reloadBttn.Visibility = ViewStates.Invisible;
				emptyLayout.Visibility = ViewStates.Invisible;
				Newdetails();
			} else {
				Toast.MakeText (Activity.ApplicationContext, "No internet connection",ToastLength.Long).Show ();

				reloadBttn.Click += delegate {
					ProceedByConnectionStatus ();
				};
			}
		}


		private async Task<Movie> LoadDetails(){

			string url = Const.UrlMovie + movieId + Const.ApiKey;
			movie = await new SearchResultLoader().GetMovieDetail (url);

			return movie;
		}

		private async void Newdetails(){
			await LoadDetails ();

			movie_title.Text = movie.original_title;
			movie_tagline.Text = movie.tagline;
			movie_overview.Text = res.GetString (Resource.String.overview);
			movie_cnt_overview.Text = movie.overview;
			movie_releasedate.Text = res.GetString (Resource.String.release_date);
			movie_cnt_releasedate.Text = movie.release_date;
			movie_runtime.Text=res.GetString (Resource.String.runtime);
			movie_cnt_runtime.Text =Convert.ToString (movie.runtime);
			movie_status.Text=res.GetString (Resource.String.status);
			movie_cnt_status.Text = movie.status;

			if (movie.poster_path != null) {
				string url = Const.UrlImage154 + movie.poster_path;
				movie_poster.SetImageBitmap (new RemoteImageLoaderAndroid ().GetRemoteBitMap (url));
			} else {
				movie_poster.SetImageDrawable(res.GetDrawable (Resource.Drawable.default_movie_image));
			}

		}

	}
}

