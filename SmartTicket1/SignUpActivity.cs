
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
using System.Threading.Tasks;
using Parse;

namespace SmartTicket1
{
	[Activity (Label = "SignUpActivity")]			
	public class SignUpActivity : Activity
	{


		public async void SignUpButton_Click(string password1, string email1,string firstname, string lastname)
		{
			var user = new ParseUser()
			{
				Username =email1,
				Password = password1,
				Email = email1
			};

			// other fields can be set just like with ParseObject
			user["UserID"] = ((int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds).ToString();
			user ["FirstName"] = firstname;
			user ["LastName"] = lastname;
			try{
			await user.SignUpAsync();
				Console.WriteLine (password1);
				Console.WriteLine ("Registered");
			}
			catch( Exception e){
				Console.WriteLine ("fail");
			}
		}
		protected override void OnCreate (Bundle bundle)
		{
			
				/*Android.Hide();*/
				Window.ClearFlags (WindowManagerFlags.TranslucentStatus);
				Window.AddFlags (WindowManagerFlags.DrawsSystemBarBackgrounds);
				Window.SetStatusBarColor (Android.Graphics.Color.ForestGreen);
				RequestWindowFeature (WindowFeatures.NoTitle);
				

			base.OnCreate (bundle);

				// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.SignUpView);

				// Get our button from the layout resource,
				// and attach an event to it



			Button signup = FindViewById<Button> (Resource.Id.btn_signup);

			signup.Click += (sender, e) =>
			{
				string passwordSignup = FindViewById<EditText>(Resource.Id.password_signup).Text;
				string mailSignup = FindViewById<EditText>(Resource.Id.email_signup).Text;
				string FirstName = FindViewById<EditText>(Resource.Id.input_firstname).Text;
				string LastName = FindViewById<EditText>(Resource.Id.input_lastname).Text;

				SignUpButton_Click(passwordSignup,mailSignup,FirstName,LastName);
			};


			TextView signinLink = FindViewById<TextView> (Resource.Id.link_login);
			signinLink.Click += (sender, e) =>
			{
				var intent = new Intent(this, typeof(SignInActivity));
				StartActivity(intent);
			};

			Window.SetSoftInputMode (SoftInput.StateAlwaysHidden);
			}
		}
	}



