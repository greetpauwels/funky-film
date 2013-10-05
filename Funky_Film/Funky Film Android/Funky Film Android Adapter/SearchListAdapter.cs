using System;
using System.Collections;
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
using Android.Util;

namespace Funky_Film.Android.Adapter 
{
	class SearchListAdapter : ArrayAdapter<Movie>
	{
	
		private Context context;
		private List<Movie> movies  = new List<Movie>();

		public SearchListAdapter(Context context, List<Movie> movies) : base(context, Resource.Layout.Rowlayout, movies){
			this.context = context;
			this.movies = movies;
		}

		public override View GetView (int position, View convertView, ViewGroup parent)
		{

			ViewHolder viewholder;

			if (convertView == null) {
				LayoutInflater inflater = LayoutInflater.From (context);
				convertView = inflater.Inflate (Resource.Layout.Rowlayout, parent, false);
				viewholder = new ViewHolder ();
				viewholder.titelView = (TextView)convertView.FindViewById (Resource.Id.title);
				viewholder.ratingView = (TextView)convertView.FindViewById (Resource.Id.rating);
				viewholder.thumbView = (ImageView)convertView.FindViewById (Resource.Id.thumbnail);
				convertView.Tag = viewholder;
			} else {
				viewholder = (ViewHolder)convertView.Tag;
			}
			int sizeList = movies.Count; 
			Log.Info ("SearchListAdapter", Convert.ToString (sizeList));

			if (movies.Count != 0) {
				Movie movie = movies.ElementAt (position);
				viewholder.titelView.Text = movie.original_title;
				viewholder.titelView.Text = movie.original_title;
				viewholder.ratingView.Text = Convert.ToString (movie.vote_average)+"/10";
				string url = Const.UrlImage92 + movie.poster_path;
				viewholder.thumbView.SetImageBitmap (new RemoteImageLoaderAndroid ().GetRemoteBitMap (url));
			}
			return convertView;
		}



		class ViewHolder:Java.Lang.Object {
			public TextView titelView;
			public TextView ratingView;
			public ImageView thumbView;
		}


	}
}

