using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProyectoONGDBNoSQL.Models;
using ProyectoONGDBNoSQL.Repositories;
using ProyectoONGDBNoSQL.ViewModels;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoONGDBNoSQL.Controllers
{
    public class IncidenciasController : Controller
    {
        private readonly IncidenciaRepository _incidenciaRepository;
        private readonly ProyectoRepository _proyectoRepository;
        private readonly VoluntarioRepository _voluntarioRepository;

        public IncidenciasController(
            IncidenciaRepository incidenciaRepository,
            ProyectoRepository proyectoRepository,
            VoluntarioRepository voluntarioRepository)
        {
            _incidenciaRepository = incidenciaRepository;
            _proyectoRepository = proyectoRepository;
            _voluntarioRepository = voluntarioRepository;
        }

        public async Task<IActionResult> Index()
        {
            var incidencias = await _incidenciaRepository.GetAllAsync();
            return View(incidencias);
        }

        public async Task<IActionResult> Details(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) return BadRequest();

            var incidencia = await _incidenciaRepository.GetByIdAsync(id);
            if (incidencia == null) return NotFound();

            return View(incidencia);
        }

        public async Task<IActionResult> Create()
        {
            var proyectos = await _proyectoRepository.GetAllAsync();
            var responsables = await _voluntarioRepository.GetAllAsync();

            var viewModel = new IncidenciaViewModel
            {
                ProyectosList = proyectos.Select(p => new SelectListItem
                {
                    Value = p.Id,
                    Text = p.NombreProyecto
                }),
                ResponsablesList = responsables.Select(r => new SelectListItem
                {
                    Value = r.Id,
                    Text = r.InfoUsuarioId
                }),
                DistribucionesList = new List<SelectListItem>() // Para completar si hay un repo para distribuciones
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IncidenciaViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var proyectos = await _proyectoRepository.GetAllAsync();
                var responsables = await _voluntarioRepository.GetAllAsync();
                model.ProyectosList = proyectos.Select(p => new SelectListItem { Value = p.Id, Text = p.NombreProyecto });
                model.ResponsablesList = responsables.Select(r => new SelectListItem { Value = r.Id, Text = r.InfoUsuarioId });
                model.DistribucionesList = new List<SelectListItem>(); // recargar si hay distribuciones
                return View(model);
            }

            var incidencia = new Incidencia
            {
                IncidenciaId = model.IncidenciaId,
                TipoIncidencia = model.TipoIncidencia,
                Descripcion = model.Descripcion,
                FechaReporte = model.FechaReporte,
                ProyectoId = model.ProyectoId,
                DistrbucionId = model.DistrbucionId,
                Estado = model.Estado,
                ResponsableId = model.ResponsableId
            };

            await _incidenciaRepository.CreateAsync(incidencia);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) return BadRequest();

            var incidencia = await _incidenciaRepository.GetByIdAsync(id);
            if (incidencia == null) return NotFound();

            var proyectos = await _proyectoRepository.GetAllAsync();
            var responsables = await _voluntarioRepository.GetAllAsync();

            var viewModel = new IncidenciaViewModel
            {
                Id = incidencia.Id,
                IncidenciaId = incidencia.IncidenciaId,
                TipoIncidencia = incidencia.TipoIncidencia,
                Descripcion = incidencia.Descripcion,
                FechaReporte = incidencia.FechaReporte,
                ProyectoId = incidencia.ProyectoId,
                DistrbucionId = incidencia.DistrbucionId,
                Estado = incidencia.Estado,
                ResponsableId = incidencia.ResponsableId,
                ProyectosList = proyectos.Select(p => new SelectListItem { Value = p.Id, Text = p.NombreProyecto }),
                ResponsablesList = responsables.Select(r => new SelectListItem { Value = r.Id, Text = r.InfoUsuarioId }),
                DistribucionesList = new List<SelectListItem>()
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, IncidenciaViewModel model)
        {
            if (id != model.Id) return BadRequest();

            if (!ModelState.IsValid)
            {
                var proyectos = await _proyectoRepository.GetAllAsync();
                var responsables = await _voluntarioRepository.GetAllAsync();
                model.ProyectosList = proyectos.Select(p => new SelectListItem { Value = p.Id, Text = p.NombreProyecto });
                model.ResponsablesList = responsables.Select(r => new SelectListItem { Value = r.Id, Text = r.InfoUsuarioId });
                model.DistribucionesList = new List<SelectListItem>();
                return View(model);
            }

            var incidencia = new Incidencia
            {
                Id = model.Id,
                IncidenciaId = model.IncidenciaId,
                TipoIncidencia = model.TipoIncidencia,
                Descripcion = model.Descripcion,
                FechaReporte = model.FechaReporte,
                ProyectoId = model.ProyectoId,
                DistrbucionId = model.DistrbucionId,
                Estado = model.Estado,
                ResponsableId = model.ResponsableId
            };

            await _incidenciaRepository.UpdateAsync(id, incidencia);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) return BadRequest();

            var incidencia = await _incidenciaRepository.GetByIdAsync(id);
            if (incidencia == null) return NotFound();

            return View(incidencia);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) return BadRequest();

            await _incidenciaRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
