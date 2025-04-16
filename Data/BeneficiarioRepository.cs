using MongoDB.Driver;
using ProyectoONGDBNoSQL.Data;
using ProyectoONGDBNoSQL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProyectoONGDBNoSQL.Repositories
{
    public class BeneficiarioRepository
    {
        private readonly MongoDbContext _context;

        public BeneficiarioRepository(MongoDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Lista de donaciones.</returns>
        public async Task<List<Beneficiario>> GetAllAsync()
        {
            return await _context.Database
                .GetCollection<Beneficiario>("beneficiarios")
                .Find(d => true)
                .ToListAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">El identificador del beneficiario (cadena con ObjectId).</param>
        /// <returns>El objeto Beneficiario si se encuentra; de lo contrario, null.</returns>
        public async Task<Beneficiario> GetByIdAsync(string id)
        {
            return await _context.Database
                .GetCollection<Beneficiario>("beneficiarios")
                .Find(d => d.Id == id)
                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="beneficiario">el beneficiario a insertar.</param>
        /// <returns>Una tarea as�ncrona.</returns>
        public async Task CreateAsync(Beneficiario beneficiario)
        {
            await _context.Database
                .GetCollection<Beneficiario>("beneficiarios")
                .InsertOneAsync(beneficiario);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">El identificador de la beneficiario a actualizar.</param>
        /// <param name="beneficiario">El objeto beneficiario con los datos actualizados.</param>
        /// <returns>Una tarea as�ncrona.</returns>
        public async Task UpdateAsync(string id, Beneficiario beneficiario)
        {
            await _context.Database
                .GetCollection<Beneficiario>("beneficiarios")
                .ReplaceOneAsync(d => d.Id == id, beneficiario);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">El identificador del beneficiario a eliminar.</param>
        /// <returns>Una tarea as�ncrona.</returns>
        public async Task DeleteAsync(string id)
        {
            await _context.Database
                .GetCollection<Beneficiario>("beneficiarios")
                .DeleteOneAsync(d => d.Id == id);
        }
    }
}
