using MongoDB.Driver;
using ProyectoONGDBNoSQL.Data;
using ProyectoONGDBNoSQL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProyectoONGDBNoSQL.Repositories
{
    public class DonanteRepository
    {
        private readonly MongoDbContext _context;

        public DonanteRepository(MongoDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Lista de donantes.</returns>
        public async Task<List<Donante>> GetAllAsync()
        {
            return await _context.Database
                .GetCollection<Donante>("donantes")
                .Find(d => true)
                .ToListAsync();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id">El ID del donante.</param>
        /// <returns>El donante encontrado o null.</returns>
        public async Task<Donante> GetByIdAsync(string id)
        {
            return await _context.Database
                .GetCollection<Donante>("donantes")
                .Find(d => d.Id == id)
                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// </summary>
        /// <param name="userId">El ID del usuario.</param>
        /// <returns>El donante asociado o null.</returns>
        public async Task<Donante> GetByUserIdAsync(string userId)
        {
            return await _context.Database
                .GetCollection<Donante>("donantes")
                .Find(d => d.UsuarioId == userId)
                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="donante">El objeto donante a crear.</param>
        public async Task CreateAsync(Donante donante)
        {
            await _context.Database
                .GetCollection<Donante>("donantes")
                .InsertOneAsync(donante);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id">El ID del donante a actualizar.</param>
        /// <param name="donanteIn">El donante con los datos actualizados.</param>
        public async Task UpdateAsync(string id, Donante donanteIn)
        {
            await _context.Database
                .GetCollection<Donante>("donantes")
                .ReplaceOneAsync(d => d.Id == id, donanteIn);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">El ID del donante a eliminar.</param>
        public async Task DeleteAsync(string id)
        {
            await _context.Database
                .GetCollection<Donante>("donantes")
                .DeleteOneAsync(d => d.Id == id);
        }
    }
}
