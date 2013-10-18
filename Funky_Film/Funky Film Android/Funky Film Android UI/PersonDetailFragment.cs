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
using Funky_Film.Util;
using Java.Util;
using Android.Provider;
using Android.Text.Format;

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
		TextView birthplace_cnt;
		TextView homepage;
		TextView homepage_cnt;
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
			birthplace_cnt =(TextView)view.FindViewById (Resource.Id.person_birthplace_cnt);
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

			personName.Text = person.Name;
			bio_cnt.Text = person.Biography;
			birthday.Text = res.GetString (Resource.String.birthday);
			birthday_cnt.Text = UIUtil.ConvertDateToEuropean(person.Birthday);

			deathday.Text = res.GetString (Resource.String.deathday);
			if (person.Deathday != "") {
				deathday_cnt.Text = UIUtil.ConvertDateToEuropean (person.Deathday);
			} else {
				deathday.Visibility = ViewStates.Gone;
				deathday_cnt.Visibility = ViewStates.Gone;
			}
			birthplace_cnt.Text = person.Place_of_birth;
			homepage.Text = res.GetString (Resource.String.homepage);
			homepage_cnt.Text = person.Homepage;

			if (person.Profile_path != null) {
				string url = Const.UrlImage154 + person.Profile_path;
				personPoster.SetImageBitmap (new RemoteImageLoaderAndroid ().GetRemoteBitMap (url));
			} else {
				personPoster.SetImageDrawable (res.GetDrawable (Resource.Drawable.default_actor_image));
			}

			birthday_cnt.Click += delegate {
				addBirthdayToCalendar ();
			};

		}

		private void addBirthdayToCalendar(){
			Intent bday = new Intent (Intent.ActionEdit);
			bday.SetType ("vnd.android.cursor.item/event");

			string birthdayforIntent = UIUtil.ConvertDateToEuropean (person.Birthday);
			int bday_day = UIUtil.getDay (birthdayforIntent);
			int bday_month = (UIUtil.getMonth (birthdayforIntent))-1;
			int bday_year = UIUtil.getYear (birthdayforIntent);
			Time today = new Time (Time.CurrentTimezone);
			today.SetToNow ();
			int year_start = today.Year;
			int year_end = 2060;

			GregorianCalendar date1 = new GregorianCalendar (year_start, bday_month, bday_day, 0, 0);
			GregorianCalendar date2 = new GregorianCalendar (year_end, bday_month, bday_day, 0, 0);

			bday.PutExtra (CalendarContract.ExtraEventAllDay, true);
			bday.PutExtra (CalendarContract.ExtraEventBeginTime, date1.TimeInMillis);
			bday.PutExtra (CalendarContract.ExtraEventEndTime, date2.TimeInMillis);
			bday.PutExtra ("rrule", "FREQ=YEARLY");
			bday.PutExtra ("title", String.Format ("{0} born on {1}", person.Name, birthdayforIntent));
			StartActivity (bday);
		}

	
	}
}

