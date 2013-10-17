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
	public class CrewCredit
	{

		public int Id { get; set; }
		public string Title { get; set; }
		public string Original_title { get; set; }
		public string Department { get; set; }
		public string Job { get; set; }
		public string Poster_path { get; set; }
		public string Release_date { get; set; }

	}
}

