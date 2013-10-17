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
using Android.Util;
using Android.Graphics;
using Funky_Film.Android.Tasks;

namespace Funky_Film.Android.Adapter
{
	class DetailActorAdapter : ArrayAdapter<Actor>
	{

		private Context context;
		private List<Actor> actors =  new List<Actor>();

		public DetailActorAdapter (Context context, List<Actor> actors) : base(context, Resource.Layout.ActorRowLayout, actors){
			this.context = context;
			this.actors = actors;
		}

		public override View GetView (int position, View convertView, ViewGroup parent)
		{
			ViewHolder viewHolder;
			Bitmap posterBitmap = null;

			if (convertView == null) {
				LayoutInflater inflater = LayoutInflater.From (context);
				convertView = inflater.Inflate (Resource.Layout.ActorRowLayout, parent, false);
				viewHolder = new ViewHolder();
				viewHolder.nameView = (TextView)convertView.FindViewById (Resource.Id.actor_name);
				viewHolder.roleView = (TextView)convertView.FindViewById (Resource.Id.actor_role);
				viewHolder.posterView = (ImageView)convertView.FindViewById (Resource.Id.actor_thumbnail);
				convertView.Tag = viewHolder;
			} else {
				// TODO use as operator instead of (cast)
				// as will return null / casting will throw an error if it cannot be cast.
				viewHolder = (ViewHolder)convertView.Tag;
			}

			Log.Info ("DetailActorAdapter", Convert.ToString (actors.Count));

			if (actors.Count != 0) {
				// TODO: implicit typing
				// var actor = actors.ElementAt (position);
				Actor actor = actors.ElementAt (position);
				viewHolder.nameView.Text = actor.Name;
				viewHolder.roleView.Text = actor.Character;

				if (actor.Profile_path != null) {
					// TODO: use String.Format () for formatting strings
					string url = Const.UrlImage92 + actor.Profile_path;
					posterBitmap = new RemoteImageLoaderAndroid ().GetRemoteBitMap (url);
				}
				if (actor.Profile_path == null | posterBitmap == null) {
					viewHolder.posterView.SetImageDrawable (context.Resources.GetDrawable (Resource.Drawable.default_actor_image));
				} else {
					viewHolder.posterView.SetImageBitmap (posterBitmap);
				}
			}
			return convertView;

		}
		// TODO: clean up whitespace :)

		class ViewHolder:Java.Lang.Object {
			public TextView nameView;
			public TextView roleView;
			public ImageView posterView;
		}


	}
}

