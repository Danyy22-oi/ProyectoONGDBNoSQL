using MongoDB.Driver;
using ProyectoONGDBNoSQL.Data;
using ProyectoONGDBNoSQL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProyectoONGDBNoSQL.Repositories
{
    public class IncidenciaRepository
    {
        private readonly MongoDbContext _context;

        public IncidenciaRepository(MongoDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Lista de incidencias.</returns>
        public async Task<List<Incidencia>> GetAllAsync()
        {
            return await _context.Database
                .GetCollection<Incidencia>("incidencias")
                .Find(d => true)
                .ToListAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">El identificador de la incidencia (cadena con ObjectId).</param>
        /// <returns>El objeto Incidencia si se encuentra; de lo contrario, null.</returns>
        public async Task<Incidencia> GetByIdAsync(string id)
        {
            return await _context.Database
                .GetCollection<Incidencia>("incidencias")
                .Find(d => d.Id == id)
                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="incidencia">La incidencia a insertar.</param>
        /// <returns>Una tarea asincrona.</returns>
        public async Task CreateAsync(Incidencia incidencia)
        {
            await _context.Database
                .GetCollection<Incidencia>("incidencias")
                .InsertOneAsync(incidencia);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">El identificador de la Incidencia a actualizar.</param>
        /// <param name="incidencia">El objeto Donacion con los datos actualizados.</param>
        /// <returns>Una tarea as�ncrona.</returns>
        public async Task UpdateAsync(string id, Incidencia incidencia)
        {
            await _context.Database
                .GetCollection<Incidencia>("incidencias")
                .ReplaceOneAsync(d => d.Id == id, incidencia);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">El identificador de la incidencia a eliminar.</param>
        /// <returns>Una tarea asincrona.</returns>
        public async Task DeleteAsync(string id)
        {
            await _context.Database
                .GetCollection<Incidencia>("incidencias")
                .DeleteOneAsync(d => d.Id == id);
        }
    }
}
