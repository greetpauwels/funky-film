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
				viewholder.tagView = (TextView)convertView.FindViewById (Resource.Id.tagline);
				viewholder.thumbView = (ImageView)convertView.FindViewById (Resource.Id.thumbnail);
				convertView.SetTag (viewholder);
			} else {
				viewholder = (ViewHolder)convertView.GetTag ();
			}

			Movie movie = movies.ElementAt (position);
			viewholder.titelView.SetText (movie.original_title);
			viewholder.tagView.SetText (movie.tagline);
			string url = Const.UrlImage92 + movie.poster_path;
			viewholder.thumbView.SetImageBitmap(new RemoteImageLoaderAndroid().GetRemoteBitMap (url));

			return convertView;
		}



		class ViewHolder {
			public TextView titelView;
			public TextView tagView;
			public ImageView thumbView;
		}


	}
}

