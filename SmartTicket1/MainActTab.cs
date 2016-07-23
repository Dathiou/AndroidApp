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
//using Android.Support.V4.View;
//using Android.Support.V4.App;
////using Android.Support.V7.AppCompat;
////using Android.Support.V7.App;
//using Android.Support.Design.Widget;
//using Parse;
//using System.Threading.Tasks;
//using System.Collections.ObjectModel;
//
//namespace SmartTicket1
//{
//	[Activity (Label = "MainActTab")]			
//	public class MainActTab : FragmentActivity 
//	{
//		protected void onCreate(Bundle savedInstanceState) {
//			base.OnCreate(savedInstanceState);
//			SetContentView(Resource.Layout.vpager);
//
//			// Get the ViewPager and set it's PagerAdapter so that it can display items
//			ViewPager viewPager = FindViewById<ViewPager>(Resource.Id.viewpager);
//			var adaptor = new SampleFragmentPagerAdapter(SupportFragmentManager);
//
//			adaptor.AddFragment (new test());
//			adaptor.AddFragment (new test());
//		
//
//			viewPager.Adapter = adaptor; 
//
//			// Give the TabLayout the ViewPager
//			TabLayout tabLayout =  FindViewById<TabLayout>(Resource.Id.sliding_tabs);
//			tabLayout.SetupWithViewPager(viewPager);
//
//			viewPager.SetOnPageChangeListener(new ViewPageListenerForActionBar(ActionBar));
//
//			ActionBar.AddTab(viewPager.GetViewPageTab(ActionBar, "Tab1"));
//			ActionBar.AddTab(viewPager.GetViewPageTab(ActionBar, "Tab2"));
//
//		}
//
//
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
//	}
//	public class SampleFragmentPagerAdapter : FragmentPagerAdapter
//	{
//		private  List<Android.Support.V4.App.Fragment> _fragmentList = new List<Android.Support.V4.App.Fragment>();
//
//		public SampleFragmentPagerAdapter(Android.Support.V4.App.FragmentManager fm) : base(fm)
//		{
//
//		}
//
//		public override int Count
//		{
//			get { return _fragmentList.Count; }
//		}
//
//		public override Android.Support.V4.App.Fragment GetItem(int position)
//		{
//			return _fragmentList[position];
//		}
//
//		public void AddFragment(Android.Support.V4.App.Fragment fragment)
//		{
//			_fragmentList.Add(fragment);
//		}
//
//		//		public override CharSequence GetPageTitle(int position) {
//		//			// Generate title based on item position
//		//			return tabTitles[position].ToString;
//		//		}
//
//		//		public override int Count
//		//		{
//		//			get { return tabTitles.Count; }
//		//		}
//		//
//		//		public override bool IsViewFromObject(View view, Java.Lang.Object obj)
//		//		{
//		//			return view == obj;
//		//		}
//		//
//		//		public override Java.Lang.Object InstantiateItem(ViewGroup container, int position)
//		//		{
//		//			return _fragments[position];
//		//		}
//		//
//		//		public string GetHeaderTitle (int position)
//		//		{
//		//			return tabTitles[position].ToString;
//		//		}
//		//
//		//		public override void DestroyItem(ViewGroup container, int position, Java.Lang.Object obj)
//		//		{
//		//			container.RemoveView((View)obj);
//		//		}
//	}
//
//	public class ViewPageListenerForActionBar : ViewPager.SimpleOnPageChangeListener
//	{
//		private ActionBar _bar;
//		public ViewPageListenerForActionBar(ActionBar bar)
//		{
//			_bar = bar;
//		}
//		public override void OnPageSelected(int position)
//		{
//			_bar.SetSelectedNavigationItem(position);
//		}
//	}
//	public static class ViewPagerExtensions
//	{
//		public static ActionBar.Tab GetViewPageTab(this ViewPager viewPager, ActionBar actionBar, string name)
//		{
//			var tab = actionBar.NewTab();
//			tab.SetText(name);
//			tab.TabSelected += (o, e) =>
//			{
//				viewPager.SetCurrentItem(actionBar.SelectedNavigationIndex, false);
//			};
//			return tab;
//		}
//	}
//}
//
