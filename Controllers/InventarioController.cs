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
            var inventarios = await _inventarioRepository.GetAllAsync();
            var recursos = await _recursoRepository.GetAllAsync();
            // 🔍 Verificar qué nombres están llegando
            foreach (var r in recursos)
            {
                Console.WriteLine($"ID: {r.IdRecurso}, Nombre: {r.NombreRecurso}");
            }
            // Crear diccionario { id => nombre }
            var recursoNombres = recursos.ToDictionary(r => r.IdRecurso.ToString(), r => r.NombreRecurso);


            // Pasar inventario y nombres al ViewBag
            ViewBag.RecursoNombres = recursoNombres;

            return View(inventarios);
        }
        public async Task<IActionResult> Create()
        {
            var recursos = await _recursoRepository.GetAllAsync();
            ViewBag.Recursos = new SelectList(recursos, "IdRecurso", "NombreRecurso");
            return View(new Inventario());
        }

        [HttpPost]
        public async Task<IActionResult> Create(Inventario inventario)
        {
            Console.WriteLine(">>> LLEGÓ AL MÉTODO POST DE INVENTARIO");
            Console.WriteLine($"Recurso ID recibido: {inventario.IdRecurso}");

            if (!ModelState.IsValid)
            {
                Console.WriteLine(">>> ❌ MODELSTATE NO ES VÁLIDO");
                foreach (var key in ModelState.Keys)
                {
                    var state = ModelState[key];
                    foreach (var error in state.Errors)
                    {
                        Console.WriteLine($"❌ Campo: {key}, Error: {error.ErrorMessage}");
                    }
                }

                var recursos = await _recursoRepository.GetAllAsync();
                ViewBag.Recursos = new SelectList(recursos, "IdRecurso", "NombreRecurso");
                return View(inventario);
            }

            await _inventarioRepository.CreateAsync(inventario);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(string id)
        {
            var inventario = await _inventarioRepository.GetByIdAsync(id);
            if (inventario == null) return NotFound();

            var recursos = await _recursoRepository.GetAllAsync();
            ViewBag.Recursos = new SelectList(recursos, "IdRecurso", "NombreRecurso", inventario.IdRecurso);

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
