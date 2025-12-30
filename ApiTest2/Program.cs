using Modelos;
using Newtonsoft.Json;
using Zoologico.ApiConsumer;
namespace ApiTest2

{
    internal class Program
    {
        static void Main(string[] args)
        {
            Crud<Raza>.UrlBase = "http://10.241.253.223/api/Razas";
            Crud<Especie>.UrlBase = "http://10.241.253.223/api/Especies";
            // insertar una especie
            var nuevaEspecie = new Especie
            {
                Codigo = 0,
                NombreComun = "León"
            };
            var apiResult = Crud<Especie>.Create(nuevaEspecie);
            var especies = Crud<Especie>.ReadAll();

            nuevaEspecie = apiResult.Data;
            nuevaEspecie.NombreComun = "León Modificado";
            Crud<Especie>.Update(nuevaEspecie.Codigo, nuevaEspecie);

            var unaEspecie = Crud<Especie>.ReadBy("Codigo", "12");
            Crud<Especie>.Delete("12");

            Console.WriteLine(apiResult);
            Console.ReadLine();


        }
    }
}
