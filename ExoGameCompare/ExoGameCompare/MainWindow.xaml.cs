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
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ExoGameCompare
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        uint pageIndex = 0;
        

        public MainWindow()
        {
            InitializeComponent();

            NetworkAPI.OnDealCatch += (_deal) =>
            {
                gameDealGrid.ItemsSource = _deal;
            };

            gameDealGrid.MouseDoubleClick += (o, e) =>
            {
                gameDealWindow _window = new gameDealWindow();
                _window.SetCurrentDeal((Deal)gameDealGrid.SelectedItem);
                _window.Show();

            };
        }

        private void OnDealButtonClick(object sender, RoutedEventArgs e)
        {
            NetworkAPI.GetRandomDeals(pageIndex, researchBox.Text);
        }

        private void OnPreviousPageButonClick(object sender, RoutedEventArgs e)
        {
            if (pageIndex == 0)
                return;

            pageIndex--;
            NetworkAPI.GetRandomDeals(pageIndex, researchBox.Text);
        }

        private void OnNextPageButtonClick(object sender, RoutedEventArgs e)
        {
            pageIndex++;
            NetworkAPI.GetRandomDeals(pageIndex, researchBox.Text);
        }
    }
}
