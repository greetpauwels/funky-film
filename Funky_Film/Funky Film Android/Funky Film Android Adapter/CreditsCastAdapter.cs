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
using Android.Graphics;
using Funky_Film.Model;
using Funky_Film.Android.Tasks;
using Android.Util;

namespace Funky_Film.Android.Adapter
{
	class CreditsCastAdapter: ArrayAdapter<CastCredit>
	{

		private Context context;
		private List<CastCredit> creditsAsCast = new List<CastCredit>();

		public CreditsCastAdapter(Context context, List<CastCredit> creditsAsCast): base(context, Resource.Layout.CrewRowLayout, creditsAsCast){
			this.context = context;
			this.creditsAsCast = creditsAsCast;
		}


		public override View GetView (int position, View convertView, ViewGroup parent)
		{
			ViewHolder viewHolder;
			Bitmap posterBitmap = null;

			if (convertView == null) {
				LayoutInflater inflater = LayoutInflater.From (context);
				convertView = inflater.Inflate (Resource.Layout.CreditsRowLayout, parent, false);
				viewHolder = new ViewHolder();
				viewHolder.titelView = convertView.FindViewById (Resource.Id.credit_title) as TextView;
				viewHolder.roleView = convertView.FindViewById (Resource.Id.credit_role) as TextView;
				viewHolder.thumbView = convertView.FindViewById (Resource.Id.credit_thumbnail) as ImageView;
				convertView.Tag = viewHolder;
			} else {
				viewHolder = convertView.Tag as ViewHolder;
			}

			Log.Info ("CreditCastAdapter", Convert.ToString (creditsAsCast.Count));
			Log.Info ("CreditCastAdapter", (viewHolder==null).ToString ());
			Log.Info ("CreditCastAdapter", (viewHolder.titelView==null).ToString ());

			if (creditsAsCast.Count != 0) {
				var credit = new CastCredit ();
				credit = creditsAsCast.ElementAt (position);

				viewHolder.titelView.Text = credit.Title;
				viewHolder.roleView.Text = credit.Character;

				if (credit.Poster_path != null) {
					string url = Const.UrlImage92 + credit.Poster_path;
					posterBitmap = new RemoteImageLoaderAndroid ().GetRemoteBitMap (url);
				} 

				if (credit.Poster_path== null |posterBitmap == null) {
					viewHolder.thumbView.SetImageDrawable (context.Resources.GetDrawable (Resource.Drawable.default_crew_image));
				} else {
					viewHolder.thumbView.SetImageBitmap (posterBitmap);
				}
			}
			return convertView;
		}

		private class ViewHolder:Java.Lang.Object {
			public TextView titelView;
			public TextView roleView;
			public ImageView thumbView;
		}
	}
}

