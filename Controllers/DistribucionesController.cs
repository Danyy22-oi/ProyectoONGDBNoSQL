using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ONG.Models;
using ProyectoONGDBNoSQL.Models;
using ProyectoONGDBNoSQL.Repositories;
using ProyectoONGDBNoSQL.ViewModels;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoONGDBNoSQL.Controllers
{
    public class DistribucionesController : Controller
    {
        private readonly DistribucionRepository _distribucionRepository;
        private readonly ProyectoRepository _proyectoRepository;
        private readonly UsuarioRepository _usuarioRepository;

        public DistribucionesController(
            DistribucionRepository distribucionRepository,
            ProyectoRepository proyectoRepository,
            UsuarioRepository usuarioRepository)
        {
            _distribucionRepository = distribucionRepository;
            _proyectoRepository = proyectoRepository;
            _usuarioRepository = usuarioRepository;
        }

        public async Task<IActionResult> Index()
        {
            var distribuciones = await _distribucionRepository.GetAllAsync();
            var viewModel = distribuciones.Select(d => new DistribucionViewModel
            {
                RecursoIdList = d.RecursoEnviados.Select(recurso => new SelectListItem
                {
                    Value = recurso.RecursoId.ToString(),
                    Text = recurso.RecursoId.ToString()
                }).ToList()
            }).ToList();

            return View(viewModel);
        }

        public async Task<IActionResult> Details(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return BadRequest();

            var distribucion = await _distribucionRepository.GetByIdAsync(id);
            if (distribucion == null)
                return NotFound();

            return View(distribucion);
        }

        public async Task<IActionResult> Create()
        {
            var proyectos = await _proyectoRepository.GetAllAsync();
            var usuarios = await _usuarioRepository.GetAllAsync();

            var viewModel = new DistribucionViewModel
            {
                ProyectosList = proyectos.Select(p => new SelectListItem
                {
                    Value = p.Id,
                    Text = p.NombreProyecto
                }),
                ResponsablesList = usuarios.Select(u => new SelectListItem
                {
                    Value = u.Id,
                    Text = $"{u.Nombre} {u.Apellido}"
                })
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DistribucionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var proyectos = await _proyectoRepository.GetAllAsync();
                var usuarios = await _usuarioRepository.GetAllAsync();
                model.ProyectosList = proyectos.Select(p => new SelectListItem
                {
                    Value = p.Id,
                    Text = p.NombreProyecto
                });
                model.ResponsablesList = usuarios.Select(u => new SelectListItem
                {
                    Value = u.Id,
                    Text = $"{u.Nombre} {u.Apellido}"
                });
                return View(model);
            }

            var distribucion = new Distribucion
            {
                ProyectoId = model.ProyectoId,
                FechaEnvio = model.FechaEnvio,
                Destino = model.Destino,
                RecursoEnviados = model.RecursoIdList.Select(recurso => new RecursoEnviado
                {
                    RecursoId = recurso.Value
                }).ToList(),
                ResponsableId = model.ResponsableId,
                Estado = model.Estado
            };

            await _distribucionRepository.CreateAsync(distribucion);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return BadRequest();

            var distribucion = await _distribucionRepository.GetByIdAsync(id);
            if (distribucion == null)
                return NotFound();

            var proyectos = await _proyectoRepository.GetAllAsync();
            var usuarios = await _usuarioRepository.GetAllAsync();

            var viewModel = new DistribucionViewModel
            {
                Id = distribucion.Id,
                ProyectoId = distribucion.ProyectoId,
                FechaEnvio = distribucion.FechaEnvio,
                Destino = distribucion.Destino,
                ResponsableId = distribucion.ResponsableId,
                Estado = distribucion.Estado,
                RecursoIdList = distribucion.RecursoEnviados.Select(recurso => new SelectListItem
                {
                    Value = recurso.RecursoId,
                    Text = recurso.RecursoId
                }).ToList(),
                ProyectosList = proyectos.Select(p => new SelectListItem
                {
                    Value = p.Id,
                    Text = p.NombreProyecto
                }),
                ResponsablesList = usuarios.Select(u => new SelectListItem
                {
                    Value = u.Id,
                    Text = $"{u.Nombre} {u.Apellido}"
                })
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, DistribucionViewModel model)
        {
            if (id != model.Id)
                return BadRequest();

            if (!ModelState.IsValid)
            {
                var proyectos = await _proyectoRepository.GetAllAsync();
                var usuarios = await _usuarioRepository.GetAllAsync();
                model.ProyectosList = proyectos.Select(p => new SelectListItem
                {
                    Value = p.Id,
                    Text = p.NombreProyecto
                });
                model.ResponsablesList = usuarios.Select(u => new SelectListItem
                {
                    Value = u.Id,
                    Text = $"{u.Nombre} {u.Apellido}"
                });
                return View(model);
            }

            var updatedDistribucion = new Distribucion
            {
                Id = model.Id,
                ProyectoId = model.ProyectoId,
                FechaEnvio = model.FechaEnvio,
                Destino = model.Destino,
                RecursoEnviados = model.RecursoIdList.Select(recurso => new RecursoEnviado
                {
                    RecursoId = recurso.Value
                }).ToList(),
                ResponsableId = model.ResponsableId,
                Estado = model.Estado
            };

            await _distribucionRepository.UpdateAsync(id, updatedDistribucion);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return BadRequest();

            var distribucion = await _distribucionRepository.GetByIdAsync(id);
            if (distribucion == null)
                return NotFound();

            return View(distribucion);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return BadRequest();

            await _distribucionRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}