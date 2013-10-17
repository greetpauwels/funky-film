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
using Android;
using Android.Util;

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
		
		public static int getDay(string date){
			string day = date.Substring (0, 2);
			return  Convert.ToInt32 (day);
		}

		public static int getMonth(string date){
			string month = date.Substring (3, 2);
			return  Convert.ToInt32 (month);
		}

		public static int getYear(string date){
			string year = date.Substring (6, 4);
			return  Convert.ToInt32 (year);
		}

	}

}
