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
	public class Actor
	{
		// TODO: a C# naming convention is to use capitals for properties
		public int id { get; set; }
		public String name { get; set; }
		public String character { get; set; }
		public int ordr { get; set; }
		public string profile_path{ get; set;}

		public Actor(){
		}

		public override string ToString ()
		{
			return this.name;
		}
	}
}

