
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
using ZXing;

namespace SmartTicket1
{
	[Activity (Label = "ImageActivity")]			
	public class BarCodeGenerator : Activity
	{
		ImageView imageBarcode;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.ImageActivity);


		
			imageBarcode = FindViewById<ImageView> (Resource.Id.imageBarcode);

			var barcodeWriter = new ZXing.Mobile.BarcodeWriter {
				Format = ZXing.BarcodeFormat.CODE_128,
				Options = new ZXing.Common.EncodingOptions {
					Width = 600,
					Height = 300
				}
			};
			var barcode = barcodeWriter.Write ("12houon");

			imageBarcode.SetImageBitmap (barcode);
		}
	}
}

