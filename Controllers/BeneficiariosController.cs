using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MongoDB.Bson;
using ProyectoONGDBNoSQL.Models;
using ProyectoONGDBNoSQL.Repositories;
using ProyectoONGDBNoSQL.ViewModels;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoONGDBNoSQL.Controllers
{
    public class BeneficiariosController : Controller
    {
        private readonly BeneficiarioRepository _beneficiarioRepository;
        private readonly ProyectoRepository _proyectoRepository;


        public BeneficiariosController(
            BeneficiarioRepository beneficiarioRepository,
            ProyectoRepository proyectoRepository)
        {
            _beneficiarioRepository = beneficiarioRepository;
            _proyectoRepository = proyectoRepository;
        }

        // GET: Beneficiarios/Index
        public async Task<IActionResult> Index()
        {
            var beneficiarios = await _beneficiarioRepository.GetAllAsync();
            return View(beneficiarios);
        }

        // GET: Beneficiarios/Details/{id}
        public async Task<IActionResult> Details(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return BadRequest();

            var beneficiario = await _beneficiarioRepository.GetByIdAsync(id);
            if (beneficiario == null)
                return NotFound();

            return View(beneficiario);
        }

        // GET: Beneficiarios/Create
        public async Task<IActionResult> Create()
        {

            var beneficiarios = await _beneficiarioRepository.GetAllAsync();
            var viewModel = new BeneficiarioViewModel
            {
                BeneficiariosList = beneficiarios.Select(u => new SelectListItem
                {
                    Value = u.Id,
                    Text = $"{u.Nombre} {u.Contacto}"
                })
            };
            return View(viewModel);
        }
        // POST: Beneficiarios/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BeneficiarioViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var beneficiariosReload = await _beneficiarioRepository.GetAllAsync();
                model.BeneficiariosList = beneficiariosReload.Select(u => new SelectListItem
                {
                    Value = u.Id,
                    Text = $"{u.Nombre} {u.Contacto}"
                });
                return View(model);
            }

            var beneficiario = await _beneficiarioRepository.GetByIdAsync(model.Id);
            if (beneficiario != null)
            {
                // El beneficiario ya existe, podrías redirigir o mostrar error
                ModelState.AddModelError("BeneficiarioID", "El beneficiario ya existe.");
                var beneficiariosReload = await _beneficiarioRepository.GetAllAsync();
                model.BeneficiariosList = beneficiariosReload.Select(u => new SelectListItem
                {
                    Value = u.Id,
                    Text = $"{u.Nombre} {u.Contacto}"
                });
                return View(model);
            }

            // Crear nuevo beneficiario
            var newBeneficiario = new Beneficiario
            {
                Id = model.Id,
                Nombre = model.Nombre,
                Contacto = model.Contacto,
                Ubicacion = model.Ubicacion,
                Necesidades = model.Necesidades?.Split(',').ToList() ?? new List<string>(),
                Proyectos = model.Proyectos != null 
                    ? model.Proyectos.Split(',').Select(ObjectId.Parse).ToList() 
                    : new List<ObjectId>()
            };

            await _beneficiarioRepository.CreateAsync(newBeneficiario);
            return RedirectToAction(nameof(Index));
        }


        // GET: Beneficiarios/Edit/{id}
        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return BadRequest();
            var beneficiario = await _beneficiarioRepository.GetByIdAsync(id);
            if (beneficiario == null)
                return NotFound();

            var proyectos = await _proyectoRepository.GetAllAsync();
            var viewModel = new BeneficiarioViewModel
            {
                Id = beneficiario.Id,
                Nombre = beneficiario.Nombre,
                Contacto = beneficiario.Contacto,
                Ubicacion = beneficiario.Ubicacion,
                Necesidades = beneficiario.Ubicacion,
                ProyectosList = proyectos.Select(u => new SelectListItem
                {
                    Value = u.Id,
                    Text = $"{u.NombreProyecto} {u.Descripcion}"
                })
            };

            return View(viewModel);
        }

        // POST: Beneficiarios/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, BeneficiarioViewModel model)
        {
            if (id != model.Id)
                return BadRequest();

            if (!ModelState.IsValid)
            {
                var beneficiarios = await _beneficiarioRepository.GetAllAsync();
                model.BeneficiariosList = beneficiarios.Select(u => new SelectListItem
                {
                    Value = u.Id,
                    Text = $"{u.Nombre} {u.Contacto}"
                });
                return View(model);
            }

            var beneficiarioToUpdate = new Beneficiario
            {
                Id = model.Id,
                Nombre = model.Nombre,
                Contacto = model.Contacto,
                Ubicacion = model.Ubicacion,
                Necesidades = model.Necesidades?.Split(',').ToList() ?? new List<string>(),
                Proyectos = model.Proyectos?.Split(',').Select(ObjectId.Parse).ToList() ?? new List<ObjectId>()
            };
            await _beneficiarioRepository.UpdateAsync(id, beneficiarioToUpdate);
            return RedirectToAction(nameof(Index));
        }

        // GET: Beneficiarios/Delete/{id}
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return BadRequest();
            var beneficiario = await _beneficiarioRepository.GetByIdAsync(id);
            if (beneficiario == null)
                return NotFound();
            return View(beneficiario);
        }

        // POST: Beneficiarios/Delete/{id}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return BadRequest();
            await _beneficiarioRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
