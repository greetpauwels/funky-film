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

		private int id{ get; set; };
		public String backdrop_path { get; set; };
		public String poster_path{ get; set; };
		public bool adult{ get; set; };
		public String[] genre{ get; set; };
		public String original_title{ get; set; };
		public String tagline{ get; set; };
		public String overview{ get; set; };
		public String release_date{ get; set; };
		public int runtime{ get; set; };
		public String status{ get; set; };
		public Actor[] cast{ get; set; };
		public CrewMember[] crew{ get; set; };
		public String[] keywords{ get; set; };

		public Movie(){
		}

	}
}

