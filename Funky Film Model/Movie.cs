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
	public class Movie
	{

		public int id{ get; set; }
		public string backdrop_path { get; set; }
		public string poster_path{ get; set; }
		public bool adult{ get; set; }
		public string[] genre{ get; set; }
		public string original_title{ get; set; }
		public string tagline{ get; set; }
		public string overview{ get; set; }
		public string release_date{ get; set; }
		public int runtime{ get; set; }
		public string status{ get; set; }
		public Actor[] cast{ get; set; }
		public CrewMember[] crew{ get; set; }
		public string[] keywords{ get; set; }

		public Movie(){
		}

	}
}

