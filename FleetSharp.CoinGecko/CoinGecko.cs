using System.Net.Http.Json;

namespace FleetSharp.CoinGecko
{
    internal class CoinGeckoErgoPriceClass
    {
        public double? usd { get; set; }
    }
    internal class CoinGeckoErgoPriceResult
    {
        public CoinGeckoErgoPriceClass? ergo { get; set; }
    }

    public static class CoinGecko
    {
        private static HttpClient client = new HttpClient();
        private static DateTime dtLastUpdate = DateTime.MinValue;
        private static double lastPrice = 0;

        public static string CoinGeckoAPI = "https://api.coingecko.com/api/v3/simple/price?ids=ergo&vs_currencies=usd";
        public static int cacheResultsForXSeconds = 60 * 15;
        public static async Task<double?> GetCurrentERGPriceInUSDCached()
        {
            if (lastPrice == 0 || (DateTime.Now - dtLastUpdate).TotalSeconds >= cacheResultsForXSeconds)
            {
                try
                {
                    CoinGeckoErgoPriceResult? ret = await client.GetFromJsonAsync<CoinGeckoErgoPriceResult>(CoinGeckoAPI);
                    if (ret != null) lastPrice = ret?.ergo?.usd ?? 0;
                }
                catch (Exception e)
                {

                }
                
            }
            return lastPrice;
        }
    }
}