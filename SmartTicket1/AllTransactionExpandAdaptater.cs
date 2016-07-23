using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;


using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Support.V4.App;
using Android.Support.V4.View;


using Parse;

namespace SmartTicket1
{
	[Activity (Label = "AllTransactionExpandAdaptater")]		

	public class AllTransactionExpandAdaptater : BaseExpandableListAdapter
	{
		//Activity context;
		//ExpandableListView lv;
		DataTransferInterface dtInterface;
		protected List<Grouping<string,itu>> items { get; set; }
	
	

		public AllTransactionExpandAdaptater(/*DataTransferInterface dtInterface,*/ObservableCollection<Grouping< string,itu>> items)
		{
			//this.context = context;

			this.items = new List<Grouping<string,itu>>(items);
			//this.AreAllItemsEnabled = items;

			//activity = a;
			//this.dtInterface = dtInterface;
			//var inflater = Application.Context.GetSystemService (Context.LayoutInflaterService) as LayoutInflater;
		}





		public override Java.Lang.Object GetChild(int groupPosition, int childPosition)
	{
			return new ObjectRef<itu> (items.ElementAt (groupPosition).ElementAt (childPosition));
				
		}

		public override long GetChildId(int groupPosition, int childPosition)
		{
			return childPosition;
		}

		public override int GetChildrenCount(int groupPosition)
		{
			return items[groupPosition].Count/*return child count for parent in groupPosition position*/;
		}

		public override View GetChildView(int groupPosition, int childPosition, bool isLastChild, View convertView, ViewGroup parent)
		{
		//	View view = convertView; // re-use an existing view, if one is supplied
		//	if (view == null) { // otherwise create a new one
			int nbChild=GetChildrenCount (groupPosition)-1;
			View view = convertView;
				if (childPosition == 0) {
				
					var inflater = Application.Context.GetSystemService (Context.LayoutInflaterService) as LayoutInflater;
					//mInflater = (LayoutInflater) getSystemService(Context.LAYOUT_INFLATER_SERVICE);
					view = inflater.Inflate(Resource.Layout.FirstItemView, parent,false);


					//view = context.LayoutInflater.Inflate (Resource.Layout.FirstItemView, parent, false);
				view.FindViewById<TextView> (Resource.Id.Store).Text = items [groupPosition].Store_Name;
				Android.Graphics.Typeface f = Android.Graphics.Typeface.CreateFromAsset(Application.Context.Assets, "fonts/Merchant Copy.ttf");
				view.FindViewById<TextView> (Resource.Id.Store).SetTypeface(f, Android.Graphics.TypefaceStyle.Normal);


				view.FindViewById<TextView> (Resource.Id.Address).Text = items [groupPosition].Store_Address;
				view.FindViewById<TextView> (Resource.Id.Address).SetTypeface(f, Android.Graphics.TypefaceStyle.Normal);
				view.FindViewById<TextView> (Resource.Id.Phone).Text = items [groupPosition].Store_Phone;
				view.FindViewById<TextView> (Resource.Id.Phone).SetTypeface(f, Android.Graphics.TypefaceStyle.Normal);
					view.FindViewById<TextView> (Resource.Id.Date).Text = items [groupPosition].Date;
				view.FindViewById<TextView> (Resource.Id.Date).SetTypeface(f, Android.Graphics.TypefaceStyle.Normal);
				view.FindViewById<TextView> (Resource.Id.Trans).Text = "Transaction #: " + items [groupPosition].Trans;
				view.FindViewById<TextView> (Resource.Id.Trans).SetTypeface(f, Android.Graphics.TypefaceStyle.Normal);
				} else if (childPosition == nbChild) {
					//view = context.LayoutInflater.Inflate (Resource.Layout.LastItemView, parent, false);

					var inflater = Application.Context.GetSystemService (Context.LayoutInflaterService) as LayoutInflater;
					//mInflater = (LayoutInflater) getSystemService(Context.LAYOUT_INFLATER_SERVICE);
					view = inflater.Inflate(Resource.Layout.LastItemView, parent,false);
				Android.Graphics.Typeface f = Android.Graphics.Typeface.CreateFromAsset(Application.Context.Assets, "fonts/Merchant Copy.ttf");


				view.FindViewById<TextView> (Resource.Id.Merci).Text = items [groupPosition].Store_Name + " vous remercie de \n votre visite!";
				view.FindViewById<TextView> (Resource.Id.Merci).SetTypeface(f, Android.Graphics.TypefaceStyle.Normal);
				view.FindViewById<TextView> (Resource.Id.MTTVA).Text = "TVA ("+items [groupPosition].TVA.ToString ("0.00")+"%)\n"+ (items [groupPosition].Amount_TTC-items [groupPosition].Amount_HT).ToString ("0.00");
				view.FindViewById<TextView> (Resource.Id.MTTVA).SetTypeface(f, Android.Graphics.TypefaceStyle.Normal);
				view.FindViewById<TextView> (Resource.Id.MTHT).Text = "HT\n" + items [groupPosition].Amount_HT.ToString ("0.00");
				view.FindViewById<TextView> (Resource.Id.MTHT).SetTypeface(f, Android.Graphics.TypefaceStyle.Normal);
				view.FindViewById<TextView> (Resource.Id.MTTTC).Text = "TTC\n" + items [groupPosition].Amount_TTC.ToString ("0.00");
				view.FindViewById<TextView> (Resource.Id.MTTTC).SetTypeface(f, Android.Graphics.TypefaceStyle.Normal);
				view.FindViewById<TextView> (Resource.Id.TotalPrice).Text = "TOTAL A PAYER: " + items [groupPosition].Amount_TTC.ToString ("0.00"); //+"€" ;
				view.FindViewById<TextView> (Resource.Id.TotalPrice).SetTypeface(f, Android.Graphics.TypefaceStyle.Normal);
			} else {

					//view = context.LayoutInflater.Inflate (Resource.Layout.ItemView, parent, false);

					var inflater = Application.Context.GetSystemService (Context.LayoutInflaterService) as LayoutInflater;
					//mInflater = (LayoutInflater) getSystemService(Context.LAYOUT_INFLATER_SERVICE);
					view = inflater.Inflate(Resource.Layout.ItemView, parent,false);

				Android.Graphics.Typeface f = Android.Graphics.Typeface.CreateFromAsset(Application.Context.Assets, "fonts/Merchant Copy.ttf");
					
				view.FindViewById<TextView> (Resource.Id.Desc).Text = items [groupPosition].ElementAt (childPosition).desc;
				view.FindViewById<TextView> (Resource.Id.Desc).SetTypeface(f, Android.Graphics.TypefaceStyle.Normal);

				view.FindViewById<TextView> (Resource.Id.Qty).Text = items [groupPosition].ElementAt (childPosition).qty.ToString ();
				view.FindViewById<TextView> (Resource.Id.Qty).SetTypeface(f, Android.Graphics.TypefaceStyle.Normal);

				view.FindViewById<TextView> (Resource.Id.Price).Text = items [groupPosition].ElementAt (childPosition).amount.ToString ("0.00");//+"€";
				view.FindViewById<TextView> (Resource.Id.Price).SetTypeface(f, Android.Graphics.TypefaceStyle.Normal);

				view.FindViewById<CheckBox> (Resource.Id.checkBox1).BringToFront();
				view.FindViewById<TextView> (Resource.Id.checkBox1).SetTypeface(f, Android.Graphics.TypefaceStyle.Normal);

				}

			// set view properties to reflect data for the given row
			//view.FindViewById<TextView>(Android.Resource.Id.Text1).Text = items[position];
			// return the view, populated with data, for display
			return view;

			//setup childview


		}

		public override Java.Lang.Object GetGroup(int groupPosition)
		{
			
			return new ObjectRef<Grouping<string, itu>> (items[groupPosition]);
		}

		public override long GetGroupId(int groupPosition)
		{
			return groupPosition;
		}

		public override View GetGroupView(int groupPosition, bool isExpanded, View convertView, ViewGroup parent)
		{
			var view = convertView;

			if (view == null)
			{
				var inflater = Application.Context.GetSystemService (Context.LayoutInflaterService) as LayoutInflater;
				//mInflater = (LayoutInflater) getSystemService(Context.LAYOUT_INFLATER_SERVICE);
				view = inflater.Inflate(Resource.Layout.GroupView, parent,false);
				//view.button = (Button) convertView.findViewById(R.id.list_item_btn);
				 

//
//				addButton.setOnClickListener(new OnClickListener() {
//					
//					public override void onClick(View view) {
//						// your code to add to the child list 
//					} 
//				}
			};

			ImageButton addButton = view.FindViewById<ImageButton> (Resource.Id.list_item_btn);
			addButton.Focusable=false;
			addButton.Click += async (sender, e) => {
				//Console.WriteLine("ok");
				// Create a pointer to an object of class Point with id dlkj83d
				//ParseObject point = ParseObject.createWithoutData(items[groupPosition].objId);

				// Set a new value on quantity
				//point.put("isNote", false);
				//user ["objectID"] = true;

				//ParseQuery query = new ParseQuery("Transaction");        
				//ParseObject o = query.get(items[groupPosition].objId);


				//o.put("isNote", true);
				//o.save();



				//Toast.MakeText (this.context,"Ajouté à Note de Frais", ToastLength.Short).Show ();
				ParseQuery<ParseObject> query = ParseObject.GetQuery("Transactions");

				Console.WriteLine(items[groupPosition].objId);


				ParseObject gameScore = await query.GetAsync(items[groupPosition].objId);
				bool isnote = gameScore.Get<bool> ("IsNote");
			
					gameScore["IsNote"] = true;
					//noteFragment.HandleRefresh();
					//var rAdapter = MainActivity.GetDataNote ();
					//BaseExpandableListAdapter mAdapter = await rAdapter;
					//lv.SetAdapter (mAdapter);

					//viewPager.getAdapter().notifyDataSetChanged();
					
					//NotifyDataSetChanged();

				await gameScore.SaveAsync();
				//NoteTransactionExpandAdaptater.setValuesmdirect();
				MainActivity.setValuesm();


	//	NoteTransactionExpandAdaptater.items
				//NoteTransactionExpandAdaptater.items








				//dtInterface.setValues();
				//var fragmentB = (FragmentB) FragmentManager.FindFragmentByTag("fragmentB");
				//NotifyDataSetChanged(NoteTransactionExpandAdaptater);
				//MainActivity.refreshNotes;
				//await noteFragment.refreshNotes;
				//MainActivity.getFragmentManager
				Toast.MakeText(Android.App.Application.Context, "Ajouté aux notes de frais", ToastLength.Short).Show();
//				// find your fragment
//				noteFragment f = (noteFragment) SupportFragmentManager().findFragmentByTag("yourFragTag");
//				noteFragment fileListFragment = SupportFragmentManager.FindFragmentById<FileListFragment>(Resource.Id.FileListFragment);
//
//				FragmentManager fm = getSupportFragmentManager(); 
//				// update the list view
//				f.updateListView(); 

				// Retrieve the object by id


				//Toast.MakeText (this, "Ajouté à Note de Frais", ToastLength.Short).Show ();
			};
		
			//view.FindViewById<TextView> (Resource.Id.Transaction).Text =items[groupPosition].Trans;
			view.FindViewById<TextView> (Resource.Id.Store).Text = items[groupPosition].Store_Name;
			view.FindViewById<TextView> (Resource.Id.Date).Text = items[groupPosition].Date;
			view.FindViewById<TextView> (Resource.Id.City).Text = items[groupPosition].Store_City;
			view.FindViewById<TextView> (Resource.Id.Amount).Text =  items[groupPosition].Amount_TTC.ToString("0.00") + "€";

			//setup groupview

			return view;
		}






	

		public override bool IsChildSelectable(int groupPosition, int childPosition)
		{
			return true;
		}

		public override int GroupCount
		{
			get { return items.Count; }
		}

		public override bool HasStableIds
		{
			get { return true; }
		}

//		public override View GetGroupView(int position, Boolean isExpanded,  View convertView, ViewGroup parent)
//		{
//			View view = convertView; // re-use an existing view, if one is supplied
//			if (view == null) // otherwise create a new one
//				view = context.LayoutInflater.Inflate(Android.Resource.Layout.SimpleListItem1, null);
//			// set view properties to reflect data for the given row
//			view.FindViewById<TextView>(Android.Resource.Id.Text1).Text = items[position];
//			// return the view, populated with data, for display
//			return view;
//		}
//		public abstract View GetChildView (Int32 groupPosition, Boolean isExpanded, View convertView, ViewGroup parent)
//		{
//			View view = convertView; // re-use an existing view, if one is supplied
//			if (view == null) // otherwise create a new one
//				view = context.LayoutInflater.Inflate(Android.Resource.Layout.SimpleListItem1, null);
//			// set view properties to reflect data for the given row
//			view.FindViewById<TextView>(Android.Resource.Id.Text1).Text = items[position];
//			// return the view, populated with data, for display
//			return view;
//		}

					}

	public class ObjectRef<T>: Java.Lang.Object{
		public ObjectRef(T value){
			Value = value;
		}
		public T Value { get; private set; }
}


}
