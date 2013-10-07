using System;

namespace Funky_Film
{
	public static class Const
	{
		public const string ApiKey = "?api_key=d4eaf6d57166a7aff04e720e78dd60da";

		public const string UrlBase = "http://api.themoviedb.org/3/";
		public const string UrlSearch = UrlBase + "search/movie" + ApiKey + "&query=";
		public const string UrlMovie = UrlBase + "movie/";
		public const string UrlPopular = UrlMovie + "upcoming" + ApiKey;

		public const string UrlImage = "http://cf2.imgobject.com/t/p/";
		public const string UrlImage92 = "http://cf2.imgobject.com/t/p/w92";
		public const string UrlImage154 = "http://cf2.imgobject.com/t/p/w154";


		public const string TestSearch = "Batman";
		//public const string Session = "&session_id=";
		//public const string Token = "&request_token=";
		public const string Tag = "FunkyFilmApplication";
	}
}

