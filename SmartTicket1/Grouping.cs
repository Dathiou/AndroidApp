
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



namespace SmartTicket1
{
	[Activity (Label = "Grouping")]			
	public class Grouping<K, T> : List<T> { 
		public K TransKey { get; private set; } 

		public IEnumerable<T> items { get; set; }
		public string Trans { get; set; }
		public string Date { get; set; }
		public bool isNF { get; set;}
		public double Amount_HT { get; set; }
		public double Amount_TTC { get; set; }
		public string Store_Address{ get; set; }
		public string Store_Phone{ get; set; }
		public string Store_Name{ get; set; }
		public string Store_City{ get; set; }
		public string objId { get; set; }
		public double TVA;


	//	public Grouping(K key1,K key2,K key3,string trans, string date, string store, IEnumerable<T> items) { 
		public Grouping(K key1,string trans, IEnumerable<T> items) { 
			TransKey = key1;
			//DateKey = key2;
			//StoreKey = key3;
			Trans = trans;
			Date=null;	
			TVA = 19.60;

			//Amount = null;


			foreach (var item in items) 
				this.Add(item); 
		}


//		public  void CompleteCollection(Grouping<string, itu> group,string store, string date, double amount){
//			
//			group.Store = store;
//			group.Date = date;
//			group.Amount_HT = amount;		
//
//
//		}

	}


	public class itu
	{

		public string desc {get;set;}
		public string trans { get; set; }
		public string transactionDate { get; set; }
		//public string store { get; set; }
		public double amount { get; set; }
		public int qty { get; set; }

	}
	public class tran {
		public string trans {get;set;}

		public string transactionDate { get; set; }
		public string storeID { get; set; }
		public double amount { get; set; }
		public string objId { get; set; }
		public bool isNF { get; set;}

	}
	public class Store {
		public string StoreID {get;set;}
		public string StoreName {get;set;}
		public string StoreCity {get;set;}
		public string Address { get; set; }
		public string PhoneNumber { get; set; }


	}
} 

