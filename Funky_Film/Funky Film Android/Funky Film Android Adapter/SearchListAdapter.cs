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
using Model = Funky_Film.Model;
using Funky_Film.Tasks;
using Funky_Film.Android.Tasks;
using Android.Util;
using Android.Graphics;

namespace Funky_Film.Android.Adapter
{
	class SearchListAdapter : ArrayAdapter<Model.Movie>
	{
	
		private Context context;
		private List<Model.Movie> movies  = new List<Funky_Film.Model.Movie>();

		public SearchListAdapter(Context context, List<Model.Movie> movies) : base(context, Resource.Layout.Rowlayout, movies){
			this.context = context;
			this.movies = movies;
		}

		public override View GetView (int position, View convertView, ViewGroup parent)
		{
			ViewHolder viewholder;
			Bitmap posterBitmap = null;

			if (convertView == null) {
				LayoutInflater inflater = LayoutInflater.From (context);
				convertView = inflater.Inflate (Resource.Layout.Rowlayout, parent, false);
				viewholder = new ViewHolder ();
				viewholder.titelView = convertView.FindViewById (Resource.Id.title) as TextView;
				viewholder.ratingView = convertView.FindViewById (Resource.Id.rating) as TextView;
				viewholder.thumbView = convertView.FindViewById (Resource.Id.thumbnail) as ImageView;
				convertView.Tag = viewholder;
			} else {
				viewholder = convertView.Tag as ViewHolder;
			}

			Log.Info ("SearchListAdapter", Convert.ToString (movies.Count));

			if (movies.Count != 0) {
				var movie = movies.ElementAt (position);
				viewholder.titelView.Text = movie.Original_title;
				if (movie.Vote_count != 0) {
					viewholder.ratingView.Text = Convert.ToString (movie.Vote_average) + "/10";
				} else {
					viewholder.ratingView.Text = context.Resources.GetString (Resource.String.no_rating);
				}

				if (movie.Poster_path != null) {
					string url = Const.UrlImage92 + movie.Poster_path;
					posterBitmap = new RemoteImageLoaderAndroid ().GetRemoteBitMap (url);
				} 
				if (movie.Poster_path== null |posterBitmap == null) {
					viewholder.thumbView.SetImageDrawable (context.Resources.GetDrawable (Resource.Drawable.default_movie_image));
				} else {
					viewholder.thumbView.SetImageBitmap (posterBitmap);
				}
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

