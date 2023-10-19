using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Newtonsoft.Json;

namespace ExoGameCompare
{
    internal class NetworkAPI
    {
        public static event Action<Deal[]> OnDealCatch = null;
        public static event Action<Store[]> OnStoresCatch = null;

        public static async void GetRandomDeals(uint _pageNumber = 0, string _title ="")
        {
            HttpClient _request = new HttpClient(); // Ne pas en recréer dans des boucles //
            HttpResponseMessage _msg = await _request.GetAsync(API_CheapShark.GetDealByName(_title, _pageNumber));
            string _result = await _msg.Content.ReadAsStringAsync();

            Deal[] _dealPage = JsonConvert.DeserializeObject<Deal[]>(_result);

            DealTreatment(_dealPage);

        }

        public static void DealTreatment(Deal[] _dealPage)
        {

            for (int i = 0; i < _dealPage.Length; i++)
            {
                ImageSourceConverter _converter = new ImageSourceConverter();
                _dealPage[i].GameThumb.Source = (ImageSource)_converter.ConvertFromString(_dealPage[i].Thumb); 

                float.TryParse(_dealPage[i].SalePrice, out float _euroSalePrice);
                _dealPage[i].SalePrice += $"$ ({(int)(_euroSalePrice * 1.05)}€)";

                float.TryParse(_dealPage[i].NormalPrice, out float _euroNormalPrice);
                _dealPage[i].NormalPrice += $"$ ({(int)(_euroNormalPrice * 1.05)}€)";

                float.TryParse(_dealPage[i].Savings, out float _savings);
                _dealPage[i].Savings = $" Saving {(int)_savings}%";
                _dealPage[i].SavingsInt = (int)_savings;
            }


            OnDealCatch?.Invoke(_dealPage);

        }

        public static async void GetStoresDataFromGameID(int _gameID)
        {

            HttpClient _request = new HttpClient(); // Ne pas en recréer dans des boucles //
            HttpResponseMessage _msg = await _request.GetAsync(API_CheapShark.GetDealByGameID(_gameID));
            string _result = await _msg.Content.ReadAsStringAsync();

            DealStore _dealStore = JsonConvert.DeserializeObject<DealStore>(_result);


            HttpResponseMessage _msgStoresDB = await _request.GetAsync(API_CheapShark.GetStores());
            string _resultStoreDB = await _msgStoresDB.Content.ReadAsStringAsync();

            Store[] _storesDB = JsonConvert.DeserializeObject<Store[]>(_resultStoreDB);

            StoresDataTreatment(_dealStore, _storesDB);
        }


        public static void StoresDataTreatment(DealStore _dealStore, Store[] _storesDataBase)
        {

            for (int i = 0; i < _storesDataBase.Length; i++)
            {
                for (int x = 0; x < _dealStore.Deals.Length; x++)
                {
                    if (_dealStore.Deals[x].StoreID == _storesDataBase[i].StoreID)
                        _dealStore.Deals[x].StoreName = _storesDataBase[i].StoreName;
                }
            }


            for (int i = 0; i < _dealStore.Deals.Length; i++)
            {
                int.TryParse(_dealStore.Deals[i].StoreID, out int _storeIDInt);

                ImageSourceConverter _converter = new ImageSourceConverter();
                _dealStore.Deals[i].Logo.Source = (ImageSource)_converter.ConvertFromString(API_CheapShark.GetStoreLogoFromID(_storeIDInt - 1));

                float.TryParse(_dealStore.Deals[i].Price, out float _euroPrice);
                _dealStore.Deals[i].Price += $"$ ({(int)(_euroPrice * 1.05)}€)";

                float.TryParse(_dealStore.Deals[i].Savings, out float _savings);
                _dealStore.Deals[i].Savings = $" Saving {(int)_savings}%";
                _dealStore.Deals[i].SavingsInt = (int)_savings;


            }



            OnStoresCatch?.Invoke(_dealStore.Deals);

        }

    }
}
