using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProyectoONGDBNoSQL.Models;
using ProyectoONGDBNoSQL.Repositories;
using ProyectoONGDBNoSQL.ViewModels;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoONGDBNoSQL.Controllers
{
    public class DonacionesController : Controller
    {
        private readonly DonacionRepository _donacionRepository;
        private readonly UsuarioRepository _usuarioRepository;
        private readonly DonanteRepository _donanteRepository;

        public DonacionesController(
            DonacionRepository donacionRepository,
            UsuarioRepository usuarioRepository,
            DonanteRepository donanteRepository)
        {
            _donacionRepository = donacionRepository;
            _usuarioRepository = usuarioRepository;
            _donanteRepository = donanteRepository;
        }

        // GET: Donaciones/Index
        public async Task<IActionResult> Index()
        {
            var donaciones = await _donacionRepository.GetAllAsync();
            return View(donaciones);
        }

        // GET: Donaciones/Details/{id}
        public async Task<IActionResult> Details(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return BadRequest();

            var donacion = await _donacionRepository.GetByIdAsync(id);
            if (donacion == null)
                return NotFound();

            return View(donacion);
        }

        // GET: Donaciones/Create
        public async Task<IActionResult> Create()
        {
            
            var usuarios = await _usuarioRepository.GetAllAsync();
            var viewModel = new DonacionViewModel
            {
                DonantesList = usuarios.Select(u => new SelectListItem
                {
                    Value = u.Id, 
                    Text = $"{u.Nombre} {u.Apellido}"
                })
            };
            return View(viewModel);
        }

        // POST: Donaciones/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DonacionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var usuariosReload = await _usuarioRepository.GetAllAsync();
                model.DonantesList = usuariosReload.Select(u => new SelectListItem
                {
                    Value = u.Id,
                    Text = $"{u.Nombre} {u.Apellido}"
                });
                return View(model);
            }


            var donante = await _donanteRepository.GetByUserIdAsync(model.DonanteId);
            if (donante == null)
            {
                
                var usuario = await _usuarioRepository.GetByIdAsync(model.DonanteId);
                if (usuario == null)
                {
                    ModelState.AddModelError("DonanteId", "El usuario seleccionado no existe.");
                    var usuarios = await _usuarioRepository.GetAllAsync();
                    model.DonantesList = usuarios.Select(u => new SelectListItem
                    {
                        Value = u.Id,
                        Text = $"{u.Nombre} {u.Apellido}"
                    });
                    return View(model);
                }

                
                donante = new Donante
                {
                    UsuarioId = usuario.Id,
                    Nombre = $"{usuario.Nombre} {usuario.Apellido}",
                    TipoDonante = "persona",
                    
                    Contacto = new Donante.ContactoData
                    {
                        Email = usuario.Email, 
                        Telefono = model.ContactoTelefono
                    },
                    Donaciones = new System.Collections.Generic.List<string>()
                };

                await _donanteRepository.CreateAsync(donante);
            }

            
            var donacion = new Donacion
            {
                DonanteId = donante.Id,
                FechaDonacion = model.FechaDonacion,
                Tipo = model.Tipo,
                Detalle = model.Detalle,
                Estado = model.Estado
            };

            await _donacionRepository.CreateAsync(donacion);
            return RedirectToAction(nameof(Index));
        }


        // GET: Donaciones/Edit/{id}
        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return BadRequest();
            var donacion = await _donacionRepository.GetByIdAsync(id);
            if (donacion == null)
                return NotFound();

            var usuarios = await _usuarioRepository.GetAllAsync();
            var viewModel = new DonacionViewModel
            {
                Id = donacion.Id,
                DonanteId = donacion.DonanteId,
                FechaDonacion = donacion.FechaDonacion,
                Tipo = donacion.Tipo,
                Detalle = donacion.Detalle,
                Estado = donacion.Estado,
                DonantesList = usuarios.Select(u => new SelectListItem
                {
                    Value = u.Id,
                    Text = $"{u.Nombre} {u.Apellido}"
                })
            };

            return View(viewModel);
        }

        // POST: Donaciones/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, DonacionViewModel model)
        {
            if (id != model.Id)
                return BadRequest();

            if (!ModelState.IsValid)
            {
                var usuarios = await _usuarioRepository.GetAllAsync();
                model.DonantesList = usuarios.Select(u => new SelectListItem
                {
                    Value = u.Id,
                    Text = $"{u.Nombre} {u.Apellido}"
                });
                return View(model);
            }

            var donacionToUpdate = new Donacion
            {
                Id = model.Id,
                DonanteId = model.DonanteId,
                FechaDonacion = model.FechaDonacion,
                Tipo = model.Tipo,
                Detalle = model.Detalle,
                Estado = model.Estado
            };

            await _donacionRepository.UpdateAsync(id, donacionToUpdate);
            return RedirectToAction(nameof(Index));
        }

        // GET: Donaciones/Delete/{id}
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return BadRequest();
            var donacion = await _donacionRepository.GetByIdAsync(id);
            if (donacion == null)
                return NotFound();
            return View(donacion);
        }

        // POST: Donaciones/Delete/{id}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return BadRequest();
            await _donacionRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
