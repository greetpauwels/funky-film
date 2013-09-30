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
using Android.Util;

namespace Funky_Film
{
	class FunkyFilmApplication : Application
	{

		const string URL_BASE = "http://api.themoviedb.org/3/";
		const string URL_IMAGE = "http://cf2.imgobject.com/t/p/";
		//const string URL_IMAGE_92 = "http://cf2.imgobject.com/t/p/w92";
		//const string URL_IMAGE_154 = "http://cf2.imgobject.com/t/p/w154";
		const string API_KEY = "?api_key=d4eaf6d57166a7aff04e720e78dd60da";
		const string SESSION = "&session_id=";
		const string TOKEN = "&request_token=";
		const string TAG = "FunkyFilmApplication";

		private static FunkyFilmApplication instance;

		public static FunkyFilmApplication getInstance(){
			return instance;
		}

		//getBase
		//getImage(int width){
		//string full_path = URL_IMAGE + width.toString ();
		//Log.Debug(TAG, full_path);
		//return full_path;}

	}
}

