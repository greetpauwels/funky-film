using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content.Res;
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
using Funky_Film.Android.Util;

namespace Funky_Film.Android.UI
{
	public class PersonDetailFragment : Fragment
	{

		Intent intent;
		Context context;
		Resources res;
		ConnectivityChecker connectionCheck;

		View view;
		LinearLayout emptyLayout;
		ImageView personPoster;
		TextView personName;
		TextView birthday;
		TextView birthday_cnt;
		TextView deathday;
		TextView deathday_cnt;
		TextView birthplace;
		TextView birthplace_cnt;
		TextView homepage;
		TextView homepage_cnt;
		TextView bio;
		TextView bio_cnt;
		Button reloadBttn;

		Person person;
		int personId;






		public override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			intent = Activity.Intent;
			personId = intent.GetIntExtra ("personId", 0);
			context = Activity.ApplicationContext;
			res = context.Resources;
			connectionCheck = new ConnectivityChecker (context);
		}


		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState){

			view = inflater.Inflate (Resource.Layout.PersonDetailFragment, container, false);

			personName = (TextView)view.FindViewById (Resource.Id.person_name);
			birthday = (TextView)view.FindViewById (Resource.Id.person_birthdate);
			birthday_cnt = (TextView)view.FindViewById (Resource.Id.person_birthdate_cnt);
			deathday = (TextView)view.FindViewById (Resource.Id.person_deathdate);
			deathday_cnt = (TextView)view.FindViewById (Resource.Id.person_deathdate_cnt);
			birthplace = (TextView)view.FindViewById (Resource.Id.person_birthplace);
			birthplace_cnt =(TextView)view.FindViewById (Resource.Id.person_birthplace_cnt);
			bio = (TextView)view.FindViewById (Resource.Id.person_bio);
			bio_cnt =  (TextView)view.FindViewById (Resource.Id.person_bio_cnt);
			personPoster = (ImageView)view.FindViewById (Resource.Id.person_poster);
			homepage = (TextView)view.FindViewById (Resource.Id.person_homepage_cnt);
			homepage_cnt = (TextView)view.FindViewById (Resource.Id.person_homepage_cnt);
			reloadBttn = (Button)view.FindViewById (Resource.Id.reload);
			emptyLayout = (LinearLayout)view.FindViewById (Resource.Id.emptyView);

			ProceedByConnectionStatus();

			return view;

		}

		private void ProceedByConnectionStatus(){
			if (connectionCheck.IsConnected()) {
				reloadBttn.Visibility = ViewStates.Invisible;
				emptyLayout.Visibility = ViewStates.Invisible;
				NewPerson();
			} else {
				Toast.MakeText (Activity.ApplicationContext, "No internet connection",ToastLength.Long).Show ();

				reloadBttn.Click += delegate {
					ProceedByConnectionStatus ();
				};
			}
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
			bio.Text = res.GetString (Resource.String.bio);
			bio_cnt.Text = person.biography;
			birthday.Text = res.GetString (Resource.String.birthday);
			birthday_cnt.Text = person.birthday;
			deathday.Text = res.GetString (Resource.String.deathday);
			deathday_cnt.Text = person.deathday;
			birthplace.Text = res.GetString (Resource.String.birthplace);
			birthplace_cnt.Text = person.place_of_birth;
			homepage.Text = res.GetString (Resource.String.homepage);
			homepage_cnt.Text = person.homepage;

			if (person.profile_path != null) {
				string url = Const.UrlImage154 + person.profile_path;
				personPoster.SetImageBitmap (new RemoteImageLoaderAndroid ().GetRemoteBitMap (url));
			} else {
				personPoster.SetImageDrawable (res.GetDrawable (Resource.Drawable.default_crew_image));
			}

		}
	}
}

