using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ExoGameCompare
{
    public class Store
    {
        public string StoreName { get; set; }
        public string StoreID { get; set; }
        public Image Logo { get; set; } = new Image();
        public string Price { get; set; }
        public string RetailPrice { get; set; }
        public string Savings { get; set; }
        public int SavingsInt { get; set; }
        public string DealID { get; set; } 
    }

    public class DealStore
    {
        public Store[] Deals { get; set; }
    }
}
