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
using Android.Net;

namespace Funky_Film.Android.Util
{
	public class ConnectivityChecker
	{

		private Context context;

		public ConnectivityChecker(Context context){
			this.context = context;
		}

		public bool IsConnected(){

			ConnectivityManager cm = (ConnectivityManager)context.GetSystemService (Context.ConnectivityService);
			NetworkInfo netInfo = cm.ActiveNetworkInfo;

			return (netInfo != null && netInfo.IsConnectedOrConnecting);

		}


	}
}

