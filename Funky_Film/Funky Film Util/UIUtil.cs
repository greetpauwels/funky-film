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

namespace Funky_Film.Util
{
	public class UIUtil
	{

		public static string ConvertDateToEuropean(string date){
			if (date!=null && date.Length == 10) {
				string day = date.Substring (8, 2);
				string month = date.Substring (5, 2);
				string year = date.Substring (0, 4);
				return day + "-" + month + "-" + year;
			} else {
				return "-";
			}


		}
	}
}

