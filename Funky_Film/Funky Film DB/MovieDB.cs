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
using Android.Database.Sqlite;

namespace Funky_Film.DB
{
	public class MovieDB :SQLiteOpenHelper
	{

		public static readonly string DatabaseName = "movies.db";
		public static readonly int DatabaseVersion = 1;
		
		private static readonly String create_table_movies = 
			"create table [movies]( "
				+ "[_id] INTEGER PRIMARY KEY AUTOINCREMENT, "
				+ "[movieid] INTEGER NOT NULL UNIQUE, "
				+ "[backdrop] TEXT, "
				+ "[poster] TEXT, "
				+ "[title] TEXT, "
				+ "[tagline] TEXT, "
				+ "[release] TEXT, "
				+ "[runtime] TEXT, "
				+ "[status] TEXT, "
				+ "[overview] TEXT );";

		public MovieDB(Context context): base(context, DatabaseName, null, DatabaseVersion) { }

		public override void OnCreate(SQLiteDatabase db)
		{
			db.ExecSQL (create_table_movies);
		}

		public override void OnUpgrade(SQLiteDatabase db, int oldVersion, int newVersion)
		{   // not required for this example
			throw new NotImplementedException();
		}
	}
}

