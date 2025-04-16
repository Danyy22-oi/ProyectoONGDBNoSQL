using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProyectoONGDBNoSQL.Models;
using ProyectoONGDBNoSQL.Repositories;
using ProyectoONGDBNoSQL.ViewModels;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoONGDBNoSQL.Controllers
{
    public class ComunicacionesController : Controller
    {
        private readonly ComunicacionRepository _comunicacionRepository;
        private readonly UsuarioRepository _usuarioRepository;

        public ComunicacionesController(
            ComunicacionRepository comunicacionRepository,
            UsuarioRepository usuarioRepository)
        {
            _comunicacionRepository = comunicacionRepository;
            _usuarioRepository = usuarioRepository;
        }

        // GET: Comunicaciones
        public async Task<IActionResult> Index()
        {
            var comunicaciones = await _comunicacionRepository.GetAllAsync();
            return View(comunicaciones);
        }

        // GET: Comunicaciones/Details/{id}
        public async Task<IActionResult> Details(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return BadRequest();

            var comunicacion = await _comunicacionRepository.GetByIdAsync(id);
            if (comunicacion == null)
                return NotFound();

            return View(comunicacion);
        }

        // GET: Comunicaciones/Create
        public async Task<IActionResult> Create()
        {
            var usuarios = await _usuarioRepository.GetAllAsync();
            var viewModel = new ComunicacionViewModel
            {
                UsuariosList = usuarios.Select(u => new SelectListItem
                {
                    Value = u.Id,
                    Text = $"{u.Nombre} {u.Apellido}"
                })
            };
            return View(viewModel);
        }

        // POST: Comunicaciones/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ComunicacionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var usuarios = await _usuarioRepository.GetAllAsync();
                model.UsuariosList = usuarios.Select(u => new SelectListItem
                {
                    Value = u.Id,
                    Text = $"{u.Nombre} {u.Apellido}"
                });
                return View(model);
            }

            var comunicacion = new Comunicacion
            {
                Contenido = model.Contenido,
                FechaCreacion = model.FechaCreacion,
                UsuarioId = model.UsuarioIds
            };

            await _comunicacionRepository.CreateAsync(comunicacion);
            return RedirectToAction(nameof(Index));
        }

        // GET: Comunicaciones/Edit/{id}
        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return BadRequest();

            var comunicacion = await _comunicacionRepository.GetByIdAsync(id);
            if (comunicacion == null)
                return NotFound();

            var usuarios = await _usuarioRepository.GetAllAsync();
            var viewModel = new ComunicacionViewModel
            {
                Id = comunicacion.Id,
                Contenido = comunicacion.Contenido,
                FechaCreacion = comunicacion.FechaCreacion,
                UsuariosList = usuarios.Select(u => new SelectListItem
                {
                    Value = u.Id,
                    Text = $"{u.Nombre} {u.Apellido}"
                })
            };

            return View(viewModel);
        }

        // POST: Comunicaciones/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, ComunicacionViewModel model)
        {
            if (id != model.Id)
                return BadRequest();

            if (!ModelState.IsValid)
            {
                var usuarios = await _usuarioRepository.GetAllAsync();
                model.UsuariosList = usuarios.Select(u => new SelectListItem
                {
                    Value = u.Id,
                    Text = $"{u.Nombre} {u.Apellido}"
                });
                return View(model);
            }

            var comunicacionToUpdate = new Comunicacion
            {
                Id = model.Id,
                Contenido = model.Contenido,
                FechaCreacion = model.FechaCreacion,
                UsuarioId = model.UsuarioIds
            };

            await _comunicacionRepository.UpdateAsync(id, comunicacionToUpdate);
            return RedirectToAction(nameof(Index));
        }

        // GET: Comunicaciones/Delete/{id}
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return BadRequest();

            var comunicacion = await _comunicacionRepository.GetByIdAsync(id);
            if (comunicacion == null)
                return NotFound();

            return View(comunicacion);
        }

        // POST: Comunicaciones/Delete/{id}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            await _comunicacionRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}