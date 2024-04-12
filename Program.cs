using System.Text.Json;
using Newtonsoft.Json;
namespace json_dz2
{
    public class ExchangeRate
    {
        public int r030 { get; set; }
        public string txt { get; set; }
        public decimal rate { get; set; }
        public string cc { get; set; }
        public string exchangedate { get; set; }
    }
    internal class Program
    {
        static async Task Main()
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("https://bank.gov.ua/NBUStatService/v1/statdirectory/exchange?json");
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            List<ExchangeRate> exchangeRates = JsonConvert.DeserializeObject<List<ExchangeRate>>(responseBody);

            Console.WriteLine("Available currencies:");
            foreach (var rate in exchangeRates)
            {
                Console.WriteLine($"Currency: {rate.r030} ({rate.txt})");
                Console.WriteLine($"Rate: {rate.rate} UAH");
                Console.WriteLine($"cc: {rate.cc}");
                Console.WriteLine($"Exchange date: {rate.exchangedate}");
                Console.WriteLine();
            }
        }
    }
}
