
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Parse;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace SmartTicket1
{
	[Activity (Label = "Paramètres")]			
	public class SettingsActivity : Activity
	{

		ParseUser user;
		protected  override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);
			SetContentView (Resource.Layout.SettingsLayout);
			Window.SetSoftInputMode (SoftInput.StateAlwaysHidden);
			ActionBar.SetDisplayHomeAsUpEnabled(true);

			user = ParseUser.CurrentUser;
			string FirstName = ParseUser.CurrentUser.Get<string> ("FirstName");
			string LastName = ParseUser.CurrentUser.Get<string> ("LastName");
			string Email = ParseUser.CurrentUser.Get<string> ("username");
			string Phone = ParseUser.CurrentUser.Get<string> ("PhoneNumber");

			EditText edittxt_FirstName = FindViewById<EditText>(Resource.Id.input_firstname);
			edittxt_FirstName.Text=FirstName;
			EditText edittxt_LastName = FindViewById<EditText>(Resource.Id.input_lastname);
			edittxt_LastName.Text=LastName;
			EditText edittxt_email = FindViewById<EditText>(Resource.Id.email_signup);
			edittxt_email.Text=Email;
			EditText edittxt_phone = FindViewById<EditText>(Resource.Id.phone);
			edittxt_phone.Text=Phone;


			Button update = FindViewById<Button> (Resource.Id.update_info);

			update.Click += (sender, e) =>
			{
				string phone_new = FindViewById<EditText>(Resource.Id.phone).Text;
				string mailSignup_new = FindViewById<EditText>(Resource.Id.email_signup).Text;
				string FirstName_new = FindViewById<EditText>(Resource.Id.input_firstname).Text;
				string LastName_new = FindViewById<EditText>(Resource.Id.input_lastname).Text;

				Update_Click( FirstName_new,  LastName_new,phone_new,  mailSignup_new);
				Toast.MakeText(this, "Enregistré", ToastLength.Short).Show();
			};



		}

		//		public override void OnBackPressed()
		//		{
		//			Toast.MakeText(this, "Enregistré", ToastLength.Short).Show();
		//		}
		//
		public  override bool OnOptionsItemSelected(IMenuItem item) {
			switch (item.ItemId) {
			case Android.Resource.Id.Home:
				// User chose the "Settings" item, show the app settings UI... 

				//var MainIntent = new Intent(this, typeof(MainActivity));
				//StartActivity(MainIntent);

				OnBackPressed ();

				return true; 


			default: 
				// If we got here, the user's action was not recognized. 
				// Invoke the superclass to handle it. 
				return base.OnOptionsItemSelected(item);

			} 
		} 


		public async void Update_Click(string  FirstName_new, string LastName_new,string phone_new, string mailSignup_new)
		{

			try{
				user.Email = mailSignup_new;
				user.Username = mailSignup_new;
				user ["FirstName"] = FirstName_new;
				user ["LastName"] = LastName_new;
				user ["PhoneNumber"] = phone_new;
				await user.SaveAsync();
				Console.WriteLine ("Enregistré");
			}
			catch( Exception e){
				Console.WriteLine ("Veuillez réessayer");
			}
		}
	}
}

