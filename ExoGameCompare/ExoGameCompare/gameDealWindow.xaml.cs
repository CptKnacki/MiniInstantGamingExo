using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Diagnostics;

namespace ExoGameCompare
{
    /// <summary>
    /// Interaction logic for gameDealWindow.xaml
    /// </summary>
    public partial class gameDealWindow : Window
    {

         Deal currentDeal = null;

        public gameDealWindow()
        {
            InitializeComponent();

            NetworkAPI.OnStoresCatch += (_stores) =>
            {
                storeGrid.ItemsSource = _stores;
            };

            storeGrid.MouseDoubleClick += (o, e) =>
            {
                string _dealURL = API_CheapShark.GetStoreDealURL(((Store)storeGrid.SelectedItem).DealID);
                Process.Start(_dealURL);
            };
        }

        private void NetworkAPI_OnStoresCatch(Store[] obj)
        {
            throw new NotImplementedException();
        }

        public void SetCurrentDeal(Deal _deal)
        {
            currentDeal = _deal;

            ImageSourceConverter _converter = new ImageSourceConverter();
            gameThumbnail.Source = (ImageSource)_converter.ConvertFromString(currentDeal.Thumb);

            gameName.Content = currentDeal.Title;
            metacriticScore.Content = "Metacritics : " + currentDeal.MetacriticScore;
            steamScore.Content = "Steam review : " + currentDeal.SteamRatingPercent;
            steamReview.Content = "Average review : " + currentDeal.SteamRatingText;

            int.TryParse(_deal.GameID, out int _result);
            NetworkAPI.GetStoresDataFromGameID(_result);


        }

    }
}
