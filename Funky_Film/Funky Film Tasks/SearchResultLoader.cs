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
		string contentConverted; 

		public async Task<string> CreateWebRequest(string url){

			Log.Info ("SearchResultLoader", "CreateWebRequestIN" );

			var request = (HttpWebRequest)HttpWebRequest.Create (new Uri (url));
			request.Accept = "application/json";
			Log.Info ("SearchResultLoader", "CreateWebRequestMID1" );
			WebResponse responseObject = await Task.Factory.FromAsync<WebResponse> (request.BeginGetResponse, request.EndGetResponse, request);
			Log.Info ("SearchResultLoader", "CreateWebRequestMID2" );
			var responseStream = responseObject.GetResponseStream ();
			var sr = new StreamReader (responseStream);
			string content = await sr.ReadToEndAsync ();

			Log.Info ("SearchResultLoader", "CreateWebRequestOUT" );

			return content;
		}

		public async Task<MovieList> GetSearchResults(string url){
			Log.Info ("SearchResultLoader", "GetSearchResultsIN" );
			contentConverted = await CreateWebRequest (url);
			Log.Info ("SearchResultLoader", "GetSearchResultsOUT" );
			return JsonConvert.DeserializeObject<MovieList> (contentConverted);

		}

		public async Task<Movie> GetMovieDetail(string url){
			Log.Info ("SearchResultLoader", "GetMovieDetailsIN" );
			contentConverted = await CreateWebRequest (url);
			Log.Info ("SearchResultLoader", "GetMovieDetailOUT" );
			return JsonConvert.DeserializeObject<Movie> (contentConverted);

		}


		/*public async Task<MovieList> GetSearchResults(string url){
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
		}*/



	}
}

