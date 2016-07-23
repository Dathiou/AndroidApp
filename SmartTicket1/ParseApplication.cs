using System;
using Android.App;
using Android.Runtime;
using Parse;

namespace SmartTicket1
{
	[Application(Name = "smartticket1.ParseApplication")]
	public class App : Application
	{
		public App (IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
		{
			//ParseObject.RegisterSubclass<Items>();
			ParseClient.Initialize("UjjU6TxHEI9WyqSsUCePlVxfv8xIgnzqTtRZzAG3",
				"AxoQJwnNnOA7nmNSIime8NNxqrxJOib53NgNmTzf");
		}

		public override void OnCreate ()
		{
			base.OnCreate ();

			// Initialize the Parse client with your Application ID and .NET Key found on
			// your Parse dashboard
			ParseClient.Initialize("UjjU6TxHEI9WyqSsUCePlVxfv8xIgnzqTtRZzAG3",
				"AxoQJwnNnOA7nmNSIime8NNxqrxJOib53NgNmTzf");
			ParsePush.ParsePushNotificationReceived += ParsePush.DefaultParsePushNotificationReceivedHandler;


			
		}

	}
}
