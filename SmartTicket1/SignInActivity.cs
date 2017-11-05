using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;


using System.Threading.Tasks;

using Parse;

namespace SmartTicket1
{
	[Activity (Label = "SmartTicket", Icon = "@drawable/icon",MainLauncher=true)]
	public class SignInActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			/*Android.Hide();*/
			Window.ClearFlags (WindowManagerFlags.TranslucentStatus);

			//Window.SetStatusBarColor (Android.Graphics.Color.Transparent);
			RequestWindowFeature (WindowFeatures.NoTitle);
			base.OnCreate (bundle);


			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.SignInView);




			// Get our button from the layout resource,
			// and attach an event to it


			TextView signupLink = FindViewById<TextView> (Resource.Id.link_signup);
			signupLink.Click += (sender, e) =>
			{
				var intent = new Intent(this, typeof(SignUpActivity));
				StartActivity(intent);
			};
		


			Button login = FindViewById<Button> (Resource.Id.btn_login);

			login.Click += async (sender, e) =>
			{
				string password3 = FindViewById<EditText>(Resource.Id.input_password).Text;
				string mail3 = FindViewById<EditText>(Resource.Id.input_email).Text;

				var userExistTask = log(mail3, password3);
				bool userExist = await userExistTask;

				if (userExist){
//					string UserId = ParseUser.CurrentUser.Get<string> ("UserID");
//					string firstname = ParseUser.CurrentUser.Get<string> ("FirstName");
//					string lastname = ParseUser.CurrentUser.Get<string> ("LastName");
//					string email = ParseUser.CurrentUser.Get<string> ("username");

					var intent1 = new Intent(this, typeof(MainActivity));
//					intent1.PutExtra("UserId",UserId);
//					intent1.PutExtra("FirstName",firstname);
//					intent1.PutExtra("LastName",lastname);
//					intent1.PutExtra("username",email);
				StartActivity(intent1);
				}



			};
				
			Window.SetSoftInputMode (SoftInput.StateAlwaysHidden);
		

//			var imageView =
//				FindViewById<ImageView> (Resource.Id.imageView1);
			/*imageView.SetImageResource (Resource.Drawable.logogreen);*/


		
		}

		public async static Task<bool> log( string mail2,string password2)
		{
			try{
				Console.WriteLine (mail2);
				Console.WriteLine (password2);

				await ParseUser.LogInAsync(mail2, password2);

				bool userExist= true;
				Console.WriteLine ("signed in");
				return userExist;

			}// Login was successful.
			catch( Exception e)
			{
				bool userExist= false;
				Console.WriteLine ("fail");
				return userExist;
			}
		}
	}

	class MyApp : Application{
		private static Context mContext;
		public void onCreate(){
			mContext = this.ApplicationContext;

		}
		public static Context getAppContext(){
			return mContext;
	}
}
}


