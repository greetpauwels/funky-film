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

namespace Funky_Film.Android.Tasks
{
	public class RemoteImageLoaderAndroid
	{
		Bitmap imageBitMap;

		public Bitmap GetRemoteBitMap(string uri){
			byte[] originalByteArray = new RemoteImageLoader ().LoadImage (uri);

			if (originalByteArray != null && originalByteArray.Length > 0) {
				imageBitMap = BitmapFactory.DecodeByteArray (originalByteArray, 0, originalByteArray.Length);
			}

			return imageBitMap;
		}
	}
}

