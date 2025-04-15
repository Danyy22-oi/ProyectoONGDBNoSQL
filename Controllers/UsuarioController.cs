using Microsoft.AspNetCore.Mvc;
using ProyectoONGDBNoSQL.Models;
using ProyectoONGDBNoSQL.Data;
using ProyectoONGDBNoSQL.Repositories;


namespace ProyectoONGDBNoSQL.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly UsuarioRepository _usuarioRepository;

        public UsuarioController(UsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        // INDEX
        public async Task<IActionResult> Index()
        {
            var usuarios = await _usuarioRepository.GetAllAsync();
            return View(usuarios);
        }

        // GET: CREATE
        public IActionResult Create()
        {
            return View();
        }

        // POST: CREATE
        [HttpPost]
        public async Task<IActionResult> Create(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                await _usuarioRepository.CreateAsync(usuario);
                return RedirectToAction(nameof(Index));
            }
            return View(usuario);
        }

        // GET: Usuario/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(id);
            if (usuario == null) return NotFound();
            return View(usuario);
        }

        // POST: Usuario/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit(string id, Usuario usuario)
        {
            if (id != usuario.Id) return NotFound();

            if (ModelState.IsValid)
            {
                await _usuarioRepository.UpdateAsync(id, usuario);
                return RedirectToAction(nameof(Index));
            }

            return View(usuario);
        }
        // Mostrar confirmación
        public async Task<IActionResult> Delete(string id)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(id);
            if (usuario == null) return NotFound();
            return View(usuario);
        }

        // Confirmar eliminación
        [HttpPost, ActionName("DeleteConfirmed")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            await _usuarioRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
