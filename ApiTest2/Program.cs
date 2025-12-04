using Newtonsoft.Json;
namespace ApiTest2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("http://localhost:5155/");
            var response = httpClient.GetAsync("api/Especies").Result;
            var json = response.Content.ReadAsStringAsync().Result;
            var especiesList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Modelos.Especie>>(json);
            Console.WriteLine(json);
            Console.ReadLine();  
        }
    }
}
