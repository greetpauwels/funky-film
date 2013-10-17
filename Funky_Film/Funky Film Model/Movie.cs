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
using Funky_Film.Util;


namespace Funky_Film.Model
{
	public class Movie
	{

		public int Id{ get; set; }
		public string Backdrop_path { get; set; }
		public string Poster_path{ get; set; }
		public bool Adult{ get; set; }
		public string[] Genre{ get; set; }
		public string Original_title{ get; set; }
		public string Tagline{ get; set; }
		public string Overview{ get; set; }
		public string Release_date{ get; set; }
		public int Runtime{ get; set; }
		public string Status{ get; set; }
		public Actor[] Cast{ get; set; }
		public CrewMember[] Crew{ get; set; }
		public string[] Keywords{ get; set; }
		public double Vote_average { get; set; }
		public double Vote_count { get; set;}

		public Movie(){
		}

	}
}

