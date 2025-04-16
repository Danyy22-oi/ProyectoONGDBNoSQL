using MongoDB.Driver;
using ProyectoONGDBNoSQL.Data;
using ProyectoONGDBNoSQL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProyectoONGDBNoSQL.Repositories
{
    public class ComunicacionRepository
    {
        private readonly MongoDbContext _context;

        public ComunicacionRepository(MongoDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtiene todas las comunicaciones.
        /// </summary>
        /// <returns>Lista de comunicaciones.</returns>
        public async Task<List<Comunicacion>> GetAllAsync()
        {
            return await _context.Database
                .GetCollection<Comunicacion>("comunicaciones")
                .Find(c => true)
                .ToListAsync();
        }

        /// <summary>
        /// Obtiene una comunicación por su Id.
        /// </summary>
        /// <param name="id">El identificador de la comunicación.</param>
        /// <returns>La comunicación correspondiente o null.</returns>
        public async Task<Comunicacion> GetByIdAsync(string id)
        {
            return await _context.Database
                .GetCollection<Comunicacion>("comunicaciones")
                .Find(c => c.Id == id)
                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Crea una nueva comunicación.
        /// </summary>
        /// <param name="comunicacion">La comunicación a insertar.</param>
        public async Task CreateAsync(Comunicacion comunicacion)
        {
            await _context.Database
                .GetCollection<Comunicacion>("comunicaciones")
                .InsertOneAsync(comunicacion);
        }

        /// <summary>
        /// Actualiza una comunicación existente.
        /// </summary>
        /// <param name="id">El identificador de la comunicación a actualizar.</param>
        /// <param name="comunicacion">Los nuevos datos de la comunicación.</param>
        public async Task UpdateAsync(string id, Comunicacion comunicacion)
        {
            await _context.Database
                .GetCollection<Comunicacion>("comunicaciones")
                .ReplaceOneAsync(c => c.Id == id, comunicacion);
        }

        /// <summary>
        /// Elimina una comunicación.
        /// </summary>
        /// <param name="id">El identificador de la comunicación a eliminar.</param>
        public async Task DeleteAsync(string id)
        {
            await _context.Database
                .GetCollection<Comunicacion>("comunicaciones")
                .DeleteOneAsync(c => c.Id == id);
        }
    }
}