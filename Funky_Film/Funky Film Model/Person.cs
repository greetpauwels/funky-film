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
	public class Person
	{	
		public string biography { get; set; }
		public string birthday { get; set; }
		public string deathday { get; set; }
		public string homepage { get; set; }
		public int id { get; set; }
		public string name { get; set; }
		public string place_of_birth { get; set; }
		public string profile_path { get; set; }
	}
}

