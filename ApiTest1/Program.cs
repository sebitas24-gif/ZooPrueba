using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;


namespace ApiTest1
{
    class Program
    {
        static void Main(string[] args)
        {
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:7241/");
            var response = httpClient.GetAsync("api/Animales").Result;
            var json = response.Content.ReadAsStringAsync().Result;
            var especies = Newtonsoft.Json.JsonConvert.DeserializeObject<Modelos.ApiResult<List<Modelos.Especie>>>(json);
            Console.WriteLine(json);
            Console.ReadLine();
        }
    }
}
