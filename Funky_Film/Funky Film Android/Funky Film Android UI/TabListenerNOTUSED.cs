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
	public class TabListener<T>: Java.Lang.Object, ActionBar.ITabListener where T: Fragment
	{

		private T fragment; 
		private Context context;
		private string tag;
		private string fragmentName;

		public TabListener(Context listenerContext){
				this.context = listenerContext;
				this.fragmentName = typeof(T).Namespace.ToLower () + "." + typeof(T).Name;
		}

		public TabListener(Context listenerContext, string tag, T exisitingFragment = null) :  this(listenerContext){
			this.fragment = exisitingFragment;
			this.tag = tag;
		}

		public T Fragment{
			get { return this.fragment;}
		}

		public void OnTabReselected(ActionBar.Tab tab, FragmentTransaction ft){

		}
		
		public void OnTabSelected(ActionBar.Tab tab, FragmentTransaction ft){
			if (this.fragment == null) {
				this.fragment = (T)global::Android.App.Fragment.Instantiate (this.context, this.fragmentName);
				if (this.tag != null) {
					ft.Add (global::Android.Resource.Id.Content, this.fragment, this.tag);
				} else {
					ft.Add (global::Android.Resource.Id.Content, this.fragment);
				}
			} else {
				ft.Attach (this.fragment);
			}
			//ft.Replace(Resource.Id.movie_container,_fragment, typeof(T).FullName);
		}
		
		public void OnTabUnselected(ActionBar.Tab tab, FragmentTransaction ft){
			if(this.fragment!=null){
				ft.Detach (this.fragment);
			}
			//ft.Remove( _fragment);
		}

		protected override void Dispose(bool disposing){
			if (disposing)
				this.fragment.Dispose();

			base.Dispose (disposing);
		}
	}
}

