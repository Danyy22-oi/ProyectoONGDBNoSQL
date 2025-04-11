using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProyectoONGDBNoSQL.Models;
using ProyectoONGDBNoSQL.Repositories;
using ProyectoONGDBNoSQL.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoONGDBNoSQL.Controllers
{
    public class DonantesController : Controller
    {
        private readonly DonanteRepository _donanteRepository;

        public DonantesController(DonanteRepository donanteRepository)
        {
            _donanteRepository = donanteRepository;
        }

        public async Task<IActionResult> Index()
        {
            var donantes = await _donanteRepository.GetAllAsync();
            return View(donantes);
        }

        public async Task<IActionResult> Details(string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest();
            var donante = await _donanteRepository.GetByIdAsync(id);
            if (donante == null)
                return NotFound();
            return View(donante);
        }

        public IActionResult Create()
        {
            return NotFound("La creación manual de donantes está deshabilitada.");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(DonanteViewModel model)
        {
            return NotFound("La creación manual de donantes está deshabilitada.");
        }

        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest();
            var donante = await _donanteRepository.GetByIdAsync(id);
            if (donante == null)
                return NotFound();
            var viewModel = new DonanteViewModel
            {
                Id = donante.Id,
                Nombre = donante.Nombre,
                TipoDonante = donante.TipoDonante,
                Email = donante.Contacto?.Email,
                Telefono = donante.Contacto?.Telefono,
                TipoDonanteList = new List<SelectListItem>
                {
                    new SelectListItem { Value = "persona", Text = "Persona" },
                    new SelectListItem { Value = "organizacion", Text = "Organización" }
                }
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, DonanteViewModel model)
        {
            if (id != model.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                var existingDonante = await _donanteRepository.GetByIdAsync(id);
                if (existingDonante == null)
                    return NotFound();
                var updatedDonante = new Donante
                {
                    Id = existingDonante.Id,
                    UsuarioId = existingDonante.UsuarioId,
                    Nombre = model.Nombre,
                    TipoDonante = model.TipoDonante,
                    Contacto = new Donante.ContactoData
                    {
                        Email = model.Email,
                        Telefono = model.Telefono
                    },
                    Donaciones = existingDonante.Donaciones ?? new List<string>()
                };
                await _donanteRepository.UpdateAsync(id, updatedDonante);
                return RedirectToAction(nameof(Index));
            }
            model.TipoDonanteList = new List<SelectListItem>
            {
                new SelectListItem { Value = "persona", Text = "Persona" },
                new SelectListItem { Value = "organizacion", Text = "Organización" }
            };
            return View(model);
        }

        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest();
            var donante = await _donanteRepository.GetByIdAsync(id);
            if (donante == null)
                return NotFound();
            return View(donante);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest();
            await _donanteRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
