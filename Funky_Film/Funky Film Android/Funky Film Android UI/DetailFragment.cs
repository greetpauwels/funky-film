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
using Funky_Film.Model;
using Java.Util.Zip;
using System.Threading.Tasks;
using Funky_Film.Tasks;

namespace Funky_Film
{
	public class DetailFragment : Fragment
	{
		private Movie movie;
		private int movieID;
		private View view; 

		ImageView movie_poster;
		TextView movie_title;
		TextView movie_tagline;
		TextView movie_cnt_overview;
		TextView movie_cnt_releasedate;
		TextView movie_cnt_runtime;
		TextView movie_cnt_status;
		TextView movie_cnt_adult;
		TextView movie_cnt_keywords;
		TextView movie_cnt_genres;


		public override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);
		}

		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState){

			view = inflater.Inflate (Resource.Layout.DetailFragment, container, false);

			movie_poster = (ImageView)view.FindViewById (Resource.Id.detail_poster);
			movie_title = (TextView)view.FindViewById (Resource.Id.detail_title);
			movie_tagline = (TextView) view.FindViewById(Resource.Id.detail_tagline);
			movie_cnt_overview = (TextView)view.FindViewById (Resource.Id.detail_cnt_overview);
			movie_cnt_releasedate = (TextView)view.FindViewById (Resource.Id.detail_cnt_releasedate);
			movie_cnt_runtime = (TextView)view.FindViewById (Resource.Id.detail_cnt_runtime);
			movie_cnt_status = (TextView)view.FindViewById (Resource.Id.detail_cnt_status);
			movie_cnt_adult = (TextView)view.FindViewById (Resource.Id.detail_cnt_adult);
			movie_cnt_keywords = (TextView)view.FindViewById (Resource.Id.detail_cnt_keywords);
			movie_cnt_genres = (TextView)view.FindViewById (Resource.Id.detail_cnt_genres);

			movieID = Arguments.GetInt ("movieId");

			Newdetails ();

			return view;
		}

		private async Task<Movie> LoadDetails(){
			string url = Const.UrlMovie + movieID + Const.ApiKey;
			movie = await new SearchResultLoader().GetMovieDetail (url);
			return movie;
		}

		private async void Newdetails(){
			await LoadDetails ();
		}

		/*private async Task<Movie> NewDetails(){

		}*/
	
	}
}

