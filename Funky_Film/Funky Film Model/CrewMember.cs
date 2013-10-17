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
		public int Id { get; set; }
		public String Name { get; set; }
		public String Department { get; set; }
		public String Profile_path { get; set; }

		public CrewMember(){
		}
	}
}

