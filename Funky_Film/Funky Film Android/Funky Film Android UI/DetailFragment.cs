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
using Funky_Film.Model;
using Funky_Film.Tasks;
using Funky_Film.Android.Tasks;
using System.Threading.Tasks;

namespace Funky_Film
{
	public class DetailFragment : Fragment
	{
		int movieId;
		Intent intent; 
		Movie movie = new Movie();
		Cast cast;

		private View view;

		ImageView movie_poster;
		TextView movie_title;
		TextView movie_tagline;
		TextView movie_cnt_overview;
		TextView movie_cnt_releasedate;
		TextView movie_cnt_runtime;
		TextView movie_cnt_status;
		//	TextView movie_cnt_adult;
		//	TextView movie_cnt_keywords;
		//	TextView movie_cnt_genres;

		public override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

		}

		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{

			view = inflater.Inflate (Resource.Layout.DetailFragment, container, false);

			intent = Activity.Intent;

			movieId = intent.GetIntExtra ("movieId", 0);

			movie_poster = (ImageView) view.FindViewById (Resource.Id.detail_poster);
			movie_title = (TextView) view.FindViewById (Resource.Id.detail_title);
			movie_tagline = (TextView) view.FindViewById(Resource.Id.detail_tagline);
			movie_cnt_overview = (TextView) view.FindViewById (Resource.Id.detail_cnt_overview);
			movie_cnt_releasedate = (TextView) view.FindViewById (Resource.Id.detail_cnt_releasedate);
			movie_cnt_runtime = (TextView) view.FindViewById (Resource.Id.detail_cnt_runtime);
			movie_cnt_status = (TextView) view.FindViewById (Resource.Id.detail_cnt_status);
			//			movie_cnt_adult = (TextView) FindViewById (Resource.Id.detail_cnt_adult);
			//movie_cnt_keywords = FindViewById (Resource.Id.detail_cnt_keywords);
			//movie_cnt_genres = FindViewById (Resource.Id.detail_cnt_genres);

			Newdetails ();
			NewCastDetails ();

			return view;

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
			movie_cnt_overview.Text = movie.overview;
			movie_cnt_releasedate.Text = movie.release_date;
			movie_cnt_runtime.Text =Convert.ToString (movie.runtime);
			movie_cnt_status.Text = movie.status;
			//			movie_cnt_adult.Text = movie.adult;

			if (movie.poster_path != null) {
				string url = Const.UrlImage154 + movie.poster_path;
				movie_poster.SetImageBitmap (new RemoteImageLoaderAndroid ().GetRemoteBitMap (url));
			} 

		}


		private async Task<Cast> LoadCastDetails(){
			string urlCast = Const.UrlMovie + movieId + "/casts" + Const.ApiKey;
			cast = await new SearchResultLoader ().GetCastDetail (urlCast);
			return cast;
		}

		private async void NewCastDetails(){
			await LoadCastDetails ();

			movie.cast = cast.cast;
			movie.crew = cast.crew;
		}


	}
}

