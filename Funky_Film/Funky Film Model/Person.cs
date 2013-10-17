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
		public string Biography { get; set; }
		public string Birthday { get; set; }
		public string Deathday { get; set; }
		public string Homepage { get; set; }
		public int Id { get; set; }
		public string Name { get; set; }
		public string Place_of_birth { get; set; }
		public string Profile_path { get; set; }
	}
}

