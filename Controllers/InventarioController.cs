using Microsoft.AspNetCore.Mvc;
using ONG.Models;
using ProyectoONGDBNoSQL.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace ProyectoONGDBNoSQL.Controllers
{
    public class InventarioController : Controller
    {
        private readonly InventarioRepository _inventarioRepository;
        private readonly RecursoRepository _recursoRepository;


        public InventarioController(InventarioRepository inventarioRepository, RecursoRepository recursoRepository)
        {
            _inventarioRepository = inventarioRepository;
            _recursoRepository = recursoRepository;
        }

        public async Task<IActionResult> Index()
        {
            var inventario = await _inventarioRepository.GetAllAsync();
            return View(inventario);
        }
        public async Task<IActionResult> Create()
        {
            var recursos = await _recursoRepository.GetAllAsync();

            // Para el dropdown
            ViewBag.Recursos = new SelectList(recursos, "IdRecurso", "NombreRecurso");

            // Para el script de JavaScript
            var recursosJson = recursos.Select(r => new
            {
                IdRecurso = r.IdRecurso,
                NombreRecurso = r.NombreRecurso
            });

            var jsonOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = null
            };

            ViewBag.RecursosJson = JsonSerializer.Serialize(recursosJson, jsonOptions);

            return View(new Inventario());
        }
        [HttpPost]
        public async Task<IActionResult> Create(Inventario inventario)
        {
            Console.WriteLine(">>> LLEGÓ AL MÉTODO POST DE INVENTARIO");
            Console.WriteLine($"Recurso ID: {inventario.IdRecurso}");

            if (ModelState.IsValid)
            {
                await _inventarioRepository.CreateAsync(inventario);
                return RedirectToAction(nameof(Index));
            }

            var recursos = await _recursoRepository.GetAllAsync();
            ViewBag.Recursos = new SelectList(recursos, "IdRecurso", "NombreRecurso");
            ViewBag.ListaRecursos = recursos;
            return View(inventario);
        }



        [HttpPost]
        public async Task<IActionResult> Edit(string id, Inventario inventario)
        {
            if (ModelState.IsValid)
            {
                await _inventarioRepository.UpdateAsync(id, inventario);
                return RedirectToAction(nameof(Index));
            }
            return View(inventario);
        }

        public async Task<IActionResult> Delete(string id)
        {
            var inv = await _inventarioRepository.GetByIdAsync(id);
            if (inv == null) return NotFound();
            return View(inv);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            await _inventarioRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
