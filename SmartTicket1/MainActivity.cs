using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;


using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Parse;


using Android.Util;
using Android.Support.V4.Widget;
using Android.Support.V4.View;

using System.Threading.Tasks;
using Android.Support.Design.Widget;

namespace SmartTicket1
{

	public interface DataTransferInterface { 
		void setValues();
	} 

	[Activity (Label = "SmartTicket")]	



	public class MainActivity : Android.Support.V4.App.FragmentActivity , DataTransferInterface
	{



		public static NoteTransactionExpandAdaptater NoteAdapter;


		public static noteFragment nF;





		public void setValues() {
			// TODO Auto-generated method stub 
		//	nF.refreshNotes();
		}  

		public static void setValuesm() {
			// TODO Auto-generated method stub 
			//noteFragment.refreshNotes();
		}  
//		static readonly string Tag = "ActionBarTabsSupport";
//
//		Fragment[] _fragments;
//		SwipeRefreshLayout refresher;



		protected async override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

//			SetContentView(Resource.Layout.MainBar);
//
			ActionBar.NavigationMode = ActionBarNavigationMode.Tabs;

//			Window.SetStatusBarColor (Android.Graphics.Color.ForestGreen);
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

			SetContentView(Resource.Layout.vpager);

			// Get the ViewPager and set it's PagerAdapter so that it can display items
			ViewPager viewPager = FindViewById<ViewPager>(Resource.Id.viewpager);
			var adaptor = new SampleFragmentPagerAdapter(SupportFragmentManager);
			ticketFragment tF = new ticketFragment ();
			nF = new noteFragment ();
			adaptor.AddFragment (new Profile());
			adaptor.AddFragment (tF);
			adaptor.AddFragment (nF);
			adaptor.AddFragment (new promoFragment());
			//FragmentManager fm = getSupportFragmentManager(); 
			viewPager.Adapter = adaptor; 

			// Give the TabLayout the ViewPager
			//TabLayout tabLayout =  FindViewById<TabLayout>(Resource.Id.sliding_tabs);
			//tabLayout.SetupWithViewPager(viewPager);

			viewPager.SetOnPageChangeListener(new ViewPageListenerForActionBar(ActionBar));


			ActionBar.AddTab(viewPager.GetViewPageTab(ActionBar, " PROFIL",Resource.Drawable.ic_perm_identity_white_24dp));
			ActionBar.AddTab(viewPager.GetViewPageTab(ActionBar, " TICKETS",Resource.Drawable.ic_receipt_white_24dp));
			ActionBar.AddTab(viewPager.GetViewPageTab(ActionBar, " NOTE DE FRAIS",Resource.Drawable.ic_card_travel_white_24dp));
			ActionBar.AddTab(viewPager.GetViewPageTab(ActionBar, " COUPONS",Resource.Drawable.ic_style_white_24dp));


		}
		/** 
     * Gets the fragment tag of a fragment at a specific position in the viewpager. 
     * 
     * @param pos the pos 
     * @return the fragment tag 
     */ 
		public String getFragmentTag(int pos){
			return "android:switcher:"+Resource.Id.viewpager+":"+pos;
		} 

//		public static void refreshNotes(){
//			//not.OnResume()
////			noteFragment currentFragment = FragmentManager.FindFragmentByTag(getFragmentTag(3));
////			FragmentTransaction fragTransaction = FragmentManager.BeginTransaction();
////			fragTransaction.Detach(currentFragment); 
////			fragTransaction.Attach(currentFragment); 
////			fragTransaction.Commit(); 
//
//			frag.getFragmentManager().beginTransaction().detach(frag).commit();
//			frag.getFragmentManager().beginTransaction().attach(frag).commit();
//		}


		public override bool OnCreateOptionsMenu (IMenu menu)
		{
			MenuInflater.Inflate (Resource.Menu.actions, menu);
			return base.OnCreateOptionsMenu (menu);
		}
		public  override bool OnOptionsItemSelected(IMenuItem item) {
			switch (item.ItemId) {
			case Resource.Id.SettingsAction:
				// User chose the "Settings" item, show the app settings UI... 

				var SettingsIntent = new Intent(this, typeof(SettingsActivity));
	
				StartActivity(SettingsIntent);
				return true; 

			case Resource.Id.LogOf:
				ParseUser.LogOut();
				var SignInIntent = new Intent(this, typeof(SignInActivity));

				StartActivity(SignInIntent);

				Toast.MakeText(this, "Déconnecté", ToastLength.Short).Show();
				// User chose the "Favorite" action, mark the current item 
				// as a favorite... 
				return true; 

			default: 
				// If we got here, the user's action was not recognized. 
				// Invoke the superclass to handle it. 
				return base.OnOptionsItemSelected(item);

			} 
		} 


		public async static Task<String> getUserID()
		{
			string UserId = ParseUser.CurrentUser.Get<string> ("UserID");
			return UserId;
		}

		public async static Task<BaseExpandableListAdapter> GetDataAll()
		{
			string UserId = ParseUser.CurrentUser.Get<string> ("UserID");
			string FirstName = ParseUser.CurrentUser.Get<string> ("FirstName");
			string LastName = ParseUser.CurrentUser.Get<string> ("LastName");

			List<tran> Trans = new List<tran>();
			var ListGrouped =  getlistAll (UserId,Trans);
			//await ListGrouped.GetAwaiter;
			ObservableCollection<Grouping<string, itu>> ListGrouped2 = await ListGrouped;
			BaseExpandableListAdapter AllAdapter = new AllTransactionExpandAdaptater (ListGrouped2);
			return AllAdapter;
		}
		public async static Task<BaseExpandableListAdapter> GetDataNote()
		{
			string UserId = ParseUser.CurrentUser.Get<string> ("UserID");
			string FirstName = ParseUser.CurrentUser.Get<string> ("FirstName");
			string LastName = ParseUser.CurrentUser.Get<string> ("LastName");

			List<tran> Trans = new List<tran>();
			var ListGrouped =  getlistNote (UserId,Trans);
			//await ListGrouped.GetAwaiter;
			ObservableCollection<Grouping<string, itu>> ListGrouped2 = await ListGrouped;
			NoteAdapter = new NoteTransactionExpandAdaptater (ListGrouped2);
			return NoteAdapter;
		}

		public async static Task<ObservableCollection<Grouping<string,itu>>> getlistAll(string UserId,List<tran> Trans)
		{
			var liliTask = listouneAll (UserId,Trans);
			List<itu> lili = await liliTask;
			var lolot = GroupByTransaction (lili,Trans);
			ObservableCollection<Grouping<string, itu>> lolo = await lolot;
			return lolo;
		}
		public async static Task<ObservableCollection<Grouping<string,itu>>> getlistNote(string UserId,List<tran> Trans)
		{
			var liliTask = listouneNote (UserId,Trans);
			List<itu> lili = await liliTask;
			var lolot = GroupByTransaction (lili,Trans);
			ObservableCollection<Grouping<string, itu>> lolo = await lolot;
			return lolo;
		}


		public async static Task<List<itu>> listouneAll ( string UserID,List<tran> Trans ){
			var transacNb = from transac in ParseObject.GetQuery ("Transactions")
					where transac.Get<string> ("UserID") == UserID
				select transac;
			var itemNb = from item in ParseObject.GetQuery ("Items")
				join transac in transacNb on item ["TransactionNb"] equals transac ["Transaction"] 
				select  item;

			IEnumerable<ParseObject> result1 = await transacNb.FindAsync (); // transactions correspondant a l'user
			IEnumerable<ParseObject> result2 = await itemNb.FindAsync ();   //items correspondants à l'user

			//Crée la liste des transactions
			foreach (var obje in result1) {
				tran r = GetTrans (obje);
				Trans.Add (r);
			}

			List<itu> tot = new List<itu>();
			//Crée liste d'items
			foreach (var obj in result2) {
				itu Ar = GetItu (obj);
				tot.Add (Ar);
			}

			return tot;
		}
		public async static Task<List<itu>> listouneNote ( string UserID,List<tran> Trans ){
			var transacNb = from transac in ParseObject.GetQuery ("Transactions")
					where transac.Get<string> ("UserID") == UserID
					where transac.Get<bool> ("IsNote") == true
				select transac;
			var itemNb = from item in ParseObject.GetQuery ("Items")
				join transac in transacNb on item ["TransactionNb"] equals transac ["Transaction"] 
				select  item;

			IEnumerable<ParseObject> result1 = await transacNb.FindAsync (); // transactions correspondant a l'user
			IEnumerable<ParseObject> result2 = await itemNb.FindAsync ();   //items correspondants à l'user

			//Crée la liste des transactions
			foreach (var obje in result1) {
				tran r = GetTrans (obje);
				Trans.Add (r);
			}

			List<itu> tot = new List<itu>();
			//Crée liste d'items
			foreach (var obj in result2) {
				itu Ar = GetItu (obj);
				tot.Add (Ar);
			}

			return tot;
		}


		public static itu GetItu (ParseObject obj){
			itu r = new itu();
			r.desc=obj.Get<string> ("ItemDesc");
			r.trans = obj.Get<string> ("TransactionNb");
			r.amount= obj.Get<double> ("Price");
			r.qty = obj.Get<int> ("Quantity");

			return r;

		}
		public static tran GetTrans (ParseObject obj){
			{
				tran r = new tran();
				r.trans = obj.Get<string> ("Transaction");
				r.storeID=obj.Get<string> ("StoreID");
				r.transactionDate=obj.Get<DateTime> ("TransactionDate").ToString("dd/MM/yyyy"); 
				r.objId = obj.ObjectId;
				r.isNF = obj.Get<bool> ("IsNote");
				//r.amount=obj.Get<double> ("Amount");
				return r;
			}
		}

		public static Store GetStoreInfo(  ParseObject obje){
			Store sto = new Store();

			sto.Address=obje.Get<string> ("Address");
			sto.PhoneNumber = obje.Get<string> ("PhoneNumber");
			sto.StoreCity=obje.Get<string> ("City");
			sto.StoreName=obje.Get<string> ("StoreName");

			return sto;
		}

		public async static Task<ObservableCollection<Grouping<string, itu>>> GroupByTransaction(List<itu> listo,List<tran> Trans)
		{
			var queryTransac = from itu in listo
				group itu by itu.trans into Itegroup
				select new Grouping<string, itu> (Itegroup.Key, Itegroup.Key, Itegroup.ToList ());

			ObservableCollection<Grouping<string, itu>> MonkeysGrouped = new ObservableCollection<Grouping<string, itu>> (queryTransac);

			foreach (var monkey in MonkeysGrouped) {
				string trr = monkey.Trans;
				double total_amount_TTC = 0;
				foreach (var item in monkey) {
					total_amount_TTC = total_amount_TTC +item.amount;
				}
				foreach (tran ti in Trans) {
					if (ti.trans == trr) {
						Store sto = new Store();

						var store = from stor in ParseObject.GetQuery ("Stores")
								where stor.Get<string> ("StoreID") == ti.storeID
							select stor;

						IEnumerable<ParseObject> Store_info = await store.FindAsync ();

						foreach (var obje in Store_info) {
							Store Sto = GetStoreInfo (obje);

							monkey.Store_Address = Sto.Address;
							monkey.Store_Phone = Sto.PhoneNumber;
							monkey.Store_Name = Sto.StoreName;
							monkey.Store_City = Sto.StoreCity;
							Console.WriteLine (monkey.Store_City);
							monkey.Amount_HT = total_amount_TTC/(1+monkey.TVA/100);
							monkey.Amount_TTC = total_amount_TTC;
							monkey.Date = ti.transactionDate;
							monkey.objId = ti.objId;
							monkey.isNF= ti.isNF;
						}
					}
				}
				monkey.Insert(0, new itu ());
				monkey.Add (new itu ());

			}
			return MonkeysGrouped;
		}


//		public async Task<ObservableCollection<Grouping<string, itu>>> getdat()
//		{
//
//			string UserId = ParseUser.CurrentUser.Get<string> ("UserID");
//			string FirstName = ParseUser.CurrentUser.Get<string> ("FirstName");
//			string LastName = ParseUser.CurrentUser.Get<string> ("LastName");
//
//			List<tran> Trans = new List<tran>();
//			var ListGrouped =  getlist (UserId,Trans);
//	
//			ObservableCollection<Grouping<string, itu>> ListGroupedbla = await ListGrouped;
//			return ListGroupedbla;
//
//		}
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
	}
	public class ViewPageListenerForActionBar : ViewPager.SimpleOnPageChangeListener
	{
		private ActionBar _bar;
		public ViewPageListenerForActionBar(ActionBar bar)
		{
			_bar = bar;
		}
		public override void OnPageSelected(int position)
		{
			_bar.SetSelectedNavigationItem(position);
		}
	}
	public static class ViewPagerExtensions
	{
		public static ActionBar.Tab GetViewPageTab(this ViewPager viewPager, ActionBar actionBar, string name,int iconResourceId)
		{
			var tab = actionBar.NewTab();
			tab.SetText(name);
			tab.SetIcon (iconResourceId);
			tab.TabSelected += (o, e) =>
			{
				viewPager.SetCurrentItem(actionBar.SelectedNavigationIndex, false);
			};
			return tab;
		}
	}
	public class SampleFragmentPagerAdapter : Android.Support.V4.App.FragmentPagerAdapter
		{
			private  List<Android.Support.V4.App.Fragment> _fragmentList = new List<Android.Support.V4.App.Fragment>();
	
			public SampleFragmentPagerAdapter(Android.Support.V4.App.FragmentManager fm) : base(fm)
			{
	
			}
	
			public override int Count
			{
				get { return _fragmentList.Count; }
			}
	
			public override Android.Support.V4.App.Fragment GetItem(int position)
			{
				return _fragmentList[position];
			}
	
			public void AddFragment(Android.Support.V4.App.Fragment fragment)
			{
				_fragmentList.Add(fragment);
			}
	
			
		}


}
