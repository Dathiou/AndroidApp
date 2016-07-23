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
//
//using Android.Util;
//using Android.Support.V4.Widget;
//
//using System.Threading.Tasks;
//
//namespace SmartTicket1
//{
//	[Activity (Label = "MainActivity")]			
//	public class MainActivity : Android.Support.V4.App.FragmentActivity
//	{
//		static readonly string Tag = "ActionBarTabsSupport";
//
//		Fragment[] _fragments;
//		SwipeRefreshLayout refresher;
//
//		protected async override void OnCreate(Bundle bundle)
//		{
//			base.OnCreate(bundle);
//
//			SetContentView(Resource.Layout.MainBar);
//
//			ActionBar.NavigationMode = ActionBarNavigationMode.Tabs;
//			SetContentView(Resource.Layout.MainBar);
//
//			_fragments = new Fragment[]
//			{
//				new test(),
//				new test(),new test(),new test(),
//				new test(),
//				new test(),
//				new test(),
//
//
//				new test()
//
//			};
//
//			AddTabToActionBar("Achats", Resource.Drawable.iconelse);
//			AddTabToActionBar("Profile", Resource.Drawable.iconelse);
//			AddTabToActionBar("Profile", Resource.Drawable.iconelse);
//			AddTabToActionBar("Profile", Resource.Drawable.iconelse);
//
//
//
//		}
//		public async static Task<BaseExpandableListAdapter> GetData()
//		{
//			string UserId = ParseUser.CurrentUser.Get<string> ("UserID");
//			string FirstName = ParseUser.CurrentUser.Get<string> ("FirstName");
//			string LastName = ParseUser.CurrentUser.Get<string> ("LastName");
//
//			List<tran> Trans = new List<tran>();
//			var ListGrouped =  getlist (UserId,Trans);
//			//await ListGrouped.GetAwaiter;
//			ObservableCollection<Grouping<string, itu>> ListGrouped2 = await ListGrouped;
//			BaseExpandableListAdapter mAdapter = new TransactionExpandAdaptater (ListGrouped2);
//			return mAdapter;
//		}
//
//		public async static Task<ObservableCollection<Grouping<string,itu>>> getlist(string UserId,List<tran> Trans)
//		{
//			var liliTask = listoune (UserId,Trans);
//			List<itu> lili = await liliTask;
//			var lolot = GroupByTransaction (lili,Trans);
//			ObservableCollection<Grouping<string, itu>> lolo = await lolot;
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
//			IEnumerable<ParseObject> result1 = await transacNb.FindAsync (); // transactions correspondant a l'user
//			IEnumerable<ParseObject> result2 = await itemNb.FindAsync ();   //items correspondants à l'user
//
//			//Crée la liste des transactions
//			foreach (var obje in result1) {
//				tran r = GetTrans (obje);
//				Trans.Add (r);
//			}
//
//			List<itu> tot = new List<itu>();
//			//Crée liste d'items
//			foreach (var obj in result2) {
//				itu Ar = GetItu (obj);
//				tot.Add (Ar);
//			}
//
//			return tot;
//		}
//
//		public static itu GetItu (ParseObject obj){
//			itu r = new itu();
//			r.desc=obj.Get<string> ("ItemDesc");
//			r.trans = obj.Get<string> ("TransactionNb");
//			r.amount= obj.Get<double> ("Price");
//			r.qty = obj.Get<int> ("Quantity");
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
//
//			sto.Address=obje.Get<string> ("Address");
//			sto.PhoneNumber = obje.Get<string> ("PhoneNumber");
//			sto.StoreCity=obje.Get<string> ("City");
//			sto.StoreName=obje.Get<string> ("StoreName");
//
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
//			}
//			return MonkeysGrouped;
//		}
//
//
//		//		public async Task<ObservableCollection<Grouping<string, itu>>> getdat()
//		//		{
//		//
//		//			string UserId = ParseUser.CurrentUser.Get<string> ("UserID");
//		//			string FirstName = ParseUser.CurrentUser.Get<string> ("FirstName");
//		//			string LastName = ParseUser.CurrentUser.Get<string> ("LastName");
//		//
//		//			List<tran> Trans = new List<tran>();
//		//			var ListGrouped =  getlist (UserId,Trans);
//		//	
//		//			ObservableCollection<Grouping<string, itu>> ListGroupedbla = await ListGrouped;
//		//			return ListGroupedbla;
//		//
//		//		}
//		//		public async static Task<ObservableCollection<Grouping<string,itu>>> getlist(string UserId,List<tran> Trans)
//		//		{
//		//			var liliTask = listoune (UserId,Trans);
//		//			List<itu> lili = await liliTask;
//		//			var lolot = GroupByTransaction (lili,Trans);
//		//			ObservableCollection<Grouping<string, itu>> lolo = await lolot;
//		//			return lolo;
//		//		}
//		//
//		//
//		//		public async static Task<List<itu>> listoune ( string UserID,List<tran> Trans ){
//		//			var transacNb = from transac in ParseObject.GetQuery ("Transactions")
//		//					where transac.Get<string> ("UserID") == UserID
//		//				select transac;
//		//			var itemNb = from item in ParseObject.GetQuery ("Items")
//		//				join transac in transacNb on item ["TransactionNb"] equals transac ["Transaction"] 
//		//				select  item;
//		//
//		//			IEnumerable<ParseObject> result1 = await transacNb.FindAsync (); // transactions correspondant a l'user
//		//			IEnumerable<ParseObject> result2 = await itemNb.FindAsync ();   //items correspondants à l'user
//		//
//		//			//Crée la liste des transactions
//		//			foreach (var obje in result1) {
//		//				tran r = GetTrans (obje);
//		//				Trans.Add (r);
//		//			}
//		//
//		//			List<itu> tot = new List<itu>();
//		//			//Crée liste d'items
//		//			foreach (var obj in result2) {
//		//				itu Ar = GetItu (obj);
//		//				tot.Add (Ar);
//		//			}
//		//
//		//			return tot;
//		//		}
//		//
//		//		public static itu GetItu (ParseObject obj){
//		//			itu r = new itu();
//		//			r.desc=obj.Get<string> ("ItemDesc");
//		//			r.trans = obj.Get<string> ("TransactionNb");
//		//			r.amount= obj.Get<double> ("Price");
//		//			r.qty = obj.Get<int> ("Quantity");
//		//			return r;
//		//
//		//		}
//		//		public static tran GetTrans (ParseObject obj){
//		//			{
//		//				tran r = new tran();
//		//				r.trans = obj.Get<string> ("Transaction");
//		//				r.storeID=obj.Get<string> ("StoreID");
//		//				r.transactionDate=obj.Get<DateTime> ("TransactionDate").ToString("dd/MM/yyyy"); 
//		//
//		//				//r.amount=obj.Get<double> ("Amount");
//		//				return r;
//		//			}
//		//		}
//		//
//		//		public static Store GetStoreInfo(  ParseObject obje){
//		//			Store sto = new Store();
//		//
//		//			sto.Address=obje.Get<string> ("Address");
//		//			sto.PhoneNumber = obje.Get<string> ("PhoneNumber");
//		//			sto.StoreCity=obje.Get<string> ("City");
//		//			sto.StoreName=obje.Get<string> ("StoreName");
//		//
//		//			return sto;
//		//		}
//		//
//		//		public async static Task<ObservableCollection<Grouping<string, itu>>> GroupByTransaction(List<itu> listo,List<tran> Trans)
//		//		{
//		//			var queryTransac = from itu in listo
//		//				group itu by itu.trans into Itegroup
//		//				select new Grouping<string, itu> (Itegroup.Key, Itegroup.Key, Itegroup.ToList ());
//		//
//		//			ObservableCollection<Grouping<string, itu>> MonkeysGrouped = new ObservableCollection<Grouping<string, itu>> (queryTransac);
//		//
//		//			foreach (var monkey in MonkeysGrouped) {
//		//				string trr = monkey.Trans;
//		//				double total_amount_TTC = 0;
//		//				foreach (var item in monkey) {
//		//					total_amount_TTC = total_amount_TTC +item.amount;
//		//				}
//		//				foreach (tran ti in Trans) {
//		//					if (ti.trans == trr) {
//		//						Store sto = new Store();
//		//
//		//						var store = from stor in ParseObject.GetQuery ("Stores")
//		//								where stor.Get<string> ("StoreID") == ti.storeID
//		//							select stor;
//		//
//		//						IEnumerable<ParseObject> Store_info = await store.FindAsync ();
//		//
//		//						foreach (var obje in Store_info) {
//		//							Store Sto = GetStoreInfo (obje);
//		//
//		//
//		//							//var task = GetStoreInfo (ti.storeID);
//		//							//Store Sto = await task;
//		//							monkey.Store_Address = Sto.Address;
//		//							monkey.Store_Phone = Sto.PhoneNumber;
//		//							monkey.Store_Name = Sto.StoreName;
//		//							monkey.Store_City = Sto.StoreCity;
//		//							Console.WriteLine (monkey.Store_City);
//		//							monkey.Amount_HT = total_amount_TTC/(1+monkey.TVA/100);
//		//							monkey.Amount_TTC = total_amount_TTC;
//		//							monkey.Date = ti.transactionDate;
//		//						}
//		//					}
//		//				}
//		//				monkey.Insert(0, new itu ());
//		//				monkey.Add (new itu ());
//		//
//		//			}
//		//			return MonkeysGrouped;
//		//		}
//		//
//
//		void AddTabToActionBar(string text, int iconResourceId)
//		{
//			ActionBar.Tab tab = ActionBar.NewTab()
//				.SetText(text)
//				.SetIcon(iconResourceId);
//			tab.TabSelected += TabOnTabSelected;
//			ActionBar.AddTab(tab);
//		}
//
//		void TabOnTabSelected(object sender, ActionBar.TabEventArgs tabEventArgs)
//		{
//			ActionBar.Tab tab = (ActionBar.Tab)sender;
//
//			Log.Debug(Tag, "The tab {0} has been selected.", tab.Text);
//			Fragment frag = _fragments[tab.Position];
//
//			tabEventArgs.FragmentTransaction.Replace(Resource.Id.frameLayout1, frag);
//		}
//	}
//}
