
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
using Parse;

namespace SmartTicket1
{
	[Activity (Label = "TransactionGroup")]			

//	Grouping<K, T> : ObservableCollection<T> { 
//		public K Key { get; private set; } 
//		public Grouping(K key, IEnumerable<T> items) { 
//			Key = key; 
//			foreach (var item in items) this.Items.Add(item);
//		} 
	public class TransactionGroup {
		public List<tran> Trans = new List<tran>();
		public async static Task<List<itu>> listoune ( string UserID){
			var transacNb = from transac in ParseObject.GetQuery ("Transactions")
			                where transac.Get<string> ("UserID") == UserID
			                select transac;
			var itemNb = from item in ParseObject.GetQuery ("Items")
				join transac in transacNb on item ["TransactionNb"] equals transac ["Transaction"] 
				select  item;
	//		itemNb.Include ("Store");
			IEnumerable<ParseObject> result1 = await transacNb.FindAsync (); // transactions correspondant a l'user
			IEnumerable<ParseObject> result2 = await itemNb.FindAsync (); //items correspondants à l'user
		
		
			List<itu> tot = new List<itu>();
			foreach (var obj in result2) {
				itu Ar = GetItu (obj);
				tot.Add (Ar);
			}
			return tot;
			}


//			public List<tran> Translate (IEnumerable<ParseObject> result1){
//			List<tran> Trans = new List<tran>();
//			foreach (var obje in result1) {
//				tran r = GetTrans (obje);
//				Trans.Add (r);
//			}
//			}


		public static itu GetItu (ParseObject obj){
				itu r = new itu();
				r.desc=obj.Get<string> ("ItemDesc");
				r.trans = obj.Get<string> ("TransactionNb");

			  	//r.transactionDate=obj.Get<DateTime> ("TransactionDate").ToString("ddMMyyyy"); 
//				foreach (tran ti in tr) {
//					if (ti.trans == r.trans)
//					{r.store = ti.store;
//					//r.transactionDate = ti.transactionDate;
//				}
				
					
				return r;

			}
		public static tran GetTrans (ParseObject obj){
			{
				tran r = new tran();

				r.trans = obj.Get<string> ("Transaction");
				r.store=obj.Get<string> ("Store");
				r.transactionDate=obj.Get<DateTime> ("TransactionDate").ToString("ddMMyyyy"); 
				r.amount=obj.Get<float> ("TotalPrice");
					return r;
					}
				}
			


		public static ObservableCollection<Grouping<string, itu>> GroupByTransaction(List<itu> listo)
		{
			var queryTransac = from itu in listo
			                   group itu by itu.trans into Itegroup
			                   select new Grouping<string, itu> (Itegroup.Key, Itegroup.Key, Itegroup.ToList ());
			
			ObservableCollection<Grouping<string, itu>> MonkeysGrouped = new ObservableCollection<Grouping<string, itu>> (queryTransac);
			return MonkeysGrouped;
		}

		public static void AddInfoGroup(ObservableCollection<Grouping<string, itu>> MonkeysGrouped,List<tran> Trans ){

			foreach (var monkey in MonkeysGrouped) {
				string trr = monkey.Trans;
				foreach (tran ti in Trans) {
					if (ti.trans == trr) {
						monkey.Store = ti.store;
						monkey.Amount = ti.amount;
						monkey.Date = ti.transactionDate;
					}
				}
			
			
				

		}


	}

	}}