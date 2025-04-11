// Archivo: UsuarioRepository.cs, ubicado en la carpeta Repositories
using MongoDB.Driver;
using ProyectoONGDBNoSQL.Data;
using ProyectoONGDBNoSQL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProyectoONGDBNoSQL.Repositories
{
    public class UsuarioRepository
    {
        private readonly MongoDbContext _context;

        public UsuarioRepository(MongoDbContext context)
        {
            _context = context;
        }

        public async Task<List<Usuario>> GetAllAsync()
        {
            return await _context.Usuarios.Find(u => true).ToListAsync();
        }

        public async Task<Usuario> GetByIdAsync(string id)
        {
            return await _context.Usuarios.Find(u => u.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Usuario> GetByEmailAsync(string email)
        {
            return await _context.Usuarios.Find(u => u.Email == email).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(Usuario usuario)
        {
            await _context.Usuarios.InsertOneAsync(usuario);
        }

        public async Task UpdateAsync(string id, Usuario usuarioIn)
        {
            await _context.Usuarios.ReplaceOneAsync(u => u.Id == id, usuarioIn);
        }

        public async Task DeleteAsync(string id)
        {
            await _context.Usuarios.DeleteOneAsync(u => u.Id == id);
        }
    }
}
