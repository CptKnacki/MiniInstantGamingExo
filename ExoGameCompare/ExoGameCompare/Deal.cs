using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ExoGameCompare
{
    public class Deal
    {
        public string DealID { get; set; } // POUR LA PAGE DU DEAL SEUL
        public string MetacriticScore { get; set; }
        public string SteamRatingText { get; set; }
        public string SteamRatingPercent { get; set; }


        // LES 5 POUR LE TABLEAUX DE BASE
        public string Title { get; set; }
        public string SalePrice { get; set; }
        public string NormalPrice { get; set; }
        public string Savings { get; set; }
        public int SavingsInt { get; set; }
        public string Thumb { get; set; }
        public Image GameThumb { get; set; } = new Image();

        public string GameID { get; set; } 


         
    }
}
