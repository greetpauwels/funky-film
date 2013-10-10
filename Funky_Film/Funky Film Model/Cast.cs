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
	public class Cast
	{

		public int id{ get; set; } 
		public Actor[]cast {get; set;}
		public CrewMember[] crew{ get; set;}

	}
}

