using System;
using Funky_Film.Tasks;
using System.Net;

namespace Funky_Film.Tasks
{
	public class RemoteImageLoader : ImageLoader
	{
		

		public byte[] LoadImage(String uri){

			byte[] imageByteArray = null;

			using (var webClient = new WebClient ()) {

				//imageByteArray = webClient.DownloadDataAsync(uri)
				imageByteArray = webClient.DownloadData (uri);

			}

			return imageByteArray;
		}
	}



	/*public class RemoteImageLoader : ImageLoader
	{
		byte[] LoadImage(String uri){

			byte[] imageByteArray = null;

			using (var webClient = new WebClient ()) {

				webClient.DownloadDataAsync(uri)

				webclient.DownloadDataCompleted += (s, e) => {

						imageByteArray = e.Result;

				}

			}

			return imageByteArray;
		}
	}*/
}

