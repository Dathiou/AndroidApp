//
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//
//using Android.App;
//using Android.Content;
//using Android.OS;
//using Android.Runtime;
//using Android.Views;
//using Android.Widget;
//
//namespace SmartTicket1
//{
//	[Activity (Label = "ListAdaptater1")]			
//
//
//public class TransactionAdaptater : BaseAdapter<string> {
//		List<string> items;
//	Activity context;
//
//		public TransactionAdaptater(Activity context, List<string> items) : base() {
//		this.context = context;
//		this.items = items;
//	}
//	public override long GetItemId(int position)
//	{
//		return position;
//		}
//	public override string this[int position] {  
//		get { return items[position]; }
//	}
//	public override int Count {
//		get { return items.Count; }
//	}
//	public override View GetView(int position, View convertView, ViewGroup parent)
//	{
//			
//			View view = convertView; // re-use an existing view, if one is supplied
//			if (view == null) // otherwise create a new one
//				view = context.LayoutInflater.Inflate(Android.Resource.Layout.SimpleListItem1, null);
//			// set view properties to reflect data for the given row
//			view.FindViewById<TextView>(Android.Resource.Id.Text1).Text = items[position];
//			// return the view, populated with data, for display
//			return view;
//	}
//	}
//}