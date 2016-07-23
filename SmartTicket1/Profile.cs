
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
using ZXing;

namespace SmartTicket1
{
	public class Profile : Android.Support.V4.App.Fragment
	{
		ImageView imageBarcode;
		TextView ID;
		TextView Name;
		View vi;
		public override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			// Create your fragment here
		}

	
		public async override void OnViewCreated(View view, Bundle savedInstanceState)
		{
			base.OnViewCreated (view, savedInstanceState);

			imageBarcode = vi.FindViewById<ImageView> (Resource.Id.imageBarcode);
			ID = vi.FindViewById<TextView> (Resource.Id.ID);
			Name = vi.FindViewById<TextView> (Resource.Id.Name);

			var userIDtask = MainActivity.getUserID ();
			String userID = await userIDtask;
			var barcodeWriter = new ZXing.Mobile.BarcodeWriter {
				Format = ZXing.BarcodeFormat.CODE_128,
				Options = new ZXing.Common.EncodingOptions {
					Width = 700,
					Height = 300
				}
			};

			var barcode = barcodeWriter.Write (userID);

			imageBarcode.SetImageBitmap (barcode);

			ID.Text = userID;

		}


		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			// Use this to return your custom view for this Fragment
			// return inflater.Inflate(Resource.Layout.YourFragment, container, false);
			vi = inflater.Inflate(Resource.Layout.Profile, container,false);

			//Name.Text= "Damien\nThioulouse";
			return vi;
		}
	}
}

