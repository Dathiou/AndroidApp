
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
	public class promoFragment : Android.Support.V4.App.Fragment
	{
		ImageView imagePromo;

		View vi;
		public override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			// Create your fragment here
		}


		public async override void OnViewCreated(View view, Bundle savedInstanceState)
		{
			base.OnViewCreated (view, savedInstanceState);

			imagePromo = vi.FindViewById<ImageView> (Resource.Id.promo);


			imagePromo.SetImageResource (Resource.Drawable.carrefour_market_milka);



		}


		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			// Use this to return your custom view for this Fragment
			// return inflater.Inflate(Resource.Layout.YourFragment, container, false);
			vi = inflater.Inflate(Resource.Layout.promoView, container,false);

			//Name.Text= "Damien\nThioulouse";
			return vi;
		}
	}
}

