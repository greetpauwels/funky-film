using System;
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

}

