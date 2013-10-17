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

namespace Funky_Film.Android.UI
{
	public class TabListener<T>: Java.Lang.Object, ActionBar.ITabListener where T: Fragment, new()
	{

		private T _fragment; 

		public TabListener(){
			_fragment = new T ();
		}

		protected TabListener(T fragment){
			_fragment = fragment;
		}

		public void OnTabReselected(ActionBar.Tab tab, FragmentTransaction ft)
		{

		}
		
		public void OnTabSelected(ActionBar.Tab tab, FragmentTransaction ft)
		{
			ft.Replace(Resource.Id.movie_container,_fragment, typeof(T).FullName);
		}
		
		public void OnTabUnselected(ActionBar.Tab tab, FragmentTransaction ft)
		{
			ft.Remove( _fragment);
		}

	}
}

