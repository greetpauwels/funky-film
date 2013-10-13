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
using Funky_Film.Model;

namespace Funky_Film
{
	public class Credits
	{
		public List<CastCredit> cast { get; set; }
		public List<CrewCredit> crew { get; set; }
		public int id { get; set; }


	}
}

