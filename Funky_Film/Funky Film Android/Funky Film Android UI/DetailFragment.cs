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
using Funky_Film.Util;
using System.Resources;

namespace Funky_Film.Android.UI
{
	public class DetailFragment : Fragment
	{
		private Intent intent; 
		private Context context;
		private Resources res;
		private ConnectivityChecker connectionCheck;

		private Movie movie = new Movie();
		private int movieId;

		private View view;
		private LinearLayout emptyLayout;
		private ImageView movie_poster;
		private TextView movie_title;
		private TextView movie_tagline;
		private TextView movie_rating;
		private TextView movie_cnt_rating;
		private TextView movie_cnt_overview;
		private TextView movie_releasedate;
		private TextView movie_cnt_releasedate;
		private TextView movie_runtime;
		private TextView movie_cnt_runtime;
		private TextView movie_status;
		private TextView movie_cnt_status;
		private Button reloadBttn;
		

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
			movie_rating = (TextView)view.FindViewById (Resource.Id.detail_rating);
			movie_cnt_rating = (TextView)view.FindViewById (Resource.Id.detail_cnt_rating);
			movie_tagline = (TextView) view.FindViewById(Resource.Id.detail_tagline);
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
			//string url = Const.UrlMovie + movieId + Const.ApiKey;
			var url = String.Format ("{0}{1}{2}",Const.UrlMovie, movieId, Const.ApiKey);
			movie = await new SearchResultLoader().GetMovieDetail (url);
			return movie;
		}

		private async void Newdetails(){
			await LoadDetails ();

			movie_title.Text = movie.Original_title;

			if (movie.Tagline != null && movie.Tagline!="") {
				movie_tagline.Text = movie.Tagline;
			} else {
				movie_tagline.Visibility = ViewStates.Gone;
			}

			if (movie.Vote_count != 0) {
				movie_cnt_rating.Text = Convert.ToString (movie.Vote_average);
				movie_rating.Text = res.GetString (Resource.String.rating);
			} else {
				movie_cnt_rating.Visibility = ViewStates.Gone;
				movie_rating.Visibility = ViewStates.Gone;
			}

			movie_cnt_overview.Text = movie.Overview;

			movie_releasedate.Text = res.GetString (Resource.String.release_date);
			movie_cnt_releasedate.Text = UIUtil.ConvertDateToEuropean (movie.Release_date);

			if (movie.Runtime != 0) {
				movie_runtime.Text = res.GetString (Resource.String.minutes);
				movie_cnt_runtime.Text = Convert.ToString (movie.Runtime);
			} else {
				movie_runtime.Visibility = ViewStates.Gone;
				movie_cnt_runtime.Visibility = ViewStates.Gone;
			}

			movie_status.Text=res.GetString (Resource.String.status);
			movie_cnt_status.Text = movie.Status;

			if (movie.Poster_path != null) {
				//string url = Const.UrlImage154 + movie.Poster_path;
				var url = String.Format ("{0}{1}", Const.UrlImage154, movie.Poster_path);
				movie_poster.SetImageBitmap (new RemoteImageLoaderAndroid ().GetRemoteBitMap (url));
			} else {
				movie_poster.SetImageDrawable(res.GetDrawable (Resource.Drawable.default_movie_image));
			}
		}
	}
}

