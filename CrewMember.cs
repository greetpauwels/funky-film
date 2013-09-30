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
	public class CrewMember
	{
		public int id { get; set; };
		public String name { get; set; };
		public String department { get; set; };
		public String profile_path { get; set; };

		public CrewMember(){
		}
	}
}

