using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Modelos;
using Zoologico.ApiConsumer;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Zoologico.MVC.Controllers
{
    public class EspeciesController : Controller
    {
        // GET: EspeciesController
        public EspeciesController()
        {
            // IMPORTANTE: Si la API en el otro PC usa un puerto específico (ej. 5050), 
            // debes incluirlo: "http://10.241.253.223:5050/api/Especies"
            Zoologico.ApiConsumer.Crud<Modelos.Especie>.UrlBase = "http://10.241.253.223:5050/api/Especies";
        }
        public ActionResult Index()
        {
            var apiResult = Crud<Especie>.ReadAll();

            // Si apiResult es nulo o Data es nulo, mandamos lista vacía
            var modelo = apiResult?.Data ?? new List<Especie>();

            return View(modelo);

        }

        // GET: EspeciesController/Details/5
        public ActionResult Details(int id)
        {
            var result = Crud<Especie>.GetById(id);

            // Verificamos que no sea nulo antes de ir a la vista
            if (result == null || result.Data == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(result.Data);
        }

        // GET: EspeciesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EspeciesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Especie data)
        {
            try
            {
                var result = Crud<Especie>.Create(data);

                // Si la API devuelve el objeto creado, 'result.Data' no será nulo
                if (result != null && result.Data != null)
                {
                    return RedirectToAction(nameof(Index));
                }

                ModelState.AddModelError("", "La API no pudo guardar los datos.");
                return View(data);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error: " + ex.Message);
                return View(data);
            }
        }

        // GET: EspeciesController/Edit/5
        public ActionResult Edit(int id)
        {
            var result = Crud<Especie>.GetById(id);

            if (result == null || result.Data == null)
            {
                return NotFound();
            }

            return View(result.Data);
        }

        // POST: EspeciesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Especie data)
        {
            try
            {
                var result = Crud<Especie>.Update(id, data);

                if (result != null && result.Data != null)
                {
                    return RedirectToAction(nameof(Index));
                }
                return View(data);
            }
            catch
            {
                return View(data);
            }
        }

        // GET: EspeciesController/Delete/5
        public ActionResult Delete(int id)
        {
            var result = Crud<Especie>.GetById(id);

            // 2. Si no hay datos (API caída o ID inexistente), regresamos al Index
            if (result == null || result.Data == null)
            {
                return RedirectToAction(nameof(Index));
            }

            // 3. Enviamos SOLO la especie a la vista
            return View(result.Data);
        }

        // POST: EspeciesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Especie data)
        {
            try
            {
                var result = Crud<Especie>.Delete(id);

                if (result != null && result.Data == true) // Si Data es true, se eliminó bien
                {
                    return RedirectToAction(nameof(Index));
                }
                return View(data);
            }
            catch
            {
                return View(data);
            }
        }
    }
}
