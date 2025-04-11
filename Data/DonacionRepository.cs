using MongoDB.Driver;
using ProyectoONGDBNoSQL.Data;
using ProyectoONGDBNoSQL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProyectoONGDBNoSQL.Repositories
{
    public class DonacionRepository
    {
        private readonly MongoDbContext _context;

        public DonacionRepository(MongoDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Lista de donaciones.</returns>
        public async Task<List<Donacion>> GetAllAsync()
        {
            return await _context.Database
                .GetCollection<Donacion>("donaciones")
                .Find(d => true)
                .ToListAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">El identificador de la donación (cadena con ObjectId).</param>
        /// <returns>El objeto Donacion si se encuentra; de lo contrario, null.</returns>
        public async Task<Donacion> GetByIdAsync(string id)
        {
            return await _context.Database
                .GetCollection<Donacion>("donaciones")
                .Find(d => d.Id == id)
                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="donacion">La donación a insertar.</param>
        /// <returns>Una tarea asíncrona.</returns>
        public async Task CreateAsync(Donacion donacion)
        {
            await _context.Database
                .GetCollection<Donacion>("donaciones")
                .InsertOneAsync(donacion);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">El identificador de la donación a actualizar.</param>
        /// <param name="donacion">El objeto Donacion con los datos actualizados.</param>
        /// <returns>Una tarea asíncrona.</returns>
        public async Task UpdateAsync(string id, Donacion donacion)
        {
            await _context.Database
                .GetCollection<Donacion>("donaciones")
                .ReplaceOneAsync(d => d.Id == id, donacion);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">El identificador de la donación a eliminar.</param>
        /// <returns>Una tarea asíncrona.</returns>
        public async Task DeleteAsync(string id)
        {
            await _context.Database
                .GetCollection<Donacion>("donaciones")
                .DeleteOneAsync(d => d.Id == id);
        }
    }
}
