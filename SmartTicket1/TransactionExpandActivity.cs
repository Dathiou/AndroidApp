//
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Collections.ObjectModel;
//
//
//using Android.App;
//using Android.Content;
//using Android.OS;
//using Android.Runtime;
//using Android.Views;
//using Android.Widget;
//using Parse;
//
//using Android.Util;
//using Android.Support.V4.Widget;
//
//
//using System.Threading.Tasks;
//
//namespace SmartTicket1
//
//{
//
//
//	[Activity(Label = "Item2",  Theme= "@android:style/Theme.Holo.Light.NoActionBar")]
//	public class TransactionExpandActivity : Activity {
//
//
//		List<string> it = new List<string>();
//		Button Barcode;
//		ImageView imageBarcode;
//		SwipeRefreshLayout refresher;
//		public string UserId;
//
//
//		protected async override void OnCreate(Bundle bundle)
//		{
//
//			base.OnCreate (bundle);
//
//			//TransactionGroup ity = new TransactionGroup ();
//			//List<itu> lili = new List<itu> ();
//			UserId = ParseUser.CurrentUser.Get<string> ("UserID");
//			string FirstName = ParseUser.CurrentUser.Get<string> ("FirstName");
//			string LastName = ParseUser.CurrentUser.Get<string> ("LastName");
//			//string Email = Intent.GetStringExtra("username");
//			//string Phone = Intent.GetStringExtra("LastName");
//			SetContentView (Resource.Layout.TransactionMain);
//			FindViewById<RelativeLayout> (Resource.Id.loadingPanel2).Visibility = ViewStates.Visible;
//
//			refresher = FindViewById<SwipeRefreshLayout> (Resource.Id.refresher);
//			refresher.SetColorScheme (Resource.Color.xam_dark_blue,
//				Resource.Color.xam_purple,
//				Resource.Color.xam_gray,
//				Resource.Color.xam_green);
//			refresher.Refresh += HandleRefresh;
//
//			FindViewById<TextView> (Resource.Id.FirstName).Text = FirstName;
//			FindViewById<TextView> (Resource.Id.LastName).Text = LastName;
//
//			MobileBarcodeScanner.Initialize (Application);
//			imageBarcode = FindViewById<ImageView> (Resource.Id.imageBarcode);
//
//			var barcodeWriter = new ZXing.Mobile.BarcodeWriter {
//				Format = ZXing.BarcodeFormat.CODE_128,
//				Options = new ZXing.Common.EncodingOptions {
//					Width = 600,
//					Height = 110
//				}
//			};
//			var barcode = barcodeWriter.Write (UserId);
//			FindViewById<TextView> (Resource.Id.ID).Text = UserId;
//			imageBarcode.SetImageBitmap (barcode);
//
//			List<tran> Trans = new List<tran>();
//			var ListGrouped =  getlist (UserId,Trans);
//			ObservableCollection<Grouping<string, itu>> ListGrouped1 = await ListGrouped;
//
//
////			var menu = FindViewById<FlyOutContainer> (Resource.Id.FlyOutContainer);
////			var menuButton = FindViewById (Resource.Id.MenuButton);
////			menuButton.Click += (sender, e) => {
////				menu.AnimatedOpened = !menu.AnimatedOpened;
////			};
////			FindViewById<RelativeLayout> (Resource.Id.loadingPanel2).Visibility = ViewStates.Gone;
////			TextView Barcode = FindViewById<TextView> (Resource.Id.MyBarCode);
////			Barcode.Click += (sender, e) =>
////			{
////				var intent = new Intent(this, typeof(BarCodeGenerator));
////				StartActivity(intent);
////			};
////			TextView Settings = FindViewById<TextView> (Resource.Id.Settings);
////			Settings.Click += (sender, e) =>
////			{
////				var intentSettings = new Intent(this, typeof(SettingsActivity));
////
////				//	intentSettings.PutExtra("FirstName",FirstName);
////				//intentSettings.PutExtra("LastName",LastName);
////				StartActivity(intentSettings);
////			};
////
//
//
//			var listView = FindViewById<ExpandableListView> (Resource.Id.myExpandableListview);
//			listView.SetAdapter (new TransactionExpandAdaptater (this, ListGrouped1));
//
//			//listView.SetChildDivider( new Android.Graphics.Drawables.Drawable());
//			//			listView.SetGroupIndicator (null);
//			//			listView.SetChildIndicator (null);
//			//	listView.SetChildDivider (GetDrawable(Resource.Drawable.separator));
//
//			//listView.DividerHeight
//
//
//
//			//ExpandableListView expandbleLis = getExpandableListView();
//
//			//			var transacNb = from transac in ParseObject.GetQuery ("Transactions")
//			//					where transac.Get<string> ("UserID") == "1"
//			//				select transac;
//			//			var itemNb = from item in ParseObject.GetQuery ("Items")
//			//				join transac in transacNb on item ["TransactionNb"] equals transac ["Transaction"]
//			//				select item;
//			//
//			//			IEnumerable<ParseObject> result2 = await itemNb.FindAsync ();
//			//			foreach (var obj in result2) {
//			//						string str = obj.Get<String> ("ItemDesc");
//			//							it.Add (str);
//			//						}
//
//		}
//
//		public async static Task<ObservableCollection<Grouping<string,itu>>> getlist(string UserId,List<tran> Trans)
//		{
//			var liliTask = listoune (UserId,Trans);
//			List<itu> lili = await liliTask;
//			var lolot = GroupByTransaction (lili,Trans);
//			ObservableCollection<Grouping<string, itu>> lolo = await lolot;
//			//AddInfoGroup (lolo, Trans);
//			//string a = lolo [1].Store_Name;
//			return lolo;
//		}
//
//
//		public async static Task<List<itu>> listoune ( string UserID,List<tran> Trans ){
//			var transacNb = from transac in ParseObject.GetQuery ("Transactions")
//					where transac.Get<string> ("UserID") == UserID
//				select transac;
//			var itemNb = from item in ParseObject.GetQuery ("Items")
//				join transac in transacNb on item ["TransactionNb"] equals transac ["Transaction"] 
//				select  item;
//
//			//		itemNb.Include ("Store");
//			IEnumerable<ParseObject> result1 = await transacNb.FindAsync (); // transactions correspondant a l'user
//			IEnumerable<ParseObject> result2 = await itemNb.FindAsync ();   //items correspondants à l'user
//
//			//Crée la liste des transactions
//			foreach (var obje in result1) {
//				tran r = GetTrans (obje);
//				Trans.Add (r);
//			}
//
//
//			List<itu> tot = new List<itu>();
//			//Crée liste d'items
//			foreach (var obj in result2) {
//				itu Ar = GetItu (obj);
//				tot.Add (Ar);
//			}
//
//			return tot; // return list d'items
//		}
//
//
//
//		public static itu GetItu (ParseObject obj){
//			itu r = new itu();
//			r.desc=obj.Get<string> ("ItemDesc");
//			r.trans = obj.Get<string> ("TransactionNb");
//			r.amount= obj.Get<double> ("Price");
//			r.qty = obj.Get<int> ("Quantity");
//			//r.transactionDate=obj.Get<DateTime> ("TransactionDate").ToString("ddMMyyyy"); 
//			//				foreach (tran ti in tr) {
//			//					if (ti.trans == r.trans)
//			//					{r.store = ti.store;
//			//					//r.transactionDate = ti.transactionDate;
//			//				}
//
//
//			return r;
//
//		}
//		public static tran GetTrans (ParseObject obj){
//			{
//				tran r = new tran();
//				r.trans = obj.Get<string> ("Transaction");
//				r.storeID=obj.Get<string> ("StoreID");
//				r.transactionDate=obj.Get<DateTime> ("TransactionDate").ToString("dd/MM/yyyy"); 
//
//				//r.amount=obj.Get<double> ("Amount");
//				return r;
//			}
//		}
//
//		public static Store GetStoreInfo(  ParseObject obje){
//			Store sto = new Store();
//			//			var store = from stor in ParseObject.GetQuery ("Stores")
//			//					where stor.Get<string> ("StoreID") == StoreID
//			//				select stor;
//			//			
//			//			IEnumerable<ParseObject> Store_info = await store.FindAsync ();
//			//
//			//			foreach (var obje in Store_info) {
//
//			sto.Address=obje.Get<string> ("Address");
//			sto.PhoneNumber = obje.Get<string> ("PhoneNumber");
//			//sto.StoreID= StoreID;
//			sto.StoreCity=obje.Get<string> ("City");
//			sto.StoreName=obje.Get<string> ("StoreName");
//
//			//			}
//			return sto;
//		}
//
//		public async static Task<ObservableCollection<Grouping<string, itu>>> GroupByTransaction(List<itu> listo,List<tran> Trans)
//		{
//			var queryTransac = from itu in listo
//				group itu by itu.trans into Itegroup
//				select new Grouping<string, itu> (Itegroup.Key, Itegroup.Key, Itegroup.ToList ());
//
//			ObservableCollection<Grouping<string, itu>> MonkeysGrouped = new ObservableCollection<Grouping<string, itu>> (queryTransac);
//
//			foreach (var monkey in MonkeysGrouped) {
//				string trr = monkey.Trans;
//				double total_amount_TTC = 0;
//				foreach (var item in monkey) {
//					total_amount_TTC = total_amount_TTC +item.amount;
//				}
//				foreach (tran ti in Trans) {
//					if (ti.trans == trr) {
//						Store sto = new Store();
//
//						var store = from stor in ParseObject.GetQuery ("Stores")
//								where stor.Get<string> ("StoreID") == ti.storeID
//							select stor;
//
//						IEnumerable<ParseObject> Store_info = await store.FindAsync ();
//
//						foreach (var obje in Store_info) {
//							Store Sto = GetStoreInfo (obje);
//
//
//							//var task = GetStoreInfo (ti.storeID);
//							//Store Sto = await task;
//							monkey.Store_Address = Sto.Address;
//							monkey.Store_Phone = Sto.PhoneNumber;
//							monkey.Store_Name = Sto.StoreName;
//							monkey.Store_City = Sto.StoreCity;
//							Console.WriteLine (monkey.Store_City);
//							monkey.Amount_HT = total_amount_TTC/(1+monkey.TVA/100);
//							monkey.Amount_TTC = total_amount_TTC;
//							monkey.Date = ti.transactionDate;
//						}
//					}
//				}
//				monkey.Insert(0, new itu ());
//				monkey.Add (new itu ());
//
//
//
//
//			}
//			return MonkeysGrouped;
//		}
//
//
//		async void HandleRefresh (object sender, EventArgs e)
//		{
//			List<tran> Trans = new List<tran>();
//			var ListGrouped =  getlist (UserId,Trans);
//			ObservableCollection<Grouping<string, itu>> ListGrouped1 = await ListGrouped;
//			var listView = FindViewById<ExpandableListView> (Resource.Id.myExpandableListview);
//			listView.SetAdapter (new TransactionExpandAdaptater (this, ListGrouped1));
//			refresher.Refreshing = false;
//		}
//
//		//			             select new Grouping<string,ParseObject> (itemGroup.Key, itemGroup); 
//
//		//		Grouping<string, ParseObject> ItemsGrouped = new ObservableCollection<Grouping<string, ParseObject>>(itemNb); 
//
//
//
//
//		//	ListAdapter = new TransactionExpandAdaptater(this, result2);
//
//	}
//
//
//	//		public virtual Boolean OnChildClick(ExpandableListView l, View v, int groupPosition,int childPosition, long id)
//	//		{
//	//
//	//			var t = it[childPosition];
//	//			Android.Widget.Toast.MakeText(this, t, Android.Widget.ToastLength.Short).Show();
//	//		}
//
//
//
//
//}
//
