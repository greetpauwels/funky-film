using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Funky_Film.Tasks
{
	class ImageLoaderTask : AsyncTask
	{

		private	Context _context;
		private String _url;



		public ImageLoaderTask(Context context,String url){
			_context = context;
			_url = url;
		}


		protected override Java.Lang.Object DoInBackground(params Java.Lang.Object[] @params){
			return true;
		}

		protected override void OnPostExecute(Java.Lang.Object result){
			base.OnPostExecute(result);       
		}

	}
}

