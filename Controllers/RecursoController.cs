using Microsoft.AspNetCore.Mvc;
using ONG.Models;
using ProyectoONGDBNoSQL.Data;

namespace ProyectoONGDBNoSQL.Controllers
{
    public class RecursoController : Controller
    {
        private readonly RecursoRepository _recursoRepository;

        public RecursoController(RecursoRepository recursoRepository)
        {
            _recursoRepository = recursoRepository;
        }

        public async Task<IActionResult> Index()
        {
            var recursos = await _recursoRepository.GetAllAsync();
            return View(recursos);
        }

        public IActionResult Create()
        {
            return View();
        }
[HttpPost]
public async Task<IActionResult> Create(Recurso recurso)
{
    Console.WriteLine(">>> Insertando recurso: " + recurso.NombreRecurso);

    await _recursoRepository.CreateAsync(recurso);
    return RedirectToAction(nameof(Index));
}


        public async Task<IActionResult> Edit(string id)
        {
            var recurso = await _recursoRepository.GetByIdAsync(id);
            if (recurso == null) return NotFound();
            return View(recurso);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, Recurso recurso)
        {
            if (ModelState.IsValid)
            {
                await _recursoRepository.UpdateAsync(id, recurso);
                return RedirectToAction(nameof(Index));
            }
            return View(recurso);
        }

        public async Task<IActionResult> Delete(string id)
        {
            var recurso = await _recursoRepository.GetByIdAsync(id);
            if (recurso == null) return NotFound();
            return View(recurso);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            await _recursoRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
