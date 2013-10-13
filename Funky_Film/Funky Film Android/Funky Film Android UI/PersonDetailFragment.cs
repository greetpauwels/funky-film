using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using System.Threading.Tasks;
using Funky_Film.Model;
using Funky_Film.Tasks;
using Funky_Film.Android.Tasks;

namespace Funky_Film.Android.UI
{
	public class PersonDetailFragment : Fragment
	{

		private View view;

		ImageView personPoster;
		TextView personName;
		TextView birthday;
		TextView deathday;
		TextView birthplace;
		TextView homepage;
		TextView bio;
		
		Person person;
		int personId;

		Intent intent;




		public override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			intent = Activity.Intent;
			personId = intent.GetIntExtra ("personId", 0);
		}


		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState){

			view = inflater.Inflate (Resource.Layout.PersonDetailFragment, container, false);

			personName = (TextView)view.FindViewById (Resource.Id.person_name);
			birthday = (TextView)view.FindViewById (Resource.Id.person_birthdate_cnt);
			deathday = (TextView)view.FindViewById (Resource.Id.person_deathdate_cnt);
			birthplace =(TextView)view.FindViewById (Resource.Id.person_birthplace_cnt);
			bio =  (TextView)view.FindViewById (Resource.Id.person_bio_cnt);
			personPoster = (ImageView)view.FindViewById (Resource.Id.person_poster);
			homepage = (TextView)view.FindViewById (Resource.Id.person_homepage_cnt);

			NewPerson();

			return view;

		}

		public async Task<Person> LoadPerson(){
			string urlPerson = Const.UrlPerson+personId+Const.ApiKey;
			Log.Info ("PersonDetailFragment", "urlPerson= " + urlPerson);
			person = await new SearchResultLoader ().GetPersonDetail (urlPerson);
			return person;
		}

		public async void NewPerson(){

			await LoadPerson ();

			personName.Text = person.name;
			bio.Text = person.biography;
			birthday.Text = person.birthday;
			deathday.Text = person.deathday;
			birthplace.Text = person.place_of_birth;
			homepage.Text = person.homepage;

			if (person.profile_path != null) {
				string url = Const.UrlImage154 + person.profile_path;
				personPoster.SetImageBitmap (new RemoteImageLoaderAndroid().GetRemoteBitMap(url));
			}

		}
	}
}

