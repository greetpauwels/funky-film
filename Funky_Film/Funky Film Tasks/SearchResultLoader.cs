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
using Funky_Film.Tasks;
using Android.Graphics;
using System.Net;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.IO;
using Funky_Film.Model;
using Android.Util;

namespace Funky_Film.Tasks
{
	public class SearchResultLoader
	{
		public async Task<MovieList> GetSearchResults(string url){
			Log.Info ("SearchResultLoader", "GetSearchResultsIN" );
			var request = (HttpWebRequest)HttpWebRequest.Create (new Uri (url));
			request.Accept = "application/json";
			WebResponse responseObject = await Task.Factory.FromAsync<WebResponse> (request.BeginGetResponse, request.EndGetResponse, request);
			var responseStream = responseObject.GetResponseStream ();
			Log.Info ("SearchResultLoader", responseStream.ToString ());
			var sr = new StreamReader (responseStream);
			string content = await sr.ReadToEndAsync ();
			Log.Info ("SearchResultLoader", "GetSearchResultsOUT" );
			return JsonConvert.DeserializeObject<MovieList> (content);
		}

	}
}

