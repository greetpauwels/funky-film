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
using Android.App;
using Android.AccessibilityServices;

namespace Funky_Film
{

	public class PopulateDB
	{
		private Funky_Film.Model.Movie movie;
		private ContentResolver contentResolver;

		public PopulateDB(Funky_Film.Model.Movie movie, ContentResolver contentResolver){
			this.movie = movie;
			this.contentResolver = contentResolver;
		}

		public bool execute(){
			ContentValues values = new ContentValues ();

			values.Put ("title", movie.Original_title );
			values.Put ("backdrop", movie.Backdrop_path );
			values.Put ("movieid", movie.Id );
			values.Put ("overview", movie.Overview );
			values.Put ("poster", movie.Poster_path );
			values.Put ("release", movie.Release_date );
			values.Put ("runtime", movie.Runtime );
			values.Put ("status", movie.Status );
			values.Put ("tagline", movie.Tagline );

			contentResolver.Insert (MovieContentProvider.CONTENT_URI, values);

			return true;
		}

	}
}

