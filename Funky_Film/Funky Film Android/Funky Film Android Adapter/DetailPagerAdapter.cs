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
using Android.Support.V13.App;


namespace Funky_Film
{
	class DetailPagerAdapter : FragmentPagerAdapter
	{
		const int NumberOfItems = 2;

		private int movieId;

		public DetailPagerAdapter(FragmentManager fm, int movieId) :  base(fm){
			this.movieId = movieId;
		}

		public override Fragment GetItem(int position){
			Bundle arguments = new Bundle ();

			arguments.PutInt ("movieId", movieId);

			switch (position) {

			case 0:
				Fragment fragment_movie = new DetailFragment ();
				fragment_movie.Arguments = arguments;
				return fragment_movie;
			case 1:
				Fragment fragment_actor = new DetailFragment ();
				fragment_actor.Arguments = arguments;
				return fragment_actor;

			}

			return null;
		}

		public override int Count {
			get {
				return NumberOfItems;
			}
		}

	}
}
