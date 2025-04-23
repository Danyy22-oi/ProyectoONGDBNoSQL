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
    public class ProyectosController : Controller
    {
        private readonly ProyectoRepository _proyectoRepository;
        private readonly UsuarioRepository _usuarioRepository;
        private readonly VoluntarioRepository _voluntarioRepository;

        public ProyectosController(ProyectoRepository proyectoRepository, UsuarioRepository usuarioRepository, VoluntarioRepository voluntarioRepository)
        {
            _proyectoRepository = proyectoRepository;
            _usuarioRepository = usuarioRepository;
            _voluntarioRepository = voluntarioRepository;
        }

        public async Task<IActionResult> Index()
        {
            var proyectos = await _proyectoRepository.GetAllAsync();
            return View(proyectos);
        }

        public async Task<IActionResult> Details(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return BadRequest();
            var proyecto = await _proyectoRepository.GetByIdAsync(id);
            if (proyecto == null)
                return NotFound();
            return View(proyecto);
        }

        public async Task<IActionResult> Create()
        {
            var usuarios = await _usuarioRepository.GetAllAsync();
            var viewModel = new ProyectoViewModel
            {
                UsuariosList = usuarios.Select(u => new SelectListItem
                {
                    Value = u.Id,
                    Text = $"{u.Nombre} {u.Apellido}"
                })
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProyectoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var usuariosReload = await _usuarioRepository.GetAllAsync();
                model.UsuariosList = usuariosReload.Select(u => new SelectListItem
                {
                    Value = u.Id,
                    Text = $"{u.Nombre} {u.Apellido}"
                });
                return View(model);
            }

            
            var newId = ObjectId.GenerateNewId().ToString();
            var proyecto = new Proyecto
            {
                Id = newId,
                NombreProyecto = model.NombreProyecto,
                TipoCrisis = model.TipoCrisis,
                FechaInicio = model.FechaInicio,
                FechaFin = model.FechaFin,
                Estado = model.Estado,
                Descripcion = model.Descripcion,
                VoluntariosAsignados = new List<string>()
            };

            await _proyectoRepository.CreateAsync(proyecto);

           
            var voluntariosAsignados = new List<string>();
            if (model.VoluntariosSeleccionados != null)
            {
                foreach (var userId in model.VoluntariosSeleccionados.Distinct())
                {
                    var voluntario = await _voluntarioRepository.GetByUserIdAsync(userId);
                    if (voluntario != null)
                    {
                        voluntario.HistorialProyectos ??= new List<string>();
                        if (!voluntario.HistorialProyectos.Contains(newId))
                        {
                            voluntario.HistorialProyectos.Add(newId);
                            await _voluntarioRepository.UpdateAsync(voluntario.Id, voluntario);
                        }
                    }
                    else
                    {
                        var usuario = await _usuarioRepository.GetByIdAsync(userId);
                        voluntario = new Voluntario
                        {
                            InfoUsuarioId = usuario.Id,
                            Habilidades = new List<string>(),
                            Disponibilidad = string.Empty,
                            HistorialProyectos = new List<string> { newId }
                        };
                        await _voluntarioRepository.CreateAsync(voluntario);
                    }

                    if (voluntario != null && !voluntariosAsignados.Contains(voluntario.Id))
                        voluntariosAsignados.Add(voluntario.Id);
                }
            }

            
            proyecto.VoluntariosAsignados = voluntariosAsignados;
            await _proyectoRepository.UpdateAsync(newId, proyecto);

            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return BadRequest();
            var proyecto = await _proyectoRepository.GetByIdAsync(id);
            if (proyecto == null)
                return NotFound();
            var usuarios = await _usuarioRepository.GetAllAsync();
            var viewModel = new ProyectoViewModel
            {
                Id = proyecto.Id,
                NombreProyecto = proyecto.NombreProyecto,
                TipoCrisis = proyecto.TipoCrisis,
                FechaInicio = proyecto.FechaInicio,
                FechaFin = proyecto.FechaFin,
                Estado = proyecto.Estado,
                Descripcion = proyecto.Descripcion,
                VoluntariosSeleccionados = proyecto.VoluntariosAsignados,
                UsuariosList = usuarios.Select(u => new SelectListItem
                {
                    Value = u.Id,
                    Text = $"{u.Nombre} {u.Apellido}"
                })
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, ProyectoViewModel model)
        {
            if (id != model.Id)
                return BadRequest();
            if (!ModelState.IsValid)
            {
                var usuariosReload = await _usuarioRepository.GetAllAsync();
                model.UsuariosList = usuariosReload.Select(u => new SelectListItem
                {
                    Value = u.Id,
                    Text = $"{u.Nombre} {u.Apellido}"
                });
                return View(model);
            }
            var proyectoToUpdate = new Proyecto
            {
                Id = model.Id,
                NombreProyecto = model.NombreProyecto,
                TipoCrisis = model.TipoCrisis,
                FechaInicio = model.FechaInicio,
                FechaFin = model.FechaFin,
                Estado = model.Estado,
                Descripcion = model.Descripcion,
                VoluntariosAsignados = model.VoluntariosSeleccionados.Distinct().ToList()
            };
            await _proyectoRepository.UpdateAsync(id, proyectoToUpdate);
            foreach (var userId in model.VoluntariosSeleccionados.Distinct())
            {
                var voluntario = await _voluntarioRepository.GetByUserIdAsync(userId);
                if (voluntario != null)
                {
                    if (voluntario.HistorialProyectos == null)
                        voluntario.HistorialProyectos = new System.Collections.Generic.List<string>();
                    if (!voluntario.HistorialProyectos.Contains(model.Id))
                    {
                        voluntario.HistorialProyectos.Add(model.Id);
                        await _voluntarioRepository.UpdateAsync(voluntario.Id, voluntario);
                    }
                }
                else
                {
                    var usuario = await _usuarioRepository.GetByIdAsync(userId);
                    if (usuario != null)
                    {
                        voluntario = new Voluntario
                        {
                            InfoUsuarioId = usuario.Id,
                            Habilidades = new System.Collections.Generic.List<string>(),
                            Disponibilidad = string.Empty,
                            HistorialProyectos = new System.Collections.Generic.List<string> { model.Id }
                        };
                        await _voluntarioRepository.CreateAsync(voluntario);
                    }
                }
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return BadRequest();
            var proyecto = await _proyectoRepository.GetByIdAsync(id);
            if (proyecto == null)
                return NotFound();
            return View(proyecto);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return BadRequest();
            await _proyectoRepository.DeleteAsync(id);
            await _voluntarioRepository.RemoveProjectFromVolunteersAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
