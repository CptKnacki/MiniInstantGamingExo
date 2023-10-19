using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExoGameCompare
{
    internal class API_CheapShark
    {
        const string BASE_URL = "https://www.cheapshark.com/api/1.0/deals?";
        const string BASE_URL_FOR_STORES = "https://www.cheapshark.com";

        public static string GetDealPage(uint _pageNumber = 0) => $"{BASE_URL}pageNumber={_pageNumber}";
        public static string GetDealByName(string _gameName = "", uint _pageNumber = 0) => $"{BASE_URL}title={_gameName}&pageNumber={_pageNumber}";
        public static string GetStores() => $"{BASE_URL_FOR_STORES}/api/1.0/stores";

        public static string GetDealByGameID(int _gameID) => $"{BASE_URL_FOR_STORES}/api/1.0/games?id={_gameID}";
        public static string GetStoreLogoFromID(int _ID) => $"{BASE_URL_FOR_STORES}/img/stores/logos/{_ID}.png";

        public static string GetStoreDealURL(string _dealID) => $"{BASE_URL_FOR_STORES}/redirect?dealID={_dealID}";
    }
}
