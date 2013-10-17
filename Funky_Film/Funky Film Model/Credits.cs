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

namespace Funky_Film.Model
{
	public class Credits
	{
		public List<CastCredit> Cast { get; set; }
		public List<CrewCredit> Crew { get; set; }
		public int Id { get; set; }


	}
}

