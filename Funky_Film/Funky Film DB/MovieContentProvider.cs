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
using Funky_Film.DB;
using Java.Util.Regex;
using Android.Database;
using Android.Database.Sqlite;



namespace Funky_Film
{
	[ContentProvider(new string[]{"be.funky_film.db.MovieContentProvider"})]
	public class MovieContentProvider: ContentProvider
	{
		MovieDB movieDB;

		public override bool OnCreate(){
			movieDB = new MovieDB (Context);
			return true;
		}

		public static readonly string AUTHORITY = "be.funky_film.db.MovieContentProvider";
		static string BASE_PATH = "movies";
		public static readonly global::Android.Net.Uri CONTENT_URI = global::Android.Net.Uri.Parse("content://" + AUTHORITY + "/" + BASE_PATH);

		public static readonly String MOVIES_MIME_TYPE = ContentResolver.CursorDirBaseType + "/be.funky_film.db.Movies";
		public static readonly String MOVIE_MIME_TYPE = ContentResolver.CursorItemBaseType + "/be.funky_film.db.Movies";

		public static class InterfaceConsts{
			public static readonly String Table="movies";
			public static readonly String Id="_id";
			public static readonly String MovieId="movieid";
			public static readonly String BackDrop="backdrop";
			public static readonly String Poster="poster";
			public static readonly String Title="title";
			public static readonly String Tagline="tagline";
			public static readonly String Overview="overview";
			public static readonly String Release="release";
			public static readonly String Runtime="runtime";
			public static readonly String Status="status";		
		}

		const int GET_ALL = 0;
		const int GET_ONE = 1;
		static UriMatcher uriMatcher = BuildUriMatcher();

		static UriMatcher BuildUriMatcher(){
			var matcher = new UriMatcher (UriMatcher.NoMatch);
			matcher.AddURI (AUTHORITY, BASE_PATH, GET_ALL);
			matcher.AddURI (AUTHORITY, BASE_PATH + "/#", GET_ONE);
			return matcher;
		}

		public override global::Android.Database.ICursor Query(global::Android.Net.Uri uri , string[] projection, string selection, string[] selectionArgs, string sortOrder){
			SQLiteQueryBuilder queryBuilder = new SQLiteQueryBuilder();
			switch (uriMatcher.Match(uri)) {
			case GET_ALL:
				queryBuilder.Tables = "movies";
				break;
			case GET_ONE:
				queryBuilder.Tables = "movies";
				queryBuilder.AppendWhere ("_id" + "=" + uri.LastPathSegment);
				break;
			default:
				throw new Java.Lang.IllegalArgumentException("Unknown Uri: " + uri);
			}
			global::Android.Database.ICursor cursor = queryBuilder.Query (movieDB.ReadableDatabase, projection, selection, selectionArgs, null, null, sortOrder);
			cursor.SetNotificationUri (Context.ContentResolver, uri);
			return cursor;
		}

		public override int Delete(global::Android.Net.Uri uri, string selection, string[] selectionArgs)
		{
			throw new Java.Lang.UnsupportedOperationException();
		}

		public override global::Android.Net.Uri Insert(global::Android.Net.Uri uri, ContentValues values)
		{
			throw new Java.Lang.UnsupportedOperationException();
			/*SQLiteDatabase sqlDB = movieDB.WritableDatabase;
			long newId = 0;
			switch(uriMatcher.Match (uri)){
			case GET_ALL:
				newId = sqlDB.InsertWithOnConflict ("movies", null, values, Conflict.Replace);
				break;
			case GET_ONE:
				break;
			default:
				throw new Java.Lang.IllegalArgumentException("Unknown Uri: " + uri);
			}
			if(newId>0){
				Uri newUri = ContentUris.WithAppendedId (uri, newId);
				Context.ContentResolver.NotifyChange(uri, null);
				return newUri;
			}else{
				throw new SQLException("Failed to insert row into "+uri);
			}*/
		}

		public override int Update(global::Android.Net.Uri uri, ContentValues values, string selection, string[] selectionArgs)
		{
			throw new Java.Lang.UnsupportedOperationException();
		}

		public override String GetType(global::Android.Net.Uri uri)
		{
			switch (uriMatcher.Match(uri)) {
			case GET_ALL:
				return MOVIES_MIME_TYPE;
			case GET_ONE:
				return MOVIE_MIME_TYPE;
			default:
				throw new Java.Lang.IllegalArgumentException ("Unknown Uri: " + uri);
			}
		}
		
	}
}

