using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProyectoONGDBNoSQL.Models;
using ProyectoONGDBNoSQL.Repositories;
using ProyectoONGDBNoSQL.ViewModels;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoONGDBNoSQL.Controllers
{
    public class VoluntariosController : Controller
    {
        private readonly VoluntarioRepository _voluntarioRepository;
        private readonly UsuarioRepository _usuarioRepository;
        private readonly ProyectoRepository _proyectoRepository;

        public VoluntariosController(
            VoluntarioRepository voluntarioRepository,
            UsuarioRepository usuarioRepository,
            ProyectoRepository proyectoRepository)
        {
            _voluntarioRepository = voluntarioRepository;
            _usuarioRepository = usuarioRepository;
            _proyectoRepository = proyectoRepository;
        }

        // GET: Voluntarios/Index
        public async Task<IActionResult> Index()
        {
            var voluntarios = await _voluntarioRepository.GetAllAsync();
            return View(voluntarios);
        }

        // GET: Voluntarios/Details/{id}
        public async Task<IActionResult> Details(string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest();

            var voluntario = await _voluntarioRepository.GetByIdAsync(id);
            if (voluntario == null)
                return NotFound();

            return View(voluntario);
        }

        // CREACI�N MANUAL: No permitida
        public IActionResult Create()
        {
            return NotFound("La creaci�n manual de voluntarios est� deshabilitada. Asigne voluntarios a trav�s del m�dulo de Proyectos.");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(VoluntarioViewModel model)
        {
            return NotFound("La creaci�n manual de voluntarios est� deshabilitada. Asigne voluntarios a trav�s del m�dulo de Proyectos.");
        }

        // GET: Voluntarios/Edit/{id}
        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest();

            var voluntario = await _voluntarioRepository.GetByIdAsync(id);
            if (voluntario == null)
                return NotFound();

            
            var usuarios = await _usuarioRepository.GetAllAsync();
            var proyectos = await _proyectoRepository.GetAllAsync();

            var viewModel = new VoluntarioViewModel
            {
                Id = voluntario.Id,
                InfoUsuarioId = voluntario.InfoUsuarioId,
                HabilidadesText = string.Join(", ", voluntario.Habilidades ?? new System.Collections.Generic.List<string>()),
                Disponibilidad = voluntario.Disponibilidad,
                
                ProyectosSeleccionados = voluntario.HistorialProyectos,
                UsuariosList = usuarios.Select(u => new SelectListItem
                {
                    Value = u.Id,
                    Text = $"{u.Nombre} {u.Apellido}"
                }),
                ProyectosList = proyectos.Select(p => new SelectListItem
                {
                    Value = p.Id,
                    Text = p.NombreProyecto
                })
            };

            return View(viewModel);
        }

        // POST: Voluntarios/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, VoluntarioViewModel model)
        {
            if (id != model.Id)
                return BadRequest();

            if (ModelState.IsValid)
            {
                var habilidadesList = string.IsNullOrWhiteSpace(model.HabilidadesText)
                    ? new System.Collections.Generic.List<string>()
                    : model.HabilidadesText.Split(',', System.StringSplitOptions.RemoveEmptyEntries)
                        .Select(x => x.Trim()).ToList();

                var voluntarioToUpdate = new Voluntario
                {
                    Id = model.Id,
                    InfoUsuarioId = model.InfoUsuarioId,
                    Habilidades = habilidadesList,
                    Disponibilidad = model.Disponibilidad,
                    HistorialProyectos = model.ProyectosSeleccionados
                };

                await _voluntarioRepository.UpdateAsync(id, voluntarioToUpdate);
                return RedirectToAction(nameof(Index));
            }

            var usuarios = await _usuarioRepository.GetAllAsync();
            var proyectos = await _proyectoRepository.GetAllAsync();
            model.UsuariosList = usuarios.Select(u => new SelectListItem
            {
                Value = u.Id,
                Text = $"{u.Nombre} {u.Apellido}"
            });
            model.ProyectosList = proyectos.Select(p => new SelectListItem
            {
                Value = p.Id,
                Text = p.NombreProyecto
            });
            return View(model);
        }

        // GET: Voluntarios/Delete/{id}
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest();

            var voluntario = await _voluntarioRepository.GetByIdAsync(id);
            if (voluntario == null)
                return NotFound();

            return View(voluntario);
        }

        // POST: Voluntarios/Delete/{id}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest();

            
            await _voluntarioRepository.DeleteAsync(id);

            
            await _proyectoRepository.RemoveVolunteerFromProjectsAsync(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
