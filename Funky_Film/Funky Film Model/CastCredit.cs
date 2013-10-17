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

namespace Funky_Film.Model
{
	public class CastCredit
	{
		public int id { get; set; }
		public string title { get; set; }
		public string character { get; set; }
		public string original_title { get; set; }
		public string poster_path { get; set; }
		public string release_date { get; set; }
	}
}

